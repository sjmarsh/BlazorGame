using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public class AICarManager
    {
        private const int NewCarSpawnHeight = 120;
        private const int CarDespawnHeight = 290;
        
        IBrowserService browserService;

        public AICarManager(IBrowserService browserService)
        {
            this.browserService = browserService;

            Cars = new List<AICarModel>();
        }

        public List<AICarModel> Cars { get; private set; }

        public void Reset()
        {
            Cars.Clear();
        }

        public async Task Animate()
        {
            var newCarSpawnHeight = NewCarSpawnHeight;
            var carDespawnHeight = CarDespawnHeight;
            var roadHeight = Constants.DefaultRoadHeight;
            var roadWidth = Constants.DefaultRoadWidth;

            var browserDimensions = await browserService.GetDimensions();
            if (browserDimensions.IsMobileDevice)
            {
                newCarSpawnHeight = (int)(browserDimensions.Height * 0.15);
                carDespawnHeight = (int)browserDimensions.Height - 100;
                roadHeight = browserDimensions.Height * 0.5;
                roadWidth = browserDimensions.Width * 0.36;
            }

            if (!Cars.Any() || !Cars.Any(a => a.Top < newCarSpawnHeight))
            {
                Cars.Add(new AICarModel(roadHeight, roadWidth));
            }

            foreach (var aiCar in Cars)
            {
                aiCar.Move();
            }

            var bottomCar = Cars.FirstOrDefault(a => a.Top > carDespawnHeight);
            if (bottomCar != null)
            {
                Cars.Remove(bottomCar);
            }
        }

        public AICarModel GetCarCollidedWithPlayer(PlayerCarModel playerCar)
        {
            AICarModel carCollidedWithPlayer = null;
            var carNearPlayer = GetCarNearestToPlayerCar(playerCar);
            if (carNearPlayer != null)
            {
                var carHasCollided = (carNearPlayer.RightSide >= playerCar.Left && carNearPlayer.Left <= playerCar.Left)
                    || (carNearPlayer.Left <= playerCar.RightSide && carNearPlayer.RightSide >= playerCar.RightSide);
                if (carHasCollided)
                {
                    carCollidedWithPlayer = carNearPlayer;
                }
            }
            return carCollidedWithPlayer;
        }

        private AICarModel GetCarNearestToPlayerCar(PlayerCarModel playerCar)
        {
            return Cars.FirstOrDefault(a => a.Top == playerCar.Top 
                                    || (a.Bottom >= playerCar.Top && a.Top < playerCar.Top));
        }
    }
}
