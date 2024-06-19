using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class Patches
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

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldsfld && (codes[i].operand as FieldInfo)?.Name is nameof(Game1.season))
                {
                    codes[i] = CodeInstruction.LoadField(typeof(ModEntry), nameof(ModEntry.SeasonByDay));
                }
                else if (codes[i].opcode == OpCodes.Call &&
                         (codes[i].operand as MethodInfo)?.Name is "get_currentSeason")
                {
                    codes[i] = CodeInstruction.Call(typeof(ModEntry), "get_CurrentSeason");
                }
                else if (codes[i].opcode == OpCodes.Call &&
                         (codes[i].operand as MethodInfo)?.Name is "get_seasonIndex")
                {
                    codes[i] = CodeInstruction.Call(typeof(ModEntry), "get_SeasonIndex");
                }
            }

            return codes.AsEnumerable();
        }
    }
}