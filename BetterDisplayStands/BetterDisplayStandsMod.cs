using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetterDisplayStands
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInProcess("PlateUp.exe")]
    public class BetterDisplayStandsMod : BaseUnityPlugin
    {
        public const string pluginGuid = "sentheegg.plateup.betterdisplaystands";
        public const string pluginName = "Better Display Stands";
        public const string pluginVersion = "1.0";

        public void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), pluginGuid);

            ConfigData.EnableMod = Config.Bind("General", "EnableMod", true, "Whether the mod is enabled or not.");
            ConfigData.DisplayStandEffectiveness = Config.Bind("General", "DisplayStandEffectiveness", 1f, "From 0 to 1, how effective you'd like your display stands to be. 0.8 is the default value.");
        }
    }
}
