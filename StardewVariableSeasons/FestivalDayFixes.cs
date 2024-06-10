using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
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
            Game1.Date.Season = ModEntry.SeasonByDay;
            Game1.Date.SeasonKey = ModEntry.SeasonByDay.ToString().ToLower();
        }
        
        public static void ResetSeasonPostfix(Season __state)
        {
            Game1.season = __state;
            Game1.currentSeason = Game1.season.ToString().ToLower();
            Game1.Date.Season = Game1.season;
            Game1.Date.SeasonKey = Game1.season.ToString().ToLower();
        }

        internal static IEnumerable<CodeInstruction> SeasonTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();

            foreach (var code in codes.Where(code => (code.operand as FieldInfo)?.Name is nameof(Game1.season)))
            {
                code.operand = CodeInstruction.LoadField(typeof(ModEntry), "_seasonByDay").operand;
            }

            return codes.AsEnumerable();
        }
    }
}