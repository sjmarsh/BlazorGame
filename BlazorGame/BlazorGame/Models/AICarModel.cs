using System;

namespace BlazorGame.Models
{
    public class AICarModel : CarModel 
    {
        private const int MinWidth = 24;
        private const int MinHeight = 10;

        private readonly Random rand;
        private readonly double moveVelocity;
        private readonly double roadWidth;
        
        public AICarModel(double roadWidth)
        {
            this.roadWidth = roadWidth;

            rand = new Random();
            Top = -20;

            var leftCarSpawnPosition = (int)(roadWidth * 0.13);
            var rightCarSpawnPosition = (int)(roadWidth * 0.53);

            Left = RandomizeStartPosition(leftCarSpawnPosition, rightCarSpawnPosition);
            Height = MinHeight;
            Width = MinWidth;
            Color = RandomizeCarColor();
            moveVelocity = RandomizeMoveVelocity();
            
            ;
        }

        public void Move()
        {
            const double MoveDistance = 5;
            const double GrowPerspectiveRatio = 1.028;

            double moveLeftDistance = roadWidth * 0.018;//2.7;
            double moveRightDistance = roadWidth * 0.0086;//1.3;

            Top += MoveDistance;
            Width *= GrowPerspectiveRatio;
            Height *= GrowPerspectiveRatio;
            var medianStripPosition = (int)roadWidth / 2;
            if (Left < medianStripPosition)
            {
                Left -= (moveLeftDistance * moveVelocity);
            }
            else
            {
                Left += (moveRightDistance * moveVelocity);
            }
        }

        private string RandomizeCarColor()
        {
            var colors = new string[] { "green", "blue", "yellow", "purple", "orange" };
            var randomIndex = rand.Next(0, colors.Length);
            return colors[randomIndex];
        }

        private int RandomizeStartPosition(int leftCarSpawnPosition, int rightCarSpawnPosition)
        {
            var positions = new int[] { leftCarSpawnPosition, rightCarSpawnPosition };  // left or right side of road
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
