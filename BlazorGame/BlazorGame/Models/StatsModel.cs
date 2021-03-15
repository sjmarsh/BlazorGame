using System;

namespace BlazorGame.Models
{
    public class StatsModel
    {
        public StatsModel()
        {
            TimePlayed = new TimeSpan(0,0,0);
            StageNumber = 1;
            Score = 0;
        }

        public TimeSpan TimePlayed { get; set; }
        public int StageNumber { get; set; }
        public int Score { get; set; }

        public void Update(TimeSpan timePlayed, int stageNumber)
        {
            TimePlayed = timePlayed;
            StageNumber = stageNumber;
            Score++;    
        }
    }
}
