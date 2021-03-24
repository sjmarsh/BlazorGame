namespace BlazorGame.Models
{
    public class CarModel : UIElement
    {
        public string Color { get; set; }
        public bool HasCollision { get; protected set; }
        
        public double RightSide => Left + Width;
        public double Bottom => Top + Height;

        public void Crash()
        {
            HasCollision = true;
        }
    }
}
