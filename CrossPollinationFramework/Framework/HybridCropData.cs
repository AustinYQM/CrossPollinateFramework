using StardewValley;

namespace CrossPollinationFramework.Framework
{
    internal sealed record HybridCropData(
        string BaseCropA,
        string BaseCropB,
        int BaseCropAReqPhase,
        int BaseCropBReqPhase,
        string ChildStartingPhase,
        double CrossChance,
        List<Season> BoostedSeasons,
        double SeasonalBoostAmount);
}