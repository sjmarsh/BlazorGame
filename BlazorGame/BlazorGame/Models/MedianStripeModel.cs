namespace BlazorGame.Models
{
    public class MedianStripeModel
    {
        public int Id { get; set; }
        public double Top { get; set; }
        public double Height { get; set; }

        public MedianStripeModel()
        {
            SendToTop();
        }
        
        public void Move()
        {
            Top *= 1.1;
            Height *= 1.06;
        }

        private void SendToTop()
        {
            Height = 1;
            Top = 1;
        }
    }
}
