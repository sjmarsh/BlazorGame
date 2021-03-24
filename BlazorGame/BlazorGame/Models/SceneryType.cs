namespace BlazorGame.Models
{ 
    public class SceneryType
    {
        public StageType StageType { get; set; }
        public string Name { get; set; }
        public string FullName { get { return $"{ StageType.ToString().ToLower()}-{Name.ToLower()}"; } }
    }  
}
