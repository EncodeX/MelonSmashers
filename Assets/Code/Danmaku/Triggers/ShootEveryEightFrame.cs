namespace Code.Danmaku.Triggers {
    public class ShootEveryEightFrame: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new ShootEveryEightFrame();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return currentFrame % 8 == 0;
        }
    }
}