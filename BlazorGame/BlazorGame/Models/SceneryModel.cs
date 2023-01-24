namespace BlazorGame.Models
{
    public class SceneryModel
    {
        private readonly double verticalMoveDisance;
        private readonly double groundHeight;
        private readonly double groundWidth;

        public SceneryModel(StageType stageType, double leftItemSpawnXAxis, double rightItemSpwanXAxis, double verticalMoveDisance, double groundHeight, double groundWidth)
        {
            this.verticalMoveDisance = verticalMoveDisance;
            this.groundHeight = groundHeight;
            this.groundWidth = groundWidth;

            LeftItem = new SceneryItemModel(leftItemSpawnXAxis, stageType);
            RightItem = new SceneryItemModel(rightItemSpwanXAxis, stageType);           
        }

        public SceneryItemModel LeftItem { get; private set; }
        public SceneryItemModel RightItem { get; private set; }

        public void Move()
        {
            LeftItem.MoveVertical(verticalMoveDisance);
            RightItem.MoveVertical(verticalMoveDisance);
            MoveHorizontal();
        }

        private void MoveHorizontal()
        {
            var vertialDistanceMoved = LeftItem.Top / groundHeight;
            var hoizontalDistanceToMove = groundWidth / 2 * vertialDistanceMoved;
            // Need to compensate for left item model size increase as it moves down the screen
            var leftItemMoveDistance = -1 * (hoizontalDistanceToMove + LeftItem.Width);

            LeftItem.MoveHorizontal(leftItemMoveDistance);
            RightItem.MoveHorizontal(hoizontalDistanceToMove);
        }
    }
}
