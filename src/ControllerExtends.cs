using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace Zones;

public static class ControllerExtends
{
    public static void Bounce(this CCSPlayerController? controller)
    {
        if (controller == null || !controller.IsValid || controller.PlayerPawn.Value == null)
            return;

        var vel = new Vector(controller.PlayerPawn.Value.AbsVelocity.X, controller.PlayerPawn.Value.AbsVelocity.Y, controller.PlayerPawn.Value.AbsVelocity.Z);
        var speed = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

        vel *= (-350 / (float)speed);
        vel.Z = vel.Z <= 0 ? 150 : Math.Min(vel.Z, 150);
        controller.PlayerPawn.Value.Teleport(controller.PlayerPawn.Value.AbsOrigin, controller.PlayerPawn.Value.EyeAngles, vel);
    }
}