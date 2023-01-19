namespace BlazorGame.Models
{
    public class SceneryModel
    {
        protected const double HorizontalMoveDistance = 5.9;

        private double moveLeftDistance;
        
        public SceneryModel(StageType stageType, int leftItemPosition, int rightItemPosition)
        {
            LeftItem = new SceneryItemModel(leftItemPosition, stageType);
            RightItem = new SceneryItemModel(rightItemPosition, stageType);
                        
            moveLeftDistance = HorizontalMoveDistance;
        }

        public SceneryItemModel LeftItem { get; private set; }
        public SceneryItemModel RightItem { get; private set; }

        public void Move()
        {
            LeftItem.MoveVertical();
            RightItem.MoveVertical();
            MoveHorizontal();
        }

        private void MoveHorizontal()
        {
            // Need to compensate for left item model size increase as it moves down the screen
            moveLeftDistance += LeftItem.Width * 0.01;
            LeftItem.MoveHorizontal(-moveLeftDistance);
            RightItem.MoveHorizontal(HorizontalMoveDistance);
        }
    }
}
