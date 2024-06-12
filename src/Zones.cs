using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Enums;

namespace Zones;

public partial class Zones : BasePlugin
{
    public override void Load(bool isReload)
    {
        RegisterListener<Listeners.OnMapStart>(OnMapStart);
        AddTimer(0.1f, Timer_Repeat, TimerFlags.REPEAT);

        if (!isReload)
            return;

        LoadJson(Server.MapName);
    }

    public override void OnAllPluginsLoaded(bool isReload)
    {
        RetakesPluginEventSenderCapability.Get()!.RetakesPluginEventHandlers += OnRetakesEvent;
    }

    private void LoadPlayerZones(CCSPlayerController player, Bombsite bombsite)
    {
        _playerZones.Remove(player.Slot);
        _playerLastPosition.Remove(player.Slot);
        _playerLastVelocity.Remove(player.Slot);
        _playerGreenZones.Remove(player.Slot);

        var playerZones = _zones.Where(z => z.Bombsite == bombsite && z.Teams.Contains((CsTeam)player!.PlayerPawn.Value!.TeamNum)).ToList();
        _playerZones.Add(player.Slot, playerZones);
        _playerLastPosition.Add(player.Slot, player!.PlayerPawn.Value!.AbsOrigin!);
        _playerLastVelocity.Add(player.Slot, player!.PlayerPawn.Value!.AbsVelocity!);
        _playerGreenZones.Add(player.Slot, []);
    }

    private void Timer_Repeat()
    {
        foreach (var player in Utilities.GetPlayers().Where(IsValidPlayer))
        {
            if (!_playerZones.TryGetValue(player.Slot, out var zones))
                continue;

            var pos = player!.PlayerPawn.Value!.AbsOrigin;
            var vel = player.PlayerPawn.Value!.AbsVelocity;
            var eyes = player.PlayerPawn.Value!.EyeAngles;
            var teleport = false;

            foreach (var zone in zones)
            {
                var isInZone = zone.IsInZone(pos!);

                if (zone.Type == ZoneType.Red && isInZone)
                {
                    teleport = true;
                    BouncePlayer(player, eyes);
                    continue;
                }

                if (zone.Type != ZoneType.Green)
                    continue;

                switch (isInZone)
                {
                    case true when !_playerGreenZones[player.Slot].Contains(zone):
                        _playerGreenZones[player.Slot].Add(zone);
                        break;
                    case false when _playerGreenZones[player.Slot].Contains(zone):
                        _playerGreenZones[player.Slot].Remove(zone);

                        if (_playerGreenZones[player.Slot].Count == 0)
                        {
                            teleport = true;
                            BouncePlayer(player, eyes);
                        }
                        break;
                }
            }

            if (teleport)
                continue;

            _playerLastPosition[player.Slot] = pos!;
            _playerLastVelocity[player.Slot] = vel!;
        }
    }

    private void BouncePlayer(CCSPlayerController controller, QAngle eyes)
    {
        var pos = _playerLastPosition[controller.Slot];
        var vel = _playerLastVelocity[controller.Slot];
        var speed = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

        vel *= (-350 / (float)speed);
        vel.Z = vel.Z <= 0f ? 150f : Math.Max(vel.Z, 150f);
        controller.PlayerPawn.Value!.Teleport(pos, eyes, vel);
    }

    private static bool IsValidPlayer(CCSPlayerController? player)
    {
        return player != null && player is { IsValid: true, Connected: PlayerConnectedState.PlayerConnected, PawnIsAlive: true, IsBot: false, IsHLTV: false };
    }
}