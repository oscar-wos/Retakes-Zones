using System.Text.Json;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Enums;
using Zones.Enums;

namespace Zones;

public partial class Zones
{
    private void LoadJson(string mapName)
    {
        _zones.Clear();
        var path = $"../../csgo/addons/counterstrikesharp/configs/plugins/Zones/{mapName}.json";

        if (!File.Exists(path))
            return;

        var json = File.ReadAllText(path);
        var obj = JsonSerializer.Deserialize<JsonBombsite>(json);

        if (obj == null)
            return;

        foreach (var zone in obj.a)
            AddZone(Bombsite.A, zone);

        foreach (var zone in obj.b)
            AddZone(Bombsite.B, zone);

        return;

        void AddZone(Bombsite bombsite, JsonZone zone)
        {
            _zones.Add(new Zone(
                bombsite,
                (ZoneType)zone.type,
                zone.teams.Select(t => (CsTeam)Enum.ToObject(typeof(CsTeam), t)).ToArray(),
                [Math.Min(zone.x[0], zone.y[0]), Math.Min(zone.x[1], zone.y[1]), Math.Min(zone.x[2], zone.y[2])],
                [Math.Max(zone.x[0], zone.y[0]), Math.Max(zone.x[1], zone.y[1]), Math.Max(zone.x[2], zone.y[2])]
            ));
        }
    }
}

public class JsonBombsite
{
    public required JsonZone[] a { get; init; }
    public required JsonZone[] b { get; init; }
}

public class JsonZone
{
    public required int type { get; set; }
    public required int[] teams { get; set; }
    public required float[] x { get; set; }
    public required float[] y { get; set; }
}