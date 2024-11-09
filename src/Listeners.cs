using CounterStrikeSharp.API;

namespace Zones;

public partial class Zones
{
    private void OnTick()
    {
    }

    private void OnMapStart(string mapName)
    {
        LoadJson(mapName);
    }

    private void OnClientPutInServer(int playerSlot)
    {
        var controller = Utilities.GetPlayerFromSlot(playerSlot);

        if (controller == null || !controller.IsValid())
            return;
    }
}