using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using Zones.Enums;
using Zones.Extensions;

namespace Zones;

public partial class Zones
{
    private void OnTick()
    {
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            PlayerData? playerData = _playerData[i];
            CCSPlayerController? player = playerData?.Player;

            if (
                playerData is null
                || player is not { IsValid: true, LifeState: (byte)LifeState_t.LIFE_ALIVE }
                || player.PlayerPawn.Value is not { IsValid: true }
            )
            {
                continue;
            }

            Vector? pos = player.PlayerPawn.Value.AbsOrigin;

            if (pos is null)
            {
                continue;
            }

            foreach (Zone zone in playerData.Zones)
            {
                bool isInZone = zone.IsInZone(pos);

                if (zone.Type == ZoneType.Red && isInZone)
                {
                    player.PlayerPawn.Value.Bounce();
                    continue;
                }

                if (zone.Type != ZoneType.Green)
                {
                    continue;
                }

                switch (isInZone)
                {
                    case true when !playerData.GreenZones.Contains(zone):
                        playerData.GreenZones.Add(zone);
                        break;

                    case false when playerData.GreenZones.Contains(zone):
                        _ = playerData.GreenZones.Remove(zone);

                        if (playerData.GreenZones.Count == 0)
                        {
                            player.PlayerPawn.Value!.Bounce();
                        }

                        break;
                }
            }
        }
    }

    private void OnMapStart(string mapName) => LoadJson(mapName);

    private void OnClientPutInServer(int playerSlot)
    {
        CCSPlayerController? player = Utilities.GetPlayerFromSlot(playerSlot);

        if (player is not { IsValid: true })
        {
            return;
        }

        _playerData[playerSlot] = new PlayerData { Player = player };
    }

    private void OnClientDisconnect(int playerSlot) => _playerData[playerSlot] = null;
}
