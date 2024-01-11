using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using StardewModdingAPI;
using StardewValley;

namespace StardewVariableSeasons
{
    [HarmonyPatch(typeof(Game1), "_newDayAfterFade")]
    public static class NewDayAfterFadeTranspiler
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            
            foreach (var code in codes)
            {
                if (code.opcode == OpCodes.Call && code.operand.ToString().Contains("StardewValley.Game1::newSeason()"))
                {
                    code.opcode = OpCodes.Nop;
                }
            }

            return codes.AsEnumerable();
        }
    }
}