namespace BlazorGame.Models
{
    public record GameDimensions
    {
        public bool IsMobileDevice { get; set; }

        public double GameAreaHeight { get; set; }
        public double GameAreaWidth { get; set; }

        public double GroundHeight { get; set; }
        public double GroundWidth { get; set; }

        public double RoadHeight { get; set; }
        public double RoadWidth { get; set; }
    }
}
