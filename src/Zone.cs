using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Enums;
using Zones.Enums;

namespace Zones;

public class Zone(Bombsite bombsite, ZoneType type, CsTeam[] teams, float[] minPoint, float[] maxPoint)
{
    public Bombsite Bombsite { get; } = bombsite;
    public ZoneType Type { get; } = type;
    public CsTeam[] Teams { get; } = teams;
    private float[] MinPoint { get; } = minPoint;
    private float[] MaxPoint { get; } = maxPoint;

    public bool IsInZone(Vector point)
    {
        return point.X >= MinPoint[0] && point.X <= MaxPoint[0] && point.Y >= MinPoint[1] && point.Y <= MaxPoint[1] && point.Z >= MinPoint[2] && point.Z <= MaxPoint[2];
    }
}