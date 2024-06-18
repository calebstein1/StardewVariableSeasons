using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
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

            return 0;
        }
        
        public static Season GetNextSeason(Season season)
        {
            return season == Season.Winter ? Season.Spring : season + 1;
        }

        public static Season StrToSeason(string season)
        {
            return season switch
            {
                "summer" => Season.Summer,
                "fall" => Season.Fall,
                "winter" => Season.Winter,
                _ => Season.Spring
            };
        }

        internal static IEnumerable<CodeInstruction> SeasonForSaveTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call && (codes[i].operand as MethodInfo)?.Name is "get_seasonIndex")
                {
                    codes[i] = CodeInstruction.Call(typeof(ModEntry), "get_SeasonIndex");
                }
            }

            return codes.AsEnumerable();
        }
    }
}