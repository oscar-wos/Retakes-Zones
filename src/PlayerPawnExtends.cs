using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace Zones;

public static class PlayerPawnExtends
{
    public static void Bounce(this CCSPlayerPawn pawn)
    {
        var vel = new Vector(pawn.AbsVelocity.X, pawn.AbsVelocity.Y, pawn.AbsVelocity.Z);
        var speed = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

        vel *= (-350 / (float)speed);
        vel.Z = vel.Z <= 0 ? 150 : Math.Min(vel.Z, 150);
        pawn.Teleport(pawn.AbsOrigin, pawn.EyeAngles, vel);
    }
}