using System;

namespace BlazorGame.Models
{
    public class AICarModel : CarModel 
    {
        private const int MinWidth = 24;
        private const int MinHeight = 10;
        private const int HiddenTopRoadHeight = 20;

        private readonly Random rand;
        private readonly bool isMobileDevice;
        private readonly double roadHeight;
        private readonly double roadWidth;

        private double leftCarSpawnPosition;
        private double rightCarSpawnPosition;
        private double randomMoveVelocity;

        public AICarModel(bool isMobileDevice, double roadHeight, double roadWidth)
        {
            this.isMobileDevice = isMobileDevice;
            this.roadHeight = roadHeight;
            this.roadWidth = roadWidth;

            rand = new Random();
                        
            Top = -HiddenTopRoadHeight;
            Height = MinHeight;
            Width = MinWidth;           
            Color = RandomizeCarColor();

            leftCarSpawnPosition = (roadWidth * 0.3);
            rightCarSpawnPosition = roadWidth * 0.6;

            Left = GetRandomLeftOrRightCarPosition(leftCarSpawnPosition, rightCarSpawnPosition);

            randomMoveVelocity = GetRandomMoveVelocity();
        }

        public void Move()
        {
            const double MoveDistance = 5;            
            var growPerspectiveRatio = 1.028;
            if (isMobileDevice)
            {
                growPerspectiveRatio = 1 + (10 / roadHeight);
            }

            Top += MoveDistance;
            Width *= growPerspectiveRatio;
            Height *= growPerspectiveRatio;
                        
            var xAxisToTravel = 0d;
            if (Top > 0)
            {
                var yAxisTravelledPercentage = Top / (roadHeight + HiddenTopRoadHeight);
                xAxisToTravel = roadWidth / 2 * yAxisTravelledPercentage * randomMoveVelocity; 
            }

            var medianStripPosition = roadWidth / 2;
            if (Left < medianStripPosition)
            {
                Left = leftCarSpawnPosition - Width - xAxisToTravel;
            }
            else
            {
                Left = rightCarSpawnPosition + xAxisToTravel;
            }            
        }

        private string RandomizeCarColor()
        {
            var colors = new string[] { "green", "blue", "yellow", "purple", "orange" };
            var randomIndex = rand.Next(0, colors.Length);
            return colors[randomIndex];
        }

        private double GetRandomLeftOrRightCarPosition(double leftCarSpawnPosition, double rightCarSpawnPosition)
        {
            var positions = new double[] { leftCarSpawnPosition, rightCarSpawnPosition };
            var randomPosition = rand.Next(0, positions.Length);
            return positions[randomPosition];
        }

        private double GetRandomMoveVelocity()
        {
            var moveVelocityInt = rand.Next(1, 4);
            return (double)moveVelocityInt / 3;
        }
    }
}
