using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public partial class SceneryManager
    {
        private const int NewScenerySpawnHeight = 60;
        private const int SceneryDespawnHeight = 180;
        private const int LeftItemPosition = 125;
        private const int RightItemPosition = 345;
        
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
            var newScenerySpawnHeight = NewScenerySpawnHeight;
            var sceneryDespawnHeight = SceneryDespawnHeight;
            var leftItemPosition = LeftItemPosition;
            var rightItemPosition = RightItemPosition;
                        
            if(browserDimensions.IsMobileDevice)
            {
                newScenerySpawnHeight = (int)browserDimensions.Height / 2;
                sceneryDespawnHeight = (int)browserDimensions.Height - 100;

                leftItemPosition = (int)(browserDimensions.Width * 0.28);
                rightItemPosition = (int)(browserDimensions.Width * 0.72);
            }

            if (!models.Any() || !models.Any(a => a.LeftItem.Top < newScenerySpawnHeight))
            {
                models.Add(new SceneryModel(CurrentStageType, leftItemPosition, rightItemPosition));
            }

            foreach (var sceneryModel in models)
            {
                sceneryModel.Move();
            }

            var bottomSceneryItem = models.FirstOrDefault(a => a.LeftItem.Top > SceneryDespawnHeight);
            if (bottomSceneryItem != null)
            {
                models.Remove(bottomSceneryItem);
            }
        }
    }
}
