using CounterStrikeSharp.API.Core.Capabilities;
using RetakesPluginShared;
using RetakesPluginShared.Enums;

namespace Zones;

public partial class Zones
{
    private const int MAX_PLAYERS = 64;

    public override string ModuleName => "Retakes-Zones";
    public override string ModuleVersion => "1.2.0";
    public override string ModuleAuthor => "https://github.com/oscar-wos/Retakes-Zones";

    private static readonly PluginCapability<IRetakesPluginEventSender> _retakesEventSenderCapability =
        new("retakes_plugin:event_sender");

    private static readonly PlayerData?[] _playerData = new PlayerData?[MAX_PLAYERS];

    private static readonly List<Zone>[] _zones = new List<Zone>[
        Enum.GetValues(typeof(Bombsite)).Length
    ];

    static Zones()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Bombsite)).Length; i++)
        {
            _zones[i] = [];
        }
    }
}
