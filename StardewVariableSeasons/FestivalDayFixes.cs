using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class FestivalDayFixes
    {
        public static void IsFestPrefix(ref string season)
        {
            season = ModEntry.SeasonByDay;
        }

        public static void LoadFestPrefix(ref string festival)
        {
            festival = $"{ModEntry.SeasonByDay}{Game1.dayOfMonth}";
        }

        public static void ResetSeasonPrefix(out string __state)
        {
            __state = Game1.currentSeason;
            Game1.currentSeason = ModEntry.SeasonByDay;
        }
        
        public static void ResetSeasonPostfix(string __state)
        {
            Game1.currentSeason = __state;
        }
        
        public static IEnumerable<CodeInstruction> ReplaceCurrentSeasonTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            
            foreach (var code in codes.Where(code =>
                         code.opcode == OpCodes.Ldsfld &&
                         code.operand.ToString().Contains("Game1::currentSeason")))
            {
                code.operand = typeof(ModEntry).GetField("SeasonByDay", BindingFlags.Static | BindingFlags.Public);
            }

            return codes.AsEnumerable();
        }
    }
}