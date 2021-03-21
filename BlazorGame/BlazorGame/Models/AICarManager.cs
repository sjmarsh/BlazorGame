using System.Collections.Generic;
using System.Linq;

namespace BlazorGame.Models
{
    public class AICarManager
    {
        private const int NewCarSpawnHeight = 120;
        private const int BottomOfRoad = 290;

        public AICarManager()
        {
            Cars = new List<AICarModel>();
        }

        public List<AICarModel> Cars { get; private set; }

        public void Reset()
        {
            Cars.Clear();
        }

        public void Animate()
        {
            if (!Cars.Any() || !Cars.Any(a => a.Top < NewCarSpawnHeight))
            {
                Cars.Add(new AICarModel());
            }

            foreach (var aiCar in Cars)
            {
                aiCar.Move();
            }

            var bottomCar = Cars.FirstOrDefault(a => a.Top > BottomOfRoad);
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
                var carHasCollided = (carNearPlayer.RightSide >= playerCar.LeftSide && carNearPlayer.LeftSide <= playerCar.LeftSide)
                    || (carNearPlayer.LeftSide <= playerCar.RightSide && carNearPlayer.RightSide >= playerCar.RightSide);
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
