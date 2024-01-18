namespace StardewVariableSeasons
{
    public static class NPCBirthdayFixes
    {
        public static void Prefix(ref string season)
        {
            season = ModEntry.SeasonByDay;
        }
    }
}