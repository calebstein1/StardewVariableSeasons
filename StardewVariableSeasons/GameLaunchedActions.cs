using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace StardewVariableSeasons
{
    internal record ModConfig
    {
        internal Season Season;
        internal Season SeasonByDay;
    }
    public static class GameLaunchedActions
    {
        private static void UpdateState(ModConfig config, IModHelper helper)
        {
            Game1.season = config.Season;
            ModEntry.SeasonByDay = config.SeasonByDay;
            Game1.setGraphicsForSeason();
            Game1.netWorldState.Value.UpdateFromGame1();
            
            var seasonByDay = new ModData
            {
                SeasonByDay = ModEntry.SeasonByDay
            };
                
            helper.Data.WriteSaveData("season-by-day", seasonByDay);
        }
        public static void OnGameLaunched(IMonitor monitor, IModHelper helper, IManifest manifest, object sender, GameLaunchedEventArgs e)
        {
            var configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null) return;

            var config = new ModConfig
            {
                Season = Game1.season,
                SeasonByDay = ModEntry.SeasonByDay
            };
            configMenu.Register(
                mod: manifest,
                reset: () => config = new ModConfig
                {
                    Season = Game1.season,
                    SeasonByDay = ModEntry.SeasonByDay
                },
                save: () => UpdateState(config, helper)
            );
            
            configMenu.AddParagraph(
                mod: manifest,
                text: () => "These options can be used to correct the in-game and calendar seasons should they become incorrect for any reason"
            );

            configMenu.AddTextOption(
                mod: manifest,
                name: () => "In-Game Season",
                getValue: () => Game1.season.ToString(),
                setValue: value => config.Season = SeasonUtils.StrToSeason(value.ToLower()),
                allowedValues: new[] { "Spring", "Summer", "Fall", "Winter" }
            );

            configMenu.AddTextOption(
                mod: manifest,
                name: () => "Calendar Season",
                getValue: () => ModEntry.SeasonByDay.ToString(),
                setValue: value => config.SeasonByDay = SeasonUtils.StrToSeason(value.ToLower()),
                allowedValues: new[] { "Spring", "Summer", "Fall", "Winter" }
            );
        }
    }
}