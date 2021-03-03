namespace BlazorGame.Models
{
    public class CarModel
    {
        public double Top { get; protected set; }
        public double LeftSide { get; protected set; }
        public double Height { get; protected set; }
        public double Width { get; protected set; }
        public string Color { get; set; }
        public bool HasCollision { get; protected set; }
        
        public double RightSide => LeftSide + Width;
        public double Bottom => Top + Height;

        public void Crash()
        {
            HasCollision = true;
        }
    }
}
