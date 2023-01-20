using System;
using System.Threading.Tasks;
using System.Diagnostics;
using BlazorGame.Services;

namespace BlazorGame.Models
{
    public class GameModel
    {
        private readonly IBrowserService browserService;
        private bool isCollisionsEnabled;

        public GameModel(StageManager stageManager, MedianStripManager medianStripManager, SceneryManager sceneryManager, AICarManager aICarManager, IBrowserService browserService)
        {
            StageManager = stageManager;
            MedianStripManager = medianStripManager;
            SceneryManager = sceneryManager;
            AICarManager = aICarManager;
            this.browserService = browserService;

            GameTimer = new Stopwatch();
            Stats = new StatsModel();
            PlayerCar = new PlayerCarModel(this.browserService);
        }

        public EventHandler MainLoopCompleted;

        public bool IsRunning { get; private set; }
        public bool IsComplete { get; private set; }
        public Stopwatch GameTimer { get; private set; }
        public StatsModel Stats { get; private set; }
        public StageManager StageManager { get; private set; }
        public MedianStripManager MedianStripManager { get; private set; }
        public SceneryManager SceneryManager { get; private set; }
        public PlayerCarModel PlayerCar { get; private set; }
        public AICarManager AICarManager { get; private set; }

        public async Task StartStopGame()
        {
            if (!IsRunning)
            {
                await ResetGame();
                MainLoop();
            }
            else
            {
                GameOver(false);
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

        public async Task ResetGame()
        {
            IsComplete = false;
            GameTimer.Reset();
            GameTimer.Start();
            Stats.Reset();
            StageManager.Reset();
            await MedianStripManager.Reset();
            SceneryManager.Reset();
            await PlayerCar.Reset();
            AICarManager.Reset();
            isCollisionsEnabled = true;          
        }

        private void GameOver(bool isComplete)
        {
            IsRunning = false;
            GameTimer.Stop();
            IsComplete = isComplete;
        }
              
        private async void MainLoop()
        {
            IsRunning = true;
            
            while (IsRunning)
            {
                if (HasCollision())
                {
                    GameOver(false);
                }

                await MedianStripManager.Animate();
                await AICarManager.Animate();
                StageManager.ShowCheckpointIfRequired(GameTimer.Elapsed.TotalSeconds);
                StageManager.IncrementStageIfRequired(GameTimer.Elapsed.TotalMinutes);
                SceneryManager.CurrentStageType = StageManager.CurrentStage.StageType;
                await SceneryManager.Animate();

                if (StageManager.AllStagesCompleted)
                {
                    GameOver(true);
                }

                Stats.Update(GameTimer.Elapsed, StageManager.CurrentStage.Number);

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(Convert.ToInt32(100 / StageManager.CurrentStage.Speed));
            }
        }
       
        private bool HasCollision()
        {
            if(!isCollisionsEnabled)
            {
                return false;
            }

            var hasCarCollided = false;

            var aiCarCollidedWithWithPlayer = AICarManager.GetCarCollidedWithPlayer(PlayerCar);

            if (aiCarCollidedWithWithPlayer != null)
            {
                PlayerCar.Crash();
                aiCarCollidedWithWithPlayer.Crash();
                hasCarCollided = true;
            }

            return hasCarCollided;
        }
    }
}