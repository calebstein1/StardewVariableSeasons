using System;
using System.Runtime.Loader;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StardewVariableSeasons
{
    internal sealed class ModEntry : Mod
    {
        public static int ChangeDate;
        public static string SeasonByDay;
        
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(ModManifest.UniqueID);
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "_newDayAfterFade"),
                transpiler: new HarmonyMethod(typeof(NewDayAfterFadeTranspiler), nameof(NewDayAfterFadeTranspiler.Transpiler))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Objects.TV), "getWeatherForecast"),
                postfix: new HarmonyMethod(typeof(CustomWeatherChannelMessage), nameof(CustomWeatherChannelMessage.Postfix))
            );
            
            helper.Events.GameLoop.DayEnding += (sender, e) => DayEndingActions.OnDayEnding(Monitor, Helper, sender, e);
            helper.Events.GameLoop.SaveLoaded +=
                (sender, e) => SaveLoadedActions.OnSaveLoaded(Monitor, Helper, sender, e);
        }
    }
}