using BlazorGame.Models;
using System;
using System.Threading.Tasks;

namespace BlazorGame.Services
{
    public interface IGameDimensionService
    {
        Task<GameDimensions> GetDimensions();
    }

    public class GameDimensionService : IGameDimensionService
    {
        private readonly IBrowserService browserService;

        public GameDimensionService(IBrowserService browserService)
        {
            this.browserService = browserService;
        }

        public async Task<GameDimensions> GetDimensions()
        {
            var dimensions = new GameDimensions
            {
                IsMobileDevice = false,
                IsLandscape = false,
                GameAreaHeight = 640,
                GameAreaWidth = 500,
                GroundHeight = 310,
                GroundWidth = 500,
                RoadHeight = 310,
                RoadWidth = 150
            };

            try
            {
                var browserDimensions = await browserService.GetDimensions();
                const double MaxMobileDeviceWidth = 991.98;
                if (browserDimensions.Width < MaxMobileDeviceWidth)
                {
                    dimensions = dimensions with
                    {
                        IsMobileDevice = true,
                        IsLandscape = browserDimensions.Width > browserDimensions.Height, 
                        GameAreaHeight = browserDimensions.Height,
                        GameAreaWidth = browserDimensions.Width,
                        GroundHeight = browserDimensions.Height * 0.5,
                        GroundWidth = browserDimensions.Width,
                        RoadHeight = browserDimensions.Height * 0.5,
                        RoadWidth = browserDimensions.Width * 0.36
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get browser dimensions. {ex.Message}");
            }

            return dimensions;
        }
    }
}
