using StardewValley;

namespace StardewVariableSeasons
{
    public sealed class ModData
    {
        public int NextSeasonChange { get; init; }
        public int CropSurvivalCounter { get; init; }
        public Season SeasonByDay { get; init; }
    }
    
    public sealed class ModDataLegacy
    {
        public int NextSeasonChange { get; init; }
        public int CropSurvivalCounter { get; init; }
        public string SeasonByDay { get; init; }
    }
}