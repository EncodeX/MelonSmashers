using System.Collections.Generic;
using UnityEngine;

namespace Code.Danmaku {
    public class Action {
        public int Angle = -165;
        public int Speed = 5;
        public float Direction = -165 * Mathf.Deg2Rad;
        public float CurrentAngle = -165;
		public float CurrentSpeed = 5;
        public int AngleOffset = 0;
        public float SpeedOffset = 0;
        public int ActionTime = -1;
        public int CurrentFrame = 0;
        public float ActionHealthThreshold = 0;
        public Action NextAction = null;
        public BulletPattern BulletPattern;
        public List<BulletPattern> BulletPatterns;

        public void CalcDirection() {
            CurrentAngle += (float)AngleOffset / 60;
            Direction = CurrentAngle * Mathf.Deg2Rad;
        }

        public void CalcSpeed() {
            CurrentSpeed += SpeedOffset / 60;
        }
    }
}