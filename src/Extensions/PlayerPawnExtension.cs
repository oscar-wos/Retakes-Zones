using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace Zones.Extensions;

public static class PlayerPawnExtension
{
    public static void Bounce(this CCSPlayerPawn playerPawn)
    {
        Vector vel = new(
            playerPawn.AbsVelocity.X,
            playerPawn.AbsVelocity.Y,
            playerPawn.AbsVelocity.Z
        );

        double speed = Math.Sqrt((vel.X * vel.X) + (vel.Y * vel.Y));

        vel *= -350 / (float)speed;
        vel.Z = vel.Z <= 0 ? 150 : Math.Min(vel.Z, 150);
        playerPawn.Teleport(playerPawn.AbsOrigin, playerPawn.EyeAngles, vel);
    }
}
