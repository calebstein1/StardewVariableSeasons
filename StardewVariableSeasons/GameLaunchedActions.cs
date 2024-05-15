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
        private static void UpdateState(ModConfig config)
        {
            Game1.season = config.Season;
            ModEntry.SeasonByDay = config.SeasonByDay;
        }
        public static void OnGameLaunched(IMonitor monitor, IModHelper helper, IManifest manifest, object sender, GameLaunchedEventArgs e)
        {
            var configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null) return;

            var Config = new ModConfig
            {
                Season = Game1.season,
                SeasonByDay = ModEntry.SeasonByDay
            };
            configMenu.Register(
                mod: manifest,
                reset: () => Config = new ModConfig
                {
                    Season = Game1.season,
                    SeasonByDay = ModEntry.SeasonByDay
                },
                save: () => UpdateState(Config)
            );
            
            configMenu.AddTextOption(
                mod: manifest,
                name: () => "In-Game Season",
                getValue: () => Config.Season.ToString(),
                setValue: value => Config.Season = value switch
                {
                    "Spring" => Season.Spring,
                    "Summer" => Season.Summer,
                    "Fall" => Season.Fall,
                    "Winter" => Season.Winter
                },
                allowedValues: new[] { "Spring", "Summer", "Fall", "Winter" }
            );

            configMenu.AddTextOption(
                mod: manifest,
                name: () => "Calendar Season",
                getValue: () => Config.SeasonByDay.ToString(),
                setValue: value => Config.SeasonByDay = value switch
                {
                    "Spring" => Season.Spring,
                    "Summer" => Season.Summer,
                    "Fall" => Season.Fall,
                    "Winter" => Season.Winter
                },
                allowedValues: new[] { "Spring", "Summer", "Fall", "Winter" }
            );
        }
    }
}