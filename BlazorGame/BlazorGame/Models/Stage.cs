namespace BlazorGame.Models
{
    public class Stage
    {
        public int Number { get; set; }
        public StageType StageType { get; set; }
        public int DurationMinutes { get; set; }
        public double Speed { get; set; }
        public bool ShowFog { get; set; }
        public double? BrightnessOffset { get; set; }
        public bool IsNightTime { get; set; }
        public string Name { get { return StageType.ToString().ToLower(); } }
    }
}
