using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class SaveLoadedActions
    {
        public static void OnSaveLoaded(IMonitor monitor, IModHelper helper, object sender, SaveLoadedEventArgs e)
        {
            ModEntry.StardewVariableSeasonsSaveData = helper.Data.ReadSaveData<ModData>("next-season-change");
            ModEntry.ChangeDate = ModEntry.StardewVariableSeasonsSaveData.NextSeasonChange;
            ModEntry.SeasonByDay = ModEntry.StardewVariableSeasonsSaveData.SeasonByDay;
        }
    }
}