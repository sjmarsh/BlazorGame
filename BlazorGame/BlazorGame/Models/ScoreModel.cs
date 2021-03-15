using System;

namespace BlazorGame.Models
{
    public class ScoreModel
    {
        public string PlayerName { get; set; }
        public int Stage { get; set; }
        public int Score { get; set; }
        public DateTime ScoreDate { get; set; }
    }
}
