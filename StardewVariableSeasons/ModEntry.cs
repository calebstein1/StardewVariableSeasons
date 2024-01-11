using System;
using System.Collections.Generic;
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
        private static int GenNextChangeDate()
        {
            var rnd = new Random();
            var rndNum = rnd.Next(100);

            var map = new List<(int, int)>
            {
                (0, 22),
                (2, 23),
                (7, 24),
                (12, 25),
                (22, 26),
                (37, 27),
                (52, 1),
                (62, 2),
                (67, 3),
                (72, 4),
                (74, 5),
                (75, 6)
            };

            foreach (var (upper, result) in map)
            {
                if (rndNum <= upper)
                    return result;
            }

            return 28;
        }

        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), "_newDayAfterFade"),
                transpiler: new HarmonyMethod(typeof(NewDayAfterFadeTranspiler), nameof(NewDayAfterFadeTranspiler.Transpiler))
            );
            helper.Events.GameLoop.DayEnding += OnDayEnding;
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            var season = new Seasons();
            var changeDate = Helper.Data.ReadSaveData<ModData>("next-season-change").NextSeasonChange;
            
            Monitor.Log(season.Next(), LogLevel.Debug);
            Monitor.Log(season.Prev(), LogLevel.Debug);

            Monitor.Log(Game1.Date.DayOfMonth.ToString(), LogLevel.Debug);
            if (Game1.dayOfMonth == 14)
            {
                Monitor.Log("Drawing new date...", LogLevel.Debug);
                var nextSeasonChange = new ModData
                {
                    NextSeasonChange = GenNextChangeDate()
                };
                
                Helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
            }

            if (Game1.dayOfMonth == 28)
                Game1.dayOfMonth = 0;
            
            Monitor.Log(Game1.currentSeason, LogLevel.Debug);
            Monitor.Log(changeDate.ToString(), LogLevel.Debug);

            if (Game1.Date.DayOfMonth == changeDate)
            {
                Monitor.Log("Change to next season", LogLevel.Debug);
                if (season.Next() == "spring")
                {
                    Game1.year++;
                    if (Game1.year == 2)
                        Game1.addKentIfNecessary();
                }

                Game1.currentSeason = season.Next();
                Game1.setGraphicsForSeason();
            }
        }
    }
}