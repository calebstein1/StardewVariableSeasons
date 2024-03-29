using System;
using System.Runtime.Loader;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StardewVariableSeasons
{
    internal sealed class ModEntry : Mod
    {
        public static int ChangeDate;
        public static int CropSurvivalCounter;
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

            harmony.Patch(
                original: AccessTools.Method(typeof(Utility), "isFestivalDay"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.IsFestPrefix))
            );

            harmony.Patch(
                original: AccessTools.Method(typeof(Event), "tryToLoadFestival"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.LoadFestPrefix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "performTenMinuteClockUpdate"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "warpFarmer",
                    new[] { typeof(LocationRequest), typeof(int), typeof(int), typeof(int) }),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), "AreStoresClosedForFestival"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Utility), "getStartTimeOfFestival"),
                transpiler: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ReplaceCurrentSeasonTranspiler))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(NPC), "isBirthday"),
                prefix: new HarmonyMethod(typeof(NPCBirthdayFixes), nameof(NPCBirthdayFixes.Prefix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(NPC), "receiveGift"),
                transpiler: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ReplaceCurrentSeasonTranspiler))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), "Kill"),
                prefix: new HarmonyMethod(typeof(CropDeathRandomizer), nameof(CropDeathRandomizer.Prefix))
            );

            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Locations.SeedShop), "addStock"),
                prefix: new HarmonyMethod(typeof(ShopStockPatches), nameof(ShopStockPatches.Prefix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Menus.Billboard), "draw", new [] { typeof(SpriteBatch) }),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Constructor(typeof(StardewValley.Menus.Billboard), new [] { typeof(bool) }),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            helper.Events.GameLoop.DayEnding += (sender, e) => DayEndingActions.OnDayEnding(Monitor, Helper, sender, e);
            helper.Events.GameLoop.SaveLoaded +=
                (sender, e) => SaveLoadedActions.OnSaveLoaded(Monitor, Helper, sender, e);
        }
    }
}