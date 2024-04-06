using StardewValley;

namespace StardewVariableSeasons
{
    public static class FestivalDayFixes
    {
        public static void LoadFestPrefix(ref string festival)
        {
            festival = $"{ModEntry.SeasonByDay.ToString().ToLower()}{Game1.dayOfMonth}";
        }

        public static void ResetSeasonPrefix(out Season __state)
        {
            __state = Game1.season;
            Game1.season = ModEntry.SeasonByDay;
            Game1.currentSeason = ModEntry.SeasonByDay.ToString().ToLower();
        }
        
        public static void ResetSeasonPostfix(Season __state)
        {
            Game1.season = __state;
            Game1.currentSeason = Game1.season.ToString().ToLower();
        }
    }
}