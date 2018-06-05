using UnityEngine;

namespace Code.Danmaku {
    /// <summary>
    /// Bullet Class, stores all data for a flying bullet
    /// </summary>
    public class FlyObject: MonoBehaviour {
        public float Direction = 0;
        
        public float Speed = 15;
//        public float Acceleration = 0;
//        public float AngularAccel = 0;
        public float SpeedCorrection = 1;
        public float SpeedCorrectionOffset = 0;

        public int LifeFrame = 3000;
        public int LivedFrame = 0;
        public bool Dead;
    }
}