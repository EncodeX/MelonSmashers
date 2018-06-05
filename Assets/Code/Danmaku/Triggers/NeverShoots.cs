using UnityEngine;

namespace Code.Danmaku.Triggers {
    public class NeverShoots: Trigger {
        public override Trigger Duplicate()  {
            Trigger result = new NeverShoots();
            result.Type = Type;
            return result;
        }

        public override bool CanShoot(int currentFrame) {
            return false;
        }
    }
}