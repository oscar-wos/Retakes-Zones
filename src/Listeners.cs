using CounterStrikeSharp.API.Modules.Utils;

namespace Zones;

public partial class Zones
{
    private void OnMapStart(string mapName)
    {
        LoadJson(mapName);
    }
}