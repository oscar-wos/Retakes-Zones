using CounterStrikeSharp.API;
using RetakesPluginShared.Events;

namespace Zones;

public partial class Zones
{
    private void OnRetakesEvent(object? sender, IRetakesPluginEvent @event)
    {
        if (@event is not AnnounceBombsiteEvent announceBombsiteEvent)
            return;

        foreach (var player in Utilities.GetPlayers().Where(IsValidPlayer))
            LoadPlayerZones(player, announceBombsiteEvent.Bombsite);
    }
}