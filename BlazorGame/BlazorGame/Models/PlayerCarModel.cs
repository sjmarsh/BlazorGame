using BlazorGame.Services;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public class PlayerCarModel : CarModel
    {
        private const int MoveDistance = 20;
        private const int MaxHeight = 38;
        private const int MaxWidth = 92;
        private const int RoadLeftSide = -140;
        private const int RoadRightSide = 180;

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
            }
        }

        public void MoveLeft()
        {
            if(Left >= RoadLeftSide)
            {
                Left -= MoveDistance;
            }
        }

        public void MoveRight()
        {
            if(Left <= RoadRightSide)
            {
                Left += MoveDistance;
            }
        }
    }
}
