using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class DayEndingActions
    {
        public static void OnDayEnding(IMonitor monitor, IModHelper helper, object sender, DayEndingEventArgs e)
        {
            ModEntry.ChangeDate = helper.Data.ReadSaveData<ModData>("next-season-change").NextSeasonChange;
            
            var season = new Seasons();
            var changeDate = ModEntry.ChangeDate;
            
            monitor.Log($"Next season is {season.Next()}", LogLevel.Debug);
            monitor.Log($"Previous season was {season.Prev()}", LogLevel.Debug);

            monitor.Log($"Current day is {Game1.Date.DayOfMonth.ToString()}", LogLevel.Debug);
            switch (Game1.dayOfMonth)
            {
                case 14:
                {
                    monitor.Log("Drawing new date...", LogLevel.Debug);
                    var nextSeasonChange = new ModData
                    {
                        NextSeasonChange = season.GenNextChangeDate()
                    };
                
                    helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
                    break;
                }
                case 28:
                    Game1.dayOfMonth = 0;
                    break;
            }

            monitor.Log($"Current season is {Game1.currentSeason}", LogLevel.Debug);
            monitor.Log($"Next season change on {changeDate.ToString()}", LogLevel.Debug);

            if (Game1.Date.DayOfMonth != changeDate) return;
            monitor.Log("Change to next season", LogLevel.Debug);
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