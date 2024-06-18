using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;

namespace StardewVariableSeasons
{
    internal sealed class ModEntry : Mod
    {
        public static int ChangeDate { get; set; }
        public static int CropSurvivalCounter { get; set; }
        public static Season SeasonByDay;
        public static string CurrentSeason => Utility.getSeasonKey(SeasonByDay);
        public static int SeasonIndex => (int)SeasonByDay;

        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(ModManifest.UniqueID);
            
            harmony.Patch(
                original: AccessTools.Method(typeof(TV), "getWeatherForecast"),
                postfix: new HarmonyMethod(typeof(CustomWeatherChannelMessage), nameof(CustomWeatherChannelMessage.Postfix))
            );

            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "_newDayAfterFade"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "UpdateWeatherForNewDay"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );

            harmony.Patch(
                original: AccessTools.Method(typeof(WorldDate), "Now"),
                transpiler: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.SeasonTranspiler))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Utility), "isFestivalDay"),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
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
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(NPC), "isBirthday"),
                transpiler: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.SeasonTranspiler))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), "IsInSeason", new [] { typeof(GameLocation) }),
                prefix: new HarmonyMethod(typeof(CropDeathRandomizer), nameof(CropDeathRandomizer.Prefix)),
                postfix: new HarmonyMethod(typeof(CropDeathRandomizer), nameof(CropDeathRandomizer.Postfix))
            );
            
            harmony.Patch(
                original: AccessTools.Method(typeof(Billboard), "draw", new [] { typeof(SpriteBatch) }),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );
            
            harmony.Patch(
                original: AccessTools.Constructor(typeof(Billboard), new [] { typeof(bool) }),
                prefix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPrefix)),
                postfix: new HarmonyMethod(typeof(FestivalDayFixes), nameof(FestivalDayFixes.ResetSeasonPostfix))
            );

            helper.Events.GameLoop.GameLaunched +=
                (sender, e) => GameLaunchedActions.OnGameLaunched(Monitor, Helper, ModManifest, sender, e);
            helper.Events.GameLoop.DayEnding += (sender, e) => DayEndingActions.OnDayEnding(Monitor, Helper, sender, e);
            helper.Events.GameLoop.SaveLoaded +=
                (sender, e) => SaveLoadedActions.OnSaveLoaded(Monitor, Helper, sender, e);
        }
    }
}