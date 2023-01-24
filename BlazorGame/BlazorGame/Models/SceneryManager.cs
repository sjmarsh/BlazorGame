using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public partial class SceneryManager
    {
        private readonly IGameDimensionService gameDimensionService;
        
        public SceneryManager(IGameDimensionService gameDimensionService)
        {
            this.gameDimensionService = gameDimensionService;

            Scenery = new List<SceneryModel>();
        }

        public IList<SceneryModel> Scenery { get; private set; }

        public StageType CurrentStageType { get; set; }

        public void Reset()
        {
            Scenery.Clear();
        }
                
        public async Task Animate()
        {
            await AnimateModels(Scenery);
        }

        private async Task AnimateModels(IList<SceneryModel> models)
        {
            var gameDimensions = await gameDimensionService.GetDimensions();

            var groundHeight = gameDimensions.GroundHeight;
            var groundWidth = gameDimensions.GroundWidth;
            var roadWidth = gameDimensions.RoadWidth;
            
            var newScenerySpawnHeight = groundHeight * 0.2;
            var sceneryDespawnHeight = groundHeight * 0.6;

            var leftItemSpawnXAxis = groundWidth * 0.25;
            var rightItemSpawnAxis = groundWidth * 0.70;
            var verticalMoveDistance = groundHeight * 0.025;
                                    
            if (!models.Any() || !models.Any(a => a.LeftItem.Top < newScenerySpawnHeight))
            {
                models.Add(new SceneryModel(CurrentStageType, leftItemSpawnXAxis, rightItemSpawnAxis, verticalMoveDistance, groundHeight, groundWidth));
            }

            foreach (var sceneryModel in models)
            {
                sceneryModel.Move();
            }

            var bottomSceneryItem = models.FirstOrDefault(a => a.LeftItem.Top > sceneryDespawnHeight);
            if (bottomSceneryItem != null)
            {
                models.Remove(bottomSceneryItem);
            }
        }
    }
}
