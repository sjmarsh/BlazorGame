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

        private readonly IBrowserService browserService;

        public PlayerCarModel(IBrowserService browserService)
        {
            this.browserService = browserService;
        }

        public async Task Reset()
        {
            Top = 210;
            Left = 20;
            Height = MaxHeight;
            Width = MaxWidth;
            Color = "red";
            HasCollision = false;

            var browserDimensions = await browserService.GetDimensions();
            if (browserDimensions.IsMobileDevice)
            {
                Top = browserDimensions.Height / 2 * .72;
                Height = browserDimensions.Height / 2 * .12;
                Width = browserDimensions.Width * .27;
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
