using CounterStrikeSharp.API;
using Zones.Enums;

namespace Zones;

public partial class Zones
{
    private void OnTick()
    {
        foreach (var controller in Utilities.GetPlayers().Where(c => c is { IsValid: true, PawnIsAlive: true }))
        {
            if (controller.PlayerPawn.Value == null || !_playerData.TryGetValue(controller, out var playerData))
                continue;

            foreach (var zone in playerData.Zones)
            {
                var isInZone = zone.IsInZone(controller.PlayerPawn.Value.AbsOrigin!);

                if (zone.Type == ZoneType.Red && isInZone)
                {
                    controller.Bounce();
                    continue;
                }

                if (zone.Type != ZoneType.Green)
                    continue;

                switch (isInZone)
                {
                    case true when !playerData.GreenZones.Contains(zone):
                        playerData.GreenZones.Add(zone);
                        break;

                    case false when playerData.GreenZones.Contains(zone):
                        playerData.GreenZones.Remove(zone);

                        if (playerData.GreenZones.Count == 0)
                            controller.Bounce();

                        break;
                }
            }
        }
    }

    private void OnMapStart(string mapName)
    {
        LoadJson(mapName);
    }

    private void OnClientPutInServer(int playerSlot)
    {
        var controller = Utilities.GetPlayerFromSlot(playerSlot);

        if (controller == null || !controller.IsValid)
            return;

        _playerData[controller] = new PlayerData();
    }

    private void OnClientDisconnect(int playerSlot)
    {
        var controller = Utilities.GetPlayerFromSlot(playerSlot);

        if (controller == null || !controller.IsValid)
            return;

        _playerData.Remove(controller);
    }
}