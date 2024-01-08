using System.Collections.Generic;
using StardewValley;

namespace StardewVariableSeasons
{
    public sealed class Seasons
    {
        private static int GetSeasonId(string seasonName)
        {
            var map = new List<(string, int)>
            {
                ("spring", 0),
                ("summer", 1),
                ("fall", 2),
                ("winter", 3)
            };

            foreach (var (season, num) in map)
            {
                if (seasonName == season)
                    return num;
            }

            return GetSeasonId(Game1.currentSeason);
        }
        
        private static string GetSeasonById(int seasonId)
        {
            if (seasonId > 3)
                seasonId = 0;
            else if (seasonId < 0)
                seasonId = 3;
                    
            var map = new List<(int, string)>
            {
                (0, "spring"),
                (1, "summer"),
                (2, "fall"),
                (3, "winter")
            };

            foreach (var (num, season) in map)
            {
                if (seasonId == num)
                    return season;
            }

            return Game1.currentSeason;
        }

        public string Next()
        {
            return GetSeasonById(GetSeasonId(Game1.currentSeason) + 1);
        }
        public string Prev()
        {
            return GetSeasonById(GetSeasonId(Game1.currentSeason) - 1);
        }
    }
}