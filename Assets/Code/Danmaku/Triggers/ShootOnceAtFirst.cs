using UnityEngine;

namespace Code.Danmaku.Triggers {
    public class ShootOnceAtFirst: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new ShootOnceAtFirst();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return currentFrame == 0;
        }
    }
}