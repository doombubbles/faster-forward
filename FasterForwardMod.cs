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
    private static readonly ModSettingHotkey Speed3 = new(KeyCode.F1)
    {
        displayName = "3x Speed (default) Hotkey"
    };

    private static readonly ModSettingHotkey Speed5 = new(KeyCode.F2)
    {
        displayName = "5x Speed Hotkey"
    };

    private static readonly ModSettingHotkey Speed10 = new(KeyCode.F3)
    {
        displayName = "10x Speed Hotkey"
    };

    private static readonly ModSettingHotkey Speed25 = new(KeyCode.F4)
    {
        displayName = "25x Speed Hotkey"
    };

    private static readonly ModSettingHotkey SpeedCustom = new(KeyCode.F5)
    {
        displayName = "Custom Speed Hotkey"
    };

    public override void OnUpdate()
    {
        if (Speed3.JustPressed())
        {
            SetSpeed(3);
        }

        if (Speed5.JustPressed())
        {
            SetSpeed(5);
        }

        if (Speed10.JustPressed())
        {
            SetSpeed(10);
        }

        if (Speed25.JustPressed())
        {
            SetSpeed(25);
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