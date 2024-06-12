using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Enums;

namespace Zones;

public class Zone(Bombsite bombsite, ZoneType type, CsTeam[] teams, Vector minPoint, Vector maxPoint)
{
    public Bombsite Bombsite { get; init; } = bombsite;
    public ZoneType Type { get; init; } = type;
    public CsTeam[] Teams { get; init; } = teams;
    public Vector MinPoint { get; init; } = minPoint;
    public Vector MaxPoint { get; init; } = maxPoint;

    public bool IsInZone(Vector point)
    {
        return point.X >= MinPoint.X && point.X <= MaxPoint.X && point.Y >= MinPoint.Y && point.Y <= MaxPoint.Y && point.Z >= MinPoint.Z && point.Z <= MaxPoint.Z;
    }
}