using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class SaveLoadedActions
    {
        private static ModData _nextSeasonChange;
        private static ModData _seasonByDay;
        private static ModData _cropSurvivalCounter;
        
        public static void OnSaveLoaded(IMonitor monitor, IModHelper helper, object sender, SaveLoadedEventArgs e)
        {
            try
            {
                _nextSeasonChange = helper.Data.ReadSaveData<ModData>("next-season-change");
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
                _seasonByDay = helper.Data.ReadSaveData<ModData>("season-by-day");
                ModEntry.SeasonByDay = _seasonByDay.SeasonByDay;
            }
            catch
            {
                ModEntry.SeasonByDay = Season.Spring;
                var seasonByDay = new ModData
                {
                    SeasonByDay = ModEntry.SeasonByDay
                };
                    
                helper.Data.WriteSaveData("season-by-day", seasonByDay);
            }
            
            try
            {
                _cropSurvivalCounter = helper.Data.ReadSaveData<ModData>("crop-survival-counter");
                ModEntry.CropSurvivalCounter = _cropSurvivalCounter.CropSurvivalCounter;
            }
            catch
            {
                ModEntry.CropSurvivalCounter = 5;
                var cropSurvivalCounter = new ModData
                {
                    CropSurvivalCounter = ModEntry.CropSurvivalCounter
                };
                    
                helper.Data.WriteSaveData("crop-survival-counter", cropSurvivalCounter);
            }
        }
    }
}