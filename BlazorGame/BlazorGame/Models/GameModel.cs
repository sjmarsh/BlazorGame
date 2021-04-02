﻿using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlazorGame.Models
{
    public class GameModel
    {
        private bool isCollisionsEnabled;

        public GameModel(StageManager stageManager, MedianStripManager medianStripManager, SceneryManager sceneryManager, AICarManager aICarManager)
        {
            StageManager = stageManager;
            MedianStripManager = medianStripManager;
            SceneryManager = sceneryManager;
            AICarManager = aICarManager;
            GameTimer = new Stopwatch();
            Stats = new StatsModel();
            
            ResetGame();
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

        public void StartStopGame()
        {
            if (!IsRunning)
            {
                ResetGame();
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

        private void ResetGame()
        {
            IsComplete = false;
            GameTimer.Reset();
            GameTimer.Start();
            Stats.Reset();
            StageManager.Reset();
            MedianStripManager.Reset();
            SceneryManager.Reset();
            PlayerCar = new PlayerCarModel();
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

                MedianStripManager.Animate();
                AICarManager.Animate();
                StageManager.ShowCheckpointIfRequired(GameTimer.Elapsed.TotalSeconds);
                StageManager.IncrementStageIfRequired(GameTimer.Elapsed.TotalMinutes);
                SceneryManager.CurrentStageType = StageManager.CurrentStage.StageType;
                SceneryManager.Animate();

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