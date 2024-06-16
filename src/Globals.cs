using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Utils;
using RetakesPluginShared;

namespace Zones;

public partial class Zones
{
    public override string ModuleName => "Retakes-Zones";
    public override string ModuleVersion => "1.0.2";
    public override string ModuleAuthor => "https://github.com/oscar-wos/Retakes-Zones";
    private static PluginCapability<IRetakesPluginEventSender> RetakesPluginEventSenderCapability { get; } = new("retakes_plugin:event_sender");

    private readonly List<Zone> _zones = [];
    private readonly Dictionary<int, List<Zone>> _playerZones = [];
    private readonly Dictionary<int, List<Zone>> _playerGreenZones = [];
    private readonly Dictionary<int, Vector> _playerLastPosition = [];
    private readonly Dictionary<int, Vector> _playerLastVelocity = [];
}