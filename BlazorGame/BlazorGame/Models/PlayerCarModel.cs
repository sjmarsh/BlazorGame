namespace BlazorGame.Models
{
    public class PlayerCarModel : CarModel
    {
        private const int MoveDistance = 20;
        private const int MaxHeight = 38;
        private const int MaxWidth = 92;
        private const int RoadLeftSide = -140;
        private const int RoadRightSide = 180;
        
        public PlayerCarModel()
        {
            Top = 210;
            LeftSide = 20;
            Height = MaxHeight;
            Width = MaxWidth;
            Color = "red";
        }

        public void MoveLeft()
        {
            if(LeftSide >= RoadLeftSide)
            {
                LeftSide -= MoveDistance;
            }
        }

        public void MoveRight()
        {
            if(LeftSide <= RoadRightSide)
            {
                LeftSide += MoveDistance;
            }
        }
    }
}
