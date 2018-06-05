namespace Code.Danmaku {
    public enum TriggerType {
        ONCE,
        REPEAT,
        DIED
    }
    
    public abstract class Trigger {
        public TriggerType Type;

        public Trigger() {
            Type = TriggerType.REPEAT;
        }

        public bool Activate(BulletPattern pattern) {
            if (pattern.DelayFrame > 0) {
                pattern.DelayFrame--;
                return false;
            }
            
            return true;
        }

        public abstract Trigger Duplicate();

        public abstract bool CanShoot(int currentFrame);
    }
} 