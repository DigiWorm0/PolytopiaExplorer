using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityExplorer.Helpers;

namespace PolytopiaExplorer
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("Polytopia.exe")]
    public class PolytopiaPatch : BasePlugin
    {
        public const string NAME = "PolytopiaExplorer";
        public const string VERSION = "1.0.0";
        public const string AUTHOR = "DigiWorm";
        public const string GUID = "com.digiworm.polytopiaexplorer";

        public Harmony Harmony { get; } = new Harmony(GUID);
        public static ManualLogSource logger = new ManualLogSource("PolytopiaExplorer");

        public override void Load()
        {
            BepInEx.Logging.Logger.Sources.Add(logger);
            logger.Log(LogLevel.Info, "Loaded PolytopiaExplorer v1.0");
            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(ClientInteraction), nameof(ClientInteraction.UpdateMouseInput))]
        public static class UIPatch
        {
            public static bool Prefix()
            {
                if (UnityExplorer.UI.UIManager.ShowMenu)
                {
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(CameraController), nameof(CameraController.UpdateInput))]
        public static class CamPatch
        {
            public static bool Prefix()
            {
                if (UnityExplorer.UI.UIManager.ShowMenu)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
