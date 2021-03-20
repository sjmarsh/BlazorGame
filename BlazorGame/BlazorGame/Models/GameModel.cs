using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlazorGame.Models
{
    public class GameModel
    {
        private bool isCollisionsEnabled;

        public GameModel()
        {
            StageManager = new StageManager();
            GameTimer = new Stopwatch();
            Stats = new StatsModel();
            MedianStripManager = new MedianStripManager();
            ResetGame();
        }

        public EventHandler MainLoopCompleted;

        public bool IsRunning { get; private set; }
        public bool IsComplete { get; private set; }
        public Stopwatch GameTimer { get; private set; }
        public StatsModel Stats { get; private set; }
        public StageManager StageManager { get; private set; }
        public MedianStripManager MedianStripManager { get; private set; }
        public PlayerCarModel PlayerCar { get; private set; }
        public List<AICarModel> AICars { get; private set; }
        
        public void StartStopGame()
        {
            if (!IsRunning)
            {
                ResetGame();
                MainLoop();
            }
            else
            {
                GameOver();
            }
        }

        public void MovePlayerCarLeft()
        { 
            if(IsRunning)
            {
                PlayerCar.MoveLeft();            
            }          
        }

        public void MovePlayerCarRight()
        {
            if (IsRunning)
            {
                PlayerCar.MoveRight();
            }
        }

        public void ToggleCollisionsEnabled()
        {
            isCollisionsEnabled = !isCollisionsEnabled;
        }

        private void ResetGame()
        {
            IsComplete = false;
            GameTimer.Reset();
            GameTimer.Start();
            Stats.Reset();
            StageManager.Reset();
            MedianStripManager.Reset();
            PlayerCar = new PlayerCarModel();
            AICars = new List<AICarModel>();           
            isCollisionsEnabled = true;          
        }

        private void GameOver()
        {
            IsRunning = false;
            GameTimer.Stop();
        }
              
        private async void MainLoop()
        {
            IsRunning = true;
            
            while (IsRunning)
            {
                if (HasCollision())
                {
                    GameOver();
                }

                MedianStripManager.Animate();
                AnimateAICars();

                StageManager.IncrementStageIfTimeHasElapsed(GameTimer.Elapsed.TotalMinutes);

                if(StageManager.AllStagesCompleted)
                {
                    GameOver();
                    IsComplete = true;
                }

                Stats.Update(GameTimer.Elapsed, StageManager.CurrentStage.Number);

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(100 / StageManager.CurrentStage.Speed);
            }
        }
       
        private void AnimateAICars()
        {
            if (!AICars.Any() || !AICars.Any(a => a.Top < 120))
            {
                AICars.Add(new AICarModel());
            }

            foreach (var aiCar in AICars)
            {
                aiCar.Move();
            }

            var bottomCar = AICars.FirstOrDefault(a => a.Top > 290);
            if(bottomCar != null)
            {
                AICars.Remove(bottomCar);
            }
        }

        private bool HasCollision()
        {
            if(!isCollisionsEnabled)
            {
                return false;
            }

            var carCollided = false;
            var carNearPlayer = AICars.FirstOrDefault(a => a.Top == PlayerCar.Top || (a.Bottom >= PlayerCar.Top && a.Top < PlayerCar.Top));
            if (carNearPlayer != null)
            {
                carCollided = (carNearPlayer.RightSide >= PlayerCar.LeftSide && carNearPlayer.LeftSide <= PlayerCar.LeftSide)
                    || (carNearPlayer.LeftSide <= PlayerCar.RightSide && carNearPlayer.RightSide >= PlayerCar.RightSide);
            }

            if (carCollided)
            {
                PlayerCar.Crash();
                carNearPlayer.Crash();
            }

            return carCollided;
        }
    }
}