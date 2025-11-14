#if DEBUG
using System.Linq;
using BTD_Mod_Helper.Api.Data;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Api.UI;
using UnityEngine.InputSystem.Utilities;

namespace FasterForward;

public class Speed : ModStartMenuEntry, IModSettings
{
    private static readonly ModSettingBool AddStartMenuEntry = new(false)
    {
        description =
            "Adds an entry to the Mod Helper in game menu for changing the fast forward speed without hotkeys"
    };

    public override string Icon => GetTextureGUID("Icon");
    public override float IconScale => 1.2f;

    public override bool DontAddToStartMenu => !AddStartMenuEntry;
}

public abstract class SpeedEntry : ModStartMenuEntry<Speed>
{
    public abstract int Speed { get; }

    public override string DisplayName => $"{Speed:N0}x";

    public override void StartMenuEntryClicked() => FasterForwardMod.SetSpeed(Speed);

    public override bool IsSelected() => NumberHelpers.Approximately(TimeHelper.OverrideFastForwardTimeScale, Speed);
}

public class Speed1 : SpeedEntry
{
    public override int Speed => FasterForwardMod.Speed1;
}

public class Speed2 : SpeedEntry
{
    public override int Speed => FasterForwardMod.Speed2;
}

public class Speed3 : SpeedEntry
{
    public override int Speed => FasterForwardMod.Speed3;
}

public class Speed4 : SpeedEntry
{
    public override int Speed => FasterForwardMod.Speed4;
}

public class Custom : ModStartMenuEntry<Speed>
{
    public override void StartMenuEntryClicked() => FasterForwardMod.SetCustomSpeed();

    public override bool IsSelected() => GetContent<SpeedEntry>().All(entry => !entry.IsSelected());
}
#endif