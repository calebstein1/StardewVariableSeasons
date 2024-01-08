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
                (0, 23),
                (2, 24),
                (7, 25),
                (12, 26),
                (22, 27),
                (37, 28),
                (52, 2),
                (62, 3),
                (67, 4),
                (72, 5),
                (74, 6),
                (75, 7)
            };

            foreach (var (upper, result) in map)
            {
                if (rndNum <= upper)
                    return result;
            }

            return 1;
        }
        
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayEnding += OnDayEnding;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            if (Game1.Date.DayOfMonth == 14)
            {
                var nextSeasonChange = new ModData
                {
                    NextSeasonChange = GenNextChangeDate()
                };
                
                Helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
            }
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            if (Game1.Date.DayOfMonth == Helper.Data.ReadSaveData<ModData>("next-season-change").NextSeasonChange)
            {
                Monitor.Log("Change to next season", LogLevel.Debug);
            }
        }
    }
}