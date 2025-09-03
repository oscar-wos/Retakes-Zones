using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Events;

namespace Zones;

public partial class Zones
{
    private void OnRetakesEvent(object? sender, IRetakesPluginEvent @event)
    {
        if (@event is not AnnounceBombsiteEvent announceBombsiteEvent)
        {
            return;
        }

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

            CsTeam playerTeam = (CsTeam)player.PlayerPawn.Value.TeamNum;

            playerData.Zones = [];
            playerData.GreenZones = [];

            foreach (Zone zone in _zones[(int)announceBombsiteEvent.Bombsite])
            {
                if (zone.Teams.Contains(playerTeam))
                {
                    playerData.Zones.Add(zone);
                }
            }
        }
    }
}
