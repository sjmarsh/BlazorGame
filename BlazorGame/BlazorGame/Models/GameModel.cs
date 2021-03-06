using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlazorGame.Models
{
    public class GameModel
    {
        private int gameSpeed;
        private int medianCounter;
        private int stripeCounter;
        private bool isCollisionsEnabled;

        public GameModel()
        {
            ResetGame();
        }

        public EventHandler MainLoopCompleted;

        public bool IsRunning { get; set; }
        public Stopwatch GameTimer { get; set; }
        public int Score { get; set; }
        public PlayerCarModel PlayerCar { get; private set; }
        public List<MedianStripeModel> MedianStripes { get; set; }
        public List<AICarModel> AICars { get; set; }
        public bool ShowFog { get; set; }

        public int StageIndex { get; set; }
        public int StageNumber => StageIndex + 1;
        public string[] Stages = new[] { "rural", "desert", "alpine", "city", "coast" };

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
            gameSpeed = 1;
            StageIndex = 0;
            Score = 0;
            PlayerCar = new PlayerCarModel();
            AICars = new List<AICarModel>();
            GenerateMedianStrip();
            ShowFog = false;
            GameTimer = new Stopwatch();
            GameTimer.Reset();
            GameTimer.Start();
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
            medianCounter = 0;
            stripeCounter = 0;

            while (IsRunning)
            {
                AnimateMedianStrip();
                AnimateAICars();

                if (HasCollision())
                {
                    GameOver();
                }

                if(GameTimer.Elapsed.TotalMinutes >= 1 && StageIndex < 1)
                {
                    StageIndex = 1;
                    gameSpeed = 2;
                }
                if (GameTimer.Elapsed.TotalMinutes >= 2 && StageIndex < 2)
                {
                    StageIndex = 2;
                    gameSpeed = 2;
                    ShowFog = true;
                }
                if (GameTimer.Elapsed.TotalMinutes >= 3 && StageIndex < 3)
                {
                    StageIndex = 3;
                    gameSpeed = 3;
                    ShowFog = false;
                }
                if (GameTimer.Elapsed.TotalMinutes >= 4 && StageIndex < 4)
                {
                    StageIndex = 4;
                    gameSpeed = 4;
                    ShowFog = false;
                }
                if(GameTimer.Elapsed.TotalMinutes >= 5)
                {
                    GameOver();
                }

                Score++;

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(100 / gameSpeed);
            }
        }
       
        private void GenerateMedianStrip()
        {
            MedianStripes = new List<MedianStripeModel>();

            for (int i = 0; i < 80; i++)
            {
                AnimateMedianStrip();
            }
        }
                
        private void AnimateMedianStrip()
        {
            if (!MedianStripes.Any() || medianCounter > 2)
            {
                stripeCounter++;
                MedianStripes.Add(new MedianStripeModel { Id = stripeCounter });
                medianCounter = 0;
            }

            foreach (var stripe in MedianStripes)
            {
                stripe.Move();
            }

            var bottomStripe = MedianStripes.FirstOrDefault(m => m.Top > 330);
            if (bottomStripe != null)
            {
                MedianStripes.Remove(bottomStripe);
            }

            medianCounter++;
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