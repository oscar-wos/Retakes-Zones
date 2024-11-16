using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Events;

namespace Zones;

public partial class Zones
{
    private void OnRetakesEvent(object? sender, IRetakesPluginEvent @event)
    {
        if (@event is not AnnounceBombsiteEvent announceBombsiteEvent)
            return;

        var zones = _zones.Where(z => z.Bombsite == announceBombsiteEvent.Bombsite).ToList();

        foreach (var player in Utilities.GetPlayers().Where(p => p is { IsValid: true, PawnIsAlive: true }))
        {
            if (player.PlayerPawn.Value == null || !_playerData.TryGetValue(player, out var playerData))
                continue;

            playerData.Zones = zones.Where(z => z.Teams.Contains((CsTeam)player.PlayerPawn.Value.TeamNum)).ToList();
            playerData.GreenZones = [];
        }
    }
}