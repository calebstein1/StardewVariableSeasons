using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using StardewValley;

namespace StardewVariableSeasons
{
    public static class SvsPatches
    {
        public static void LoadFestPrefix(ref string festival)
        {
            festival = $"{ModEntry.SeasonByDay.ToString().ToLower()}{Game1.dayOfMonth}";
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

        internal static IEnumerable<CodeInstruction> ExtractNewDayAfterFadeMethod(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            
            foreach (var code in codes.Where(code => code.opcode == OpCodes.Newobj))
            {
                ModEntry.NewDayAfterFadeMethod = code.operand as MemberInfo;
            }
            
            return codes.AsEnumerable();
        }
        
        internal static IEnumerable<CodeInstruction> ExtractTenMinuteMethod(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            
            foreach (var code in codes.Where(code => code.opcode == OpCodes.Ldftn))
            {
                ModEntry.TenMinuteMethod = code.operand as MethodInfo;
            }
            
            return codes.AsEnumerable();
        }
        
        internal static IEnumerable<CodeInstruction> ExtractGetBirthdaysMethod(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            
            foreach (var code in codes.Where(code => code.opcode == OpCodes.Ldftn))
            {
                ModEntry.GetBirthdaysMethod = code.operand as MethodInfo;
            }
            
            return codes.AsEnumerable();
        }
    }
}