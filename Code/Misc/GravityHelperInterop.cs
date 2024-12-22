using MonoMod.ModInterop;
using System;

namespace Celeste.Mod.VortexHelper.Misc;

internal static class GravityHelperInterop
{
    [ModImportName("GravityHelper")]
    internal static class Imports
    {
        public static Func<bool> IsPlayerInverted;
    }

    public static bool IsPlayerInverted() => Imports.IsPlayerInverted?.Invoke() ?? false;
}