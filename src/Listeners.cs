using CounterStrikeSharp.API;
using Zones.Enums;

namespace Zones;

public partial class Zones
{
    private void OnTick()
    {
        foreach (var player in Utilities.GetPlayers().Where(p => p is { IsValid: true, PawnIsAlive: true }))
        {
            var pos = player.PlayerPawn.Value?.AbsOrigin;

            if (pos == null || !_playerData.TryGetValue(player, out var playerData))
                continue;

            foreach (var zone in playerData.Zones)
            {
                var isInZone = zone.IsInZone(pos);

                if (zone.Type == ZoneType.Red && isInZone)
                {
                    player.PlayerPawn.Value!.Bounce();
                    continue;
                }

                if (zone.Type != ZoneType.Green)
                    continue;

                switch (isInZone)
                {
                    case true when !playerData.GreenZones.Contains(zone):
                        playerData.GreenZones.Add(zone);
                        break;

                    case false when playerData.GreenZones.Contains(zone):
                        playerData.GreenZones.Remove(zone);

                        if (playerData.GreenZones.Count == 0)
                            player.PlayerPawn.Value!.Bounce();

                        break;
                }
            }
        }
    }

    private void OnMapStart(string mapName)
    {
        LoadJson(mapName);
    }

    private void OnClientPutInServer(int playerSlot)
    {
        var player = Utilities.GetPlayerFromSlot(playerSlot);

        if (player == null || !player.IsValid)
            return;

        _playerData[player] = new PlayerData();
    }

    private void OnClientDisconnect(int playerSlot)
    {
        var player = Utilities.GetPlayerFromSlot(playerSlot);

        if (player == null || !player.IsValid)
            return;

        _playerData.Remove(player);
    }
}