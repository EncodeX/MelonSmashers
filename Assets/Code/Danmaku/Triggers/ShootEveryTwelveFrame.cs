namespace Code.Danmaku.Triggers {
    public class ShootEveryTwelveFrame: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new ShootEveryTwelveFrame();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return currentFrame % 12 == 0;
        }
    }
}