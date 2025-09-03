using CounterStrikeSharp.API.Modules.Utils;
using Zones.Enums;

namespace Zones;

public class Zone(ZoneType type, CsTeam[] teams, float[] minPoint, float[] maxPoint)
{
    public ZoneType Type { get; } = type;
    public CsTeam[] Teams { get; } = teams;
    private float[] MinPoint { get; } = minPoint;
    private float[] MaxPoint { get; } = maxPoint;

    public bool IsInZone(Vector point) =>
        point.X >= MinPoint[0]
        && point.X <= MaxPoint[0]
        && point.Y >= MinPoint[1]
        && point.Y <= MaxPoint[1]
        && point.Z + 36 >= MinPoint[2]
        && point.Z + 36 <= MaxPoint[2];
}
