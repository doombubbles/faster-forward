using System;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using Il2CppAssets.Scripts.Utils;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using FasterForward;
using UnityEngine;
using UnityEngine.PlayerLoop;

[assembly: MelonInfo(typeof(FasterForwardMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace FasterForward;

public class FasterForwardMod : BloonsTD6Mod
{
    private static int speed = 3;

    private static readonly ModSettingHotkey Speed3 = new(KeyCode.I)
    {
        displayName = "3x Speed (default) Hotkey"
    };

    private static readonly ModSettingHotkey Speed5 = new(KeyCode.O)
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

    private static readonly ModSettingHotkey SpeedCustom = new(KeyCode.P)
    {
        displayName = "Custom Speed Hotkey"
    };

    public override void OnUpdate()
    {
        var lastSpeed = speed;
        if (Speed3.JustPressed())
        {
            speed = 3;
        }

        if (Speed5.JustPressed())
        {
            speed = 5;
        }

        if (Speed10.JustPressed())
        {
            speed = 10;
        }

        if (Speed25.JustPressed())
        {
            speed = 25;
        }

        if (InGame.instance == null) return;

        UpdateInGame();

        if (speed != lastSpeed)
        {
            Game.instance.ShowMessage("Fast Forward Speed is now " + speed + "x" + (speed == 3 ? " (Default)" : ""),
                1f);
        }
    }

    public static void UpdateInGame()
    {
        if (SpeedCustom.JustPressed())
        {
            PopupScreen.instance.ShowSetValuePopup("Custom Fast Forward Speed",
                "Sets the Fast Forward speed to the specified value",
                new Action<int>(i =>
                {
                    if (i < 1)
                    {
                        i = 1;
                    }

                    if (i > 100)
                    {
                        i = 100;
                    }

                    speed = i;
                    Game.instance.ShowMessage(
                        "Fast Forward Speed is now " + speed + "x" + (speed == 3 ? " (Default)" : ""), 1f);
                }), speed);
        }

        if (TimeManager.FastForwardActive)
        {
            TimeManager.timeScaleWithoutNetwork = speed;
            TimeManager.networkScale = speed;
        }
        else
        {
            TimeManager.timeScaleWithoutNetwork = 1;
            TimeManager.networkScale = 1;
        }

        TimeManager.maxSimulationStepsPerUpdate = speed;
    }
}
