using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;

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
                2 => 50,
                3 => 75,
                4 => 99,
                _ => 100
            };

            return rndNum < survivalPercentage;
        }
    }
}