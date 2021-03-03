using System;

namespace BlazorGame.Models
{
    public class AICarModel : CarModel 
    {
        private const int MinWidth = 24;
        private const int MinHeight = 10;
        private const int MedianStripPosition = 75;

        private readonly Random rand;
        private double moveStrength;

        public AICarModel()
        {
            rand = new Random();
            Top = -20;
            LeftSide = RandomizeStartPosition();
            Height = MinHeight;
            Width = MinWidth;
            Color = RandomizeCarColor();
            moveStrength = RandomizeMoveStrength();
        }

        public void Move()
        {
            Top += 5;
            Width *= 1.028;
            Height *= 1.028;
            if(LeftSide < MedianStripPosition)
            {
                LeftSide -= (2.7 * moveStrength);
            }
            else
            {
                LeftSide += (1.3 * moveStrength);
            }
        }

        private string RandomizeCarColor()
        {
            var colors = new string[] { "green", "blue", "yellow", "purple", "orange" };
            var randomIndex = rand.Next(0, 5);
            return colors[randomIndex];
        }

        private int RandomizeStartPosition()
        {
            var positions = new int[] { 20, 80 };  // left or right side of road
            var randomPosition = rand.Next(0, 2);
            return positions[randomPosition];
        }

        private double RandomizeMoveStrength()
        {
            var moveStrengthInt = rand.Next(1, 4);
            return (double)moveStrengthInt / 3;
        }
    }
}
