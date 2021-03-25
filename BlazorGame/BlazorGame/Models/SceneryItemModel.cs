using System.Collections.Generic;
using System.Linq;
using System;

namespace BlazorGame.Models
{
    public class SceneryItemModel : UIElement
    {
        private const int SpawnHeight = -20;
        private const int MinWidth = 20;
        private const int MinHeight = 20;

        private Random rand;

        public SceneryItemModel(double left_XAxis, StageType stageType)
        {
            Top = SpawnHeight;
            Height = MinHeight;
            Width = MinWidth;
            Left = left_XAxis;
            rand = new Random();
            SceneryType = GetRandomSceneryType(stageType);
        }

        public SceneryType SceneryType { get; private set; }

        public void MoveVertical()
        {
            const double MoveDistance = 8;
            const double GrowPerspectiveRatio = 1.08;
            Top += MoveDistance;
            Width *= GrowPerspectiveRatio;
            Height *= GrowPerspectiveRatio;
        }

        public void MoveHorizontal(double moveDistance)
        {
            Left += moveDistance;
        }

        private SceneryType GetRandomSceneryType(StageType stageType)
        {
            SceneryType sceneryType = null;
            var sceneryTypes = new List<SceneryType>
            {
                new SceneryType{ StageType = StageType.Rural, Name = "tree" },
                new SceneryType{ StageType = StageType.Rural, Name = "tree-2" },
                new SceneryType{ StageType = StageType.Rural, Name = "sign" },
                new SceneryType{ StageType = StageType.Rural, Name = "sign-2" },

                new SceneryType{ StageType = StageType.Desert, Name = "cactus" },
                new SceneryType{ StageType = StageType.Desert, Name = "cactus-2" },
                new SceneryType{ StageType = StageType.Desert, Name = "weed" },
                new SceneryType{ StageType = StageType.Desert, Name = "sign" },

                new SceneryType{ StageType = StageType.Alpine, Name = "tree" },
                new SceneryType{ StageType = StageType.Alpine, Name = "tree-2" },
                new SceneryType{ StageType = StageType.Alpine, Name = "sign" },
                new SceneryType{ StageType = StageType.Alpine, Name = "sign-2" },
                new SceneryType{ StageType = StageType.Alpine, Name = "snowman" },

                new SceneryType{ StageType = StageType.City, Name = "sign" },
                new SceneryType{ StageType = StageType.City, Name = "sign-2" },
                new SceneryType{ StageType = StageType.City, Name = "sign-3" },
                new SceneryType{ StageType = StageType.City, Name = "sign-4" },
                new SceneryType{ StageType = StageType.City, Name = "sign-5" },

                new SceneryType{ StageType = StageType.Coast, Name = "palm" }
            };

            var filteredSceneryTypes = sceneryTypes.Where(s => s.StageType == stageType);
            if (filteredSceneryTypes.Any())
            {
                sceneryType = filteredSceneryTypes.ElementAt(rand.Next(0, filteredSceneryTypes.Count()));
            }

            return sceneryType;
        }
    }
}
