using Celeste.Mod.VortexHelper.Entities;

namespace Celeste.Mod.VortexHelper;

public class VortexHelperSession : EverestModuleSession
{
    public enum SwitchBlockColor
    {
        Blue, Rose, Orange, Lime
    }

    public SwitchBlockColor SessionSwitchBlockColor { get; set; } = SwitchBlockColor.Blue;

    internal bool BoosterQoL { get; set; } = false;
    internal bool BoosterLegacyGravityHelper { get; set; } = false;
}
