using StardewValley;

namespace StardewVariableSeasons
{
    public static class ShopStockPatches
    {
        public static void Prefix(ref Season item_season)
        {
            var nextSeason = SeasonUtils.GetNextSeason(Game1.season);

            if (item_season == nextSeason)
                item_season = Game1.season;
        }
    }
}