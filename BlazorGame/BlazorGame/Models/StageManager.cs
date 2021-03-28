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

        public bool IsFinalStage => currentStageNumber == stages.Count;

        public bool AllStagesCompleted { get; private set; }

        public bool ShowCheckpoint { get; private set; }

        public void Reset()
        {
            AllStagesCompleted = false;
            currentStageNumber = 1;
        }

        public void IncrementStageIfRequired(double gameTimeElapsedMinutes)
        {
            if (gameTimeElapsedMinutes >= GetEndOfCurrentStageTime())
            {
                if (currentStageNumber < stages.Count)
                {
                    currentStageNumber++;
                }
                else
                {
                    AllStagesCompleted = true;
                }
            }
        }
        
        public void ShowCheckpointIfRequired(double gameTimeElapsedSeconds)
        {
            const int CheckPointDuration = 5;
            var endOfStageSeconds = GetEndOfCurrentStageTime() * 60;
             ShowCheckpoint = (gameTimeElapsedSeconds >= (endOfStageSeconds - CheckPointDuration)) 
                                && gameTimeElapsedSeconds < endOfStageSeconds;
        }

        private int GetEndOfCurrentStageTime()
        {
            return stages.Where(s => s.Number <= currentStageNumber).Sum(s => s.DurationMinutes);
        }

        private void InitializeStages()
        {
            stages = new List<Stage>
            {
                new Stage { Number = 1, StageType = StageType.Rural, DurationMinutes = 1, Speed = 1.5 },
                new Stage { Number = 2, StageType = StageType.Desert, DurationMinutes = 1, Speed = 2 },
                new Stage { Number = 3, StageType = StageType.Alpine, DurationMinutes = 1, Speed = 2, ShowFog = true },
                new Stage { Number = 4, StageType = StageType.City, DurationMinutes = 1, Speed = 3 },
                new Stage { Number = 5, StageType = StageType.Coast, DurationMinutes = 1, Speed = 4 }
            };
        } 
        
    }
}
