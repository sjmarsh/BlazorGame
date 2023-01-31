using BlazorGame.Services;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public class PlayerCarModel : CarModel
    {
        private const int MoveDistance = 20;
        private const int MaxHeight = 38;
        private const int MaxWidth = 92;
        
        private double roadLeftSide = -140;
        private double roadRightSide = 180;

        private readonly IGameDimensionService gameDimensionService;

        public PlayerCarModel(IGameDimensionService gameDimensionService)
        {
            this.gameDimensionService = gameDimensionService;
        }

        public async Task Reset()
        {
            Top = 210;
            Left = 20;
            Height = MaxHeight;
            Width = MaxWidth;
            Color = "red";
            HasCollision = false;

            var gameDimensions = await gameDimensionService.GetDimensions();
            if (gameDimensions.IsMobileDevice)
            {
                Top = gameDimensions.GroundHeight * 0.72;
                
                Height = gameDimensions.GroundHeight * 0.12;
                Width = gameDimensions.GroundWidth * 0.27;
                if (gameDimensions.IsLandscape)
                {
                    Height = gameDimensions.GroundHeight * 0.27;
                    Width = gameDimensions.GroundWidth * 0.12;
                }

                Left = (gameDimensions.RoadWidth * 0.5) - (Width * 0.5);
                
                roadLeftSide = -1 * (gameDimensions.RoadWidth * 0.9);
                roadRightSide = gameDimensions.RoadWidth * 2 * 0.9 - Width;
            }
        }

        public void MoveLeft()
        {
            if(Left >= roadLeftSide)
            {
                Left -= MoveDistance;
            }
        }

        public void MoveRight()
        {
            if(Left <= roadRightSide)
            {
                Left += MoveDistance;
            }
        }
    }
}
