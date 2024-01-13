using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class SaveLoadedActions
    {
        private static ModData _nextSeasonChange;
        private static ModData _seasonByDay;
        
        public static void OnSaveLoaded(IMonitor monitor, IModHelper helper, object sender, SaveLoadedEventArgs e)
        {
            _nextSeasonChange = helper.Data.ReadSaveData<ModData>("next-season-change");
            _seasonByDay = helper.Data.ReadSaveData<ModData>("season-by-day");
            
            try
            {
                ModEntry.ChangeDate = _nextSeasonChange.NextSeasonChange;
            }
            catch
            {
                var nextSeasonChange = new ModData
                {
                    NextSeasonChange = 28
                };
            
                helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
            }
            
            try
            {
                ModEntry.SeasonByDay = _seasonByDay.SeasonByDay;
            }
            catch
            {
                ModEntry.SeasonByDay = "spring";
                var seasonByDay = new ModData
                {
                    SeasonByDay = ModEntry.SeasonByDay
                };
                    
                helper.Data.WriteSaveData("season-by-day", seasonByDay);
            }
        }
    }
}