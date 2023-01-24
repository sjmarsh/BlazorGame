using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public partial class SceneryManager
    {
        IBrowserService browserService;
        
        public SceneryManager(IBrowserService browserService)
        {
            this.browserService = browserService;

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
            var browserDimensions = await browserService.GetDimensions();

            var groundHeight = Constants.DefaultGroundHeight;
            var groundWidth = Constants.DefaultGroundWidth;
            var roadWidth = Constants.DefaultRoadWidth;

            if (browserDimensions.IsMobileDevice)
            {
                groundHeight = browserDimensions.Height * Constants.BrowserGroundHeightPercentage;
                groundWidth = browserDimensions.Width;
                roadWidth = browserDimensions.Width * Constants.BrowserRoadWidthPercentage;
            }

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
