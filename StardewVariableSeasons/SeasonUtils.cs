using System;
using System.Collections.Generic;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class SeasonUtils
    {
        public static int GenNextChangeDate()
        {
            var rnd = new Random();
            var rndNum = rnd.Next(100);

            var map = new List<(int, int)>
            {
                (0, 22),
                (2, 23),
                (7, 24),
                (12, 25),
                (22, 26),
                (37, 27),
                (52, 1),
                (62, 2),
                (67, 3),
                (72, 4),
                (74, 5),
                (75, 6)
            };

            foreach (var (upper, result) in map)
            {
                if (rndNum <= upper)
                    return result;
            }

            return 28;
        }
        
        public static Season GetNextSeason(Season season)
        {
            return season == Season.Winter ? Season.Spring : season + 1;
        }
        public static Season GetPrevSeason(Season season)
        {
            return season == Season.Spring ? Season.Winter : season - 1;
        }
    }
}