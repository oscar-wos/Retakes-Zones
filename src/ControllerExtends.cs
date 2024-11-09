using CounterStrikeSharp.API.Core;

namespace Zones;

public static class ControllerExtends
{
    public static bool IsValid(this CCSPlayerController? controller, bool checkBot = false)
    {
        if (checkBot)
            return controller is { IsValid: true, IsBot: false };

        return controller is { IsValid: true };
    }

    public static void Bounce(this CCSPlayerController? controller)
    {
        if (controller == null || !controller.IsValid() || controller.PlayerPawn.Value == null)
            return;

        var vel = controller.PlayerPawn.Value.AbsVelocity;
        var speed = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

        vel *= (-350 / (float)speed);
        vel.Z = vel.Z <= 0 ? 150 : Math.Min(vel.Z, 150);
        controller.PlayerPawn.Value.Teleport(controller.PlayerPawn.Value.AbsOrigin, controller.PlayerPawn.Value.EyeAngles, vel);
    }
}