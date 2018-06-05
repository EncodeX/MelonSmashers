namespace Code.Danmaku.Triggers {
    public class ShootEveryTwoFrame: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new ShootEveryTwoFrame();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return currentFrame % 2 == 0;
        }
    }
}