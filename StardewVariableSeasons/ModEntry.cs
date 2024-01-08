using System;
using System.Collections.Generic;
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
            helper.Events.GameLoop.DayEnding += OnDayEnding;
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            var Season = new Seasons();
            var changeDate = Helper.Data.ReadSaveData<ModData>("next-season-change").NextSeasonChange;
            
            Monitor.Log(Season.Next(), LogLevel.Debug);
            Monitor.Log(Season.Prev(), LogLevel.Debug);

            if (Game1.Date.DayOfMonth == 28)
                Game1.currentSeason = Season.Prev();
            
            Monitor.Log(Game1.Date.DayOfMonth.ToString(), LogLevel.Debug);
            if (Game1.Date.DayOfMonth == 14)
            {
                Monitor.Log("Drawing new date...", LogLevel.Debug);
                var nextSeasonChange = new ModData
                {
                    NextSeasonChange = GenNextChangeDate()
                };
                
                Helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
            }
            
            Monitor.Log(Game1.currentSeason, LogLevel.Debug);
            Monitor.Log(changeDate.ToString(), LogLevel.Debug);

            if (Game1.Date.DayOfMonth == changeDate)
            {
                Monitor.Log("Change to next season", LogLevel.Debug);
                Game1.currentSeason = Season.Next();
                Game1.setGraphicsForSeason();
            }
        }
    }
}