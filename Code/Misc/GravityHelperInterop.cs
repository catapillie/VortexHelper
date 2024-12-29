using Microsoft.Xna.Framework;
using MonoMod.ModInterop;
using System;

namespace Celeste.Mod.VortexHelper.Misc;

internal static class GravityHelperInterop
{
    [ModImportName("GravityHelper")]
    internal static class Imports
    {
        public static Func<bool> IsPlayerInverted;
        public static Func<Actor, bool> IsActorInverted;
        public static Action BeginOverride;
        public static Action EndOverride;
    }

    public static bool IsPlayerInverted() => Imports.IsPlayerInverted?.Invoke() ?? false;
    public static bool IsActorInverted(Actor actor) => Imports.IsActorInverted?.Invoke(actor) ?? false;
    public static void BeginOverride() => Imports.BeginOverride?.Invoke();
    public static void EndOverride() => Imports.EndOverride?.Invoke();
    public static Vector2 InvertIfRequired(Vector2 v) => IsPlayerInverted() ? new Vector2(v.X, -v.Y) : v;
}