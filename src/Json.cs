using System.Text.Json;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared.Enums;
using Zones.Enums;

namespace Zones;

public partial class Zones
{
    private void LoadJson(string mapName)
    {
        for (int i = 0; i < Enum.GetValues(typeof(Bombsite)).Length; i++)
        {
            _zones[i] = [];
        }

        string path = $"../../csgo/addons/counterstrikesharp/configs/plugins/Zones/{mapName}.json";

        if (!File.Exists(path))
        {
            return;
        }

        string json = File.ReadAllText(path);
        JsonBombsite? obj = JsonSerializer.Deserialize<JsonBombsite>(json);

        if (obj is null)
        {
            return;
        }

        foreach (JsonZone jsonZone in obj.a)
        {
            AddZone(Bombsite.A, jsonZone);
        }

        foreach (JsonZone jsonZone in obj.b)
        {
            AddZone(Bombsite.B, jsonZone);
        }
    }

    private void AddZone(Bombsite bombsite, JsonZone jsonZone)
    {
        Zone zone = new(
            (ZoneType)jsonZone.type,
            [.. jsonZone.teams.Select(t => (CsTeam)Enum.ToObject(typeof(CsTeam), t))],
            [
                Math.Min(jsonZone.x[0], jsonZone.y[0]),
                Math.Min(jsonZone.x[1], jsonZone.y[1]),
                Math.Min(jsonZone.x[2], jsonZone.y[2]),
            ],
            [
                Math.Max(jsonZone.x[0], jsonZone.y[0]),
                Math.Max(jsonZone.x[1], jsonZone.y[1]),
                Math.Max(jsonZone.x[2], jsonZone.y[2]),
            ]
        );

        _zones[(int)bombsite].Add(zone);
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
