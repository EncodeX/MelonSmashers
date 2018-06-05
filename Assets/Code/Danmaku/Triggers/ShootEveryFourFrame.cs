namespace Code.Danmaku.Triggers {
    public class ShootEveryFourFrame: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new ShootEveryFourFrame();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return currentFrame % 4 == 0;
        }
    }
}