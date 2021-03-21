using System.Collections.Generic;
using System.Linq;

namespace BlazorGame.Models
{
    public class StageManager
    {
        private List<Stage> stages;
        private int currentStageNumber;

        public StageManager()
        {
            InitializeStages();
            Reset();
        }

        public Stage CurrentStage => stages.Single(s => s.Number == currentStageNumber);

        public bool AllStagesCompleted { get; private set; }

        public void Reset()
        {
            AllStagesCompleted = false;
            currentStageNumber = 1;
        }

        public void IncrementIfStageTimeHasElapsed(double gameTimeElapsedMinutes)
        {
            int timeToIncrementStage = stages.Where(s => s.Number <= currentStageNumber).Sum(s => s.DurationMinutes);
            if (gameTimeElapsedMinutes >= timeToIncrementStage)
            {
                if(currentStageNumber < stages.Count)
                {
                    currentStageNumber++;
                }
                else
                {
                    AllStagesCompleted = true;
                }
            }
        }

        private void InitializeStages()
        {
            stages = new List<Stage>
            {
                new Stage { Number = 1, Name = "rural", DurationMinutes = 1, Speed = 1 },
                new Stage { Number = 2, Name = "desert", DurationMinutes = 1, Speed = 2 },
                new Stage { Number = 3, Name = "alpine", DurationMinutes = 1, Speed = 2, ShowFog = true },
                new Stage { Number = 4, Name = "city", DurationMinutes = 1, Speed = 3 },
                new Stage { Number = 5, Name = "coast", DurationMinutes = 1, Speed = 4 }
            };
        } 
        
    }
}
