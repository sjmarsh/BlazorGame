using System.Collections.Generic;
using System.Linq;

namespace BlazorGame.Models
{
    public class MedianStripManager
    {
        private const int TotalStripes = 80;
        private const int BottomOfRoad = 330;

        private int medianCounter;
        private int stripeCounter;

        public MedianStripManager()
        {
            Reset();
        }

        public List<MedianStripeModel> MedianStripes { get; set; }

        public void Reset()
        {
            medianCounter = 0;
            stripeCounter = 0;
            MedianStripes = new List<MedianStripeModel>();

            for (int i = 0; i < TotalStripes; i++)
            {
                Animate();
            }
        }
        
        public void Animate()
        {
            if (!MedianStripes.Any() || medianCounter > 2)
            {
                MedianStripes.Add(new MedianStripeModel { Id = stripeCounter++ });
                medianCounter = 0;
            }

            foreach (var stripe in MedianStripes)
            {
                stripe.Move();
            }

            var bottomStripe = MedianStripes.FirstOrDefault(m => m.Top > BottomOfRoad);
            if (bottomStripe != null)
            {
                MedianStripes.Remove(bottomStripe);
            }

            medianCounter++;
        }
    }
}
