using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public class AICarManager
    {
        private const double NewCarSpawnHeight = 120;
        private const double CarDespawnHeight = 290;

        private readonly IGameDimensionService gameDimensionService;
        
        public AICarManager(IGameDimensionService gameDimensionService)
        {
            this.gameDimensionService = gameDimensionService;

            Cars = new List<AICarModel>();
        }

        public List<AICarModel> Cars { get; private set; }

        public void Reset()
        {
            Cars.Clear();
        }

        public async Task Animate()
        {
            var gameDimensions = await gameDimensionService.GetDimensions();

            var newCarSpawnHeight = NewCarSpawnHeight;
            var carDespawnHeight = CarDespawnHeight;
            var roadHeight = gameDimensions.RoadHeight;
            var roadWidth = gameDimensions.RoadWidth;
                        
            if (gameDimensions.IsMobileDevice)
            {
                newCarSpawnHeight = gameDimensions.GameAreaHeight * 0.15;
                carDespawnHeight = gameDimensions.GameAreaHeight * 0.85;
            }

            if (!Cars.Any() || !Cars.Any(a => a.Top < newCarSpawnHeight))
            {
                Cars.Add(new AICarModel(gameDimensions.IsMobileDevice, roadHeight, roadWidth));
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
