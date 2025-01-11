using System;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using FasterForward;
using Il2CppAssets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[assembly: MelonInfo(typeof(FasterForwardMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace FasterForward;

public class FasterForwardMod : BloonsTD6Mod
{
    private static readonly ModSettingHotkey Hotkey1 = new(KeyCode.F1)
    {
        displayName = "First Speed Hotkey"
    };

    private static readonly ModSettingInt Speed1 = new ModSettingInt(3)
    {
        displayName = "Speed"
    };

    private static readonly ModSettingHotkey Hotkey2 = new(KeyCode.F2)
    {
        displayName = "Second Speed Hotkey"
    };

    private static readonly ModSettingInt Speed2 = new ModSettingInt(5)
    {
        displayName = "Speed"
    };

    private static readonly ModSettingHotkey Hotkey3 = new(KeyCode.F3)
    {
        displayName = "Third Speed Hotkey"
    };

    private static readonly ModSettingInt Speed3 = new ModSettingInt(10)
    {
        displayName = "Speed"
    };

    private static readonly ModSettingHotkey Hotkey4 = new(KeyCode.F4)
    {
        displayName = "Fourth Speed Hotkey"
    };

    private static readonly ModSettingInt Speed4 = new ModSettingInt(25)
    {
        displayName = "Speed"
    };

    private static readonly ModSettingHotkey SpeedCustom = new(KeyCode.F5)
    {
        displayName = "Custom Speed Hotkey"
    };

    public override void OnUpdate()
    {
        if (Hotkey1.JustPressed())
        {
            SetSpeed(Speed1);
        }

        if (Hotkey2.JustPressed())
        {
            SetSpeed(Speed2);
        }

        if (Hotkey3.JustPressed())
        {
            SetSpeed(Speed3);
        }

        if (Hotkey4.JustPressed())
        {
            SetSpeed(Speed4);
        }

        if (SpeedCustom.JustPressed() && InGame.instance != null)
        {
            PopupScreen.instance.ShowSetValuePopup("Custom Fast Forward Speed",
                "Sets the Fast Forward speed to the specified value",
                new Action<int>(s => SetSpeed(Mathf.Clamp(s, 1, 100))), (int) TimeHelper.OverrideFastForwardTimeScale);
        }
    }

    private static void SetSpeed(double speed)
    {
        if (NumberHelpers.Approximately(TimeHelper.OverrideFastForwardTimeScale, speed)) return;

        TimeHelper.OverrideFastForwardTimeScale = speed;
        TimeHelper.OverrideMaxSimulationStepsPerUpdate = speed;

        var isDefault = NumberHelpers.Approximately(speed, Constants.fastForwardTimeScaleMultiplier);

        var message = $"Fast Forward Speed is now {speed}x{(isDefault ? " (Default)" : "")}";

        if (InGame.instance == null || InGame.Bridge == null)
        {
            ModHelper.Msg<FasterForwardMod>(message);
        }
        else
        {
            Game.instance.ShowMessage(message, 1f);
        }
    }
}
