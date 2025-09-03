using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace Zones;

public partial class Zones : BasePlugin
{
    public override void Load(bool isReload)
    {
        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnMapStart>(OnMapStart);
        RegisterListener<Listeners.OnClientPutInServer>(OnClientPutInServer);
        RegisterListener<Listeners.OnClientDisconnect>(OnClientDisconnect);

        if (isReload)
        {
            ReloadPlugin();
        }
    }

    public override void OnAllPluginsLoaded(bool isReload) =>
        _retakesEventSenderCapability.Get()!.RetakesPluginEventHandlers += OnRetakesEvent;

    public override void Unload(bool isReload) =>
        _retakesEventSenderCapability.Get()!.RetakesPluginEventHandlers -= OnRetakesEvent;

    private void ReloadPlugin()
    {
        LoadJson(Server.MapName);

        Server.NextFrame(() =>
        {
            foreach (
                CCSPlayerController player in Utilities
                    .GetPlayers()
                    .Where(p => p is { IsValid: true })
            )
            {
                _playerData[player.Slot] = new PlayerData() { Player = player };
            }
        });
    }
}
