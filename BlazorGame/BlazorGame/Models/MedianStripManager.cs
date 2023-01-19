using BlazorGame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGame.Models
{
    public class MedianStripManager
    {
        private const int TotalStripes = 80;
        private const int BottomOfRoad = 330;

        private readonly IBrowserService browserService;
        private int medianCounter;
        private int stripeCounter;
        
        public MedianStripManager(IBrowserService browserService)
        {
            this.browserService = browserService;
            
            
        }

        public List<MedianStripeModel> MedianStripes { get; set; }

        public async Task Reset()
        {
            medianCounter = 0;
            stripeCounter = 0;
            MedianStripes = new List<MedianStripeModel>();

            for (int i = 0; i < TotalStripes; i++)
            {
                await Animate();
            }
        }
        
        public async Task Animate()
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

            var bottomOfRoad = BottomOfRoad;
            var browserDimensions = await browserService.GetDimensions();
            if(browserDimensions != null && browserDimensions.Width < 991.9)
            {
                bottomOfRoad = (int)browserDimensions.Height;
            }

            var bottomStripe = MedianStripes.FirstOrDefault(m => m.Top > bottomOfRoad);
            if (bottomStripe != null)
            {
                MedianStripes.Remove(bottomStripe);
            }

            medianCounter++;
        }
    }
}
