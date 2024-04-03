using System;

namespace StardewVariableSeasons
{
    public static class CropDeathRandomizer
    {
        public static bool Prefix()
        {
            var rnd = new Random();
            var rndNum = rnd.Next(100);
            
            var survivalPercentage = ModEntry.CropSurvivalCounter switch
            {
                0 => 1,
                1 => 25,
                2 => 33,
                3 => 50,
                4 => 75,
                _ => 100
            };

            return rndNum < survivalPercentage;
        }
    }
}