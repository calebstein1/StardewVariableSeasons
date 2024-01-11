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
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "_newDayAfterFade"),
                transpiler: new HarmonyMethod(typeof(NewDayAfterFadeTranspiler), nameof(NewDayAfterFadeTranspiler.Transpiler))
            );
            helper.Events.GameLoop.DayEnding += (sender, e) => DayEndingActions.OnDayEnding(Monitor, Helper, sender, e);
        }
    }
}