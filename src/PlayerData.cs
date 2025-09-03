using CounterStrikeSharp.API.Core;

namespace Zones;

public class PlayerData
{
    public required CCSPlayerController Player { get; set; }
    public List<Zone> Zones { get; set; } = [];
    public List<Zone> GreenZones { get; set; } = [];
}
