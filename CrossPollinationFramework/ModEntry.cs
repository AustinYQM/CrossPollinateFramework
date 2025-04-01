using CrossPollinationFramework.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Minigames;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace CrossPollinationFramework
{ 

    internal sealed class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.DayEnding += this.OnDayEnding;
        }

        public HybridCropData? ExtractHybridCropData(Crop crop)
        {
            var cropData = crop.GetData();
            if (cropData is not null && cropData.CustomFields is not null
                                     && cropData.CustomFields.TryGetValue("BaseCropA", out var baseCropA) &&
                                     cropData.CustomFields.TryGetValue("BaseCropB", out var baseCropB)
               )
                
            {
                var childStartingPhase = cropData.CustomFields.GetValueOrDefault("ChildStartingPhase", "0");
                var baseCropAReqPhase = cropData.CustomFields.GetValueOrDefault("BaseCropAReqPhase", "-1");
                var baseCropBReqPhase = cropData.CustomFields.GetValueOrDefault("BaseCropBReqPhase", "-1");
                var boostedSeasons = cropData.CustomFields.GetValueOrDefault("BoostedSeasons", "").ToLower();
                var crossBreedChance = cropData.CustomFields.GetValueOrDefault("CrossBreedChance", "0.05");
                var seasonalBoostAmount = cropData.CustomFields.GetValueOrDefault("SeasonalBoostAmount", "2.0");
                
                var boostedSeasonsList = boostedSeasons.Split(',').ToList();
                var crossBreedChanceD = Convert.ToDouble(crossBreedChance);
                var seasonalBoostAmountD = Convert.ToDouble(seasonalBoostAmount);
                var baseCropAReqPhaseI = Convert.ToInt32(baseCropAReqPhase);
                var baseCropBReqPhaseI = Convert.ToInt32(baseCropBReqPhase);

                var boostedSeasonsEnumList = boostedSeasonsList.ConvertAll(season =>
                {
                    return season switch
                    {
                        "summer" => Season.Summer,
                        "spring" => Season.Spring,
                        "fall" => Season.Fall,
                        "winter" => Season.Winter,
                        _ => throw new Exception($"Unknown season {season}"),
                    };
                });

                return new HybridCropData(
                    baseCropA,
                    baseCropB,
                    baseCropAReqPhaseI,
                    baseCropBReqPhaseI,
                    childStartingPhase,
                    crossBreedChanceD,
                    boostedSeasonsEnumList,
                    seasonalBoostAmountD
                );
            }
            else
            {
                return null;
            }
        }

        

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            
            Utility.ForEachCrop(crop =>
            {
                if (crop.forageCrop.Value)
                    return false;
                
                this.Monitor.Log($"Location: {crop.Dirt.Location.DisplayName} X: {crop.Dirt.Tile.X}, Y: {crop.Dirt.Tile.Y}", LogLevel.Debug);
                return true;
            });

        }
        
        private void OnDayEnding(object? sender, DayEndingEventArgs e)
        {
            if (!Game1.IsMasterGame)
                return;

            Utility.ForEachCrop(crop =>
            {
                if (crop.forageCrop.Value)
                    return false;
                
                this.Monitor.Log($"Location: {crop.Dirt.Location.DisplayName} X: {crop.Dirt.Tile.X}, Y: {crop.Dirt.Tile.Y}", LogLevel.Debug);
                return true;
            });
        }

        private bool CheckNeighbors(GameLocation gameLocation, Vector2 tile)
        {

            Vector2 northernNeighbor = tile - Vector2.UnitY;
            Vector2 southernNeighbor = tile + Vector2.UnitY;
            Vector2 westernNeighbor = tile - Vector2.UnitX;
            Vector2 easternNeighbor = tile + Vector2.UnitX;


            return true;
        }
    }
}

