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

        if (!isReload)
            return;

        LoadJson(Server.MapName);
    }

    public override void OnAllPluginsLoaded(bool isReload)
    {
        RetakesPluginEventSenderCapability.Get()!.RetakesPluginEventHandlers += OnRetakesEvent;
    }
}