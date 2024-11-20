using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using RetakesPluginShared;

namespace Zones;

public partial class Zones
{
    public override string ModuleName => "Retakes-Zones";
    public override string ModuleVersion => "1.1.1";
    public override string ModuleAuthor => "https://github.com/oscar-wos/Retakes-Zones";
    private static PluginCapability<IRetakesPluginEventSender> RetakesPluginEventSenderCapability { get; } = new("retakes_plugin:event_sender");

    private readonly List<Zone> _zones = [];
    private readonly Dictionary<CCSPlayerController, PlayerData> _playerData = [];
}