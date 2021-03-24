using System.Collections.Generic;
using System.Linq;

namespace BlazorGame.Models
{
    public partial class SceneryManager
    {
        private const int NewScenerySpawnHeight = 60;
        private const int SceneryDespawnHeight = 180;
        
        public SceneryManager()
        {
            Scenery = new List<SceneryModel>();
        }

        public IList<SceneryModel> Scenery { get; private set; }

        public StageType CurrentStageType { get; set; }

        public void Reset()
        {
            Scenery.Clear();
        }
                
        public void Animate()
        {
            AnimateModels(Scenery);
        }

        private void AnimateModels(IList<SceneryModel> models)
        {
            if (!models.Any() || !models.Any(a => a.LeftItem.Top < NewScenerySpawnHeight))
            {
                models.Add(new SceneryModel(CurrentStageType));
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
