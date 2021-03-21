using System;

namespace BlazorGame.Models
{
    public class AICarModel : CarModel 
    {
        private const int MinWidth = 24;
        private const int MinHeight = 10;
        private const int MedianStripPosition = 75;
        
        private readonly Random rand;
        private readonly double moveVelocity;

        public AICarModel()
        {
            rand = new Random();
            Top = -20;
            LeftSide = RandomizeStartPosition();
            Height = MinHeight;
            Width = MinWidth;
            Color = RandomizeCarColor();
            moveVelocity = RandomizeMoveVelocity();
        }

        public void Move()
        {
            const double MoveDistance = 5;
            const double GrowPerspectiveRatio = 1.028;
            const double MoveLeftDistance = 2.7;
            const double MoveRightDistance = 1.3;

            Top += MoveDistance;
            Width *= GrowPerspectiveRatio;
            Height *= GrowPerspectiveRatio;
            if(LeftSide < MedianStripPosition)
            {
                LeftSide -= (MoveLeftDistance * moveVelocity);
            }
            else
            {
                LeftSide += (MoveRightDistance * moveVelocity);
            }
        }

        private string RandomizeCarColor()
        {
            var colors = new string[] { "green", "blue", "yellow", "purple", "orange" };
            var randomIndex = rand.Next(0, colors.Length);
            return colors[randomIndex];
        }

        private int RandomizeStartPosition()
        {
            var positions = new int[] { 20, 80 };  // left or right side of road
            var randomPosition = rand.Next(0, positions.Length);
            return positions[randomPosition];
        }

        private double RandomizeMoveVelocity()
        {
            var moveVelocityInt = rand.Next(1, 4);
            return (double)moveVelocityInt / 3;
        }
    }
}
