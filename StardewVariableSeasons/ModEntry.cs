using System;
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
            helper.Events.GameLoop.DayEnding += OnDayEnding;
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            
            Monitor.Log($"{Helper.Data.ReadSaveData<ModData>("next-season-change").NextSeasonChange}", LogLevel.Debug);

            if (Game1.Date.DayOfMonth == 14)
            {
                var nextSeasonChange = new ModData
                {
                    NextSeasonChange = 24
                };
                
                Helper.Data.WriteSaveData("next-season-change", nextSeasonChange);
            }
        }
    }
}