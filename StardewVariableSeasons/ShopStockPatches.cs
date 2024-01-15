using StardewValley;

namespace StardewVariableSeasons
{
    public static class ShopStockPatches
    {
        public static void Prefix(ref string item_season)
        {
            var season = new Seasons();
            var nextSeason = season.Next(Game1.currentSeason);

            if (item_season == nextSeason)
                item_season = Game1.currentSeason;
        }
    }
}