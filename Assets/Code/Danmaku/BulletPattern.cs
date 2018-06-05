using System;
using System.Reflection;
using UnityEngine;

namespace Code.Danmaku {
    /// <summary>
    /// Bullet Pattern
    /// </summary>
    public class BulletPattern{
        public Trigger Trigger;
        public int DelayFrame = 0;
        public int IntervalFrame = 20;
        public int CoolDownFrame = 0;
        public bool CoolingDown = false;
        public int CurrentFrame = 0;
        public bool Playing = false;
        public bool Completed = false;

        public int BulletCount = 1;
        public int Angle = -90;
        public int BulletAngle = 20;
        public Vector2 RandomAngleRange = new Vector2();
        public float AngleOffset = 0;
        public float AngleOffsetStepper = 0f;
        public float LoopAngleOffset = 0;
        public float CurrentBaseAngle = 0;
        public bool LockedOnAvatar = true;
        public float SpeedCorrection = 1;
        public float SpeedCorrectionOffset = 0;
        public Vector2? RandomSpeedRange = null;
        
        public float CurrentShootAngle = 0;
        public int DestroyableProportion = 100;
        public Vector2 PosOffset = new Vector2();

        public BulletPattern Duplicate() {
            var result = new BulletPattern {
                Trigger = Trigger.Duplicate(),
                DelayFrame = DelayFrame,
                IntervalFrame = IntervalFrame,
                CoolDownFrame = CoolDownFrame,
                CoolingDown = CoolingDown,
                CurrentFrame = CurrentFrame,
                Playing = Playing,
                Completed = Completed,
                BulletCount = BulletCount,
                Angle = Angle,
                BulletAngle = BulletAngle,
                AngleOffset = AngleOffset,
                AngleOffsetStepper = AngleOffsetStepper,
                LockedOnAvatar = LockedOnAvatar,
                CurrentShootAngle = CurrentShootAngle,
                DestroyableProportion = DestroyableProportion,
                PosOffset = new Vector2(PosOffset.x, PosOffset.y),
                RandomAngleRange = new Vector2(RandomAngleRange.x, RandomAngleRange.y),
                SpeedCorrection = SpeedCorrection,
                SpeedCorrectionOffset = SpeedCorrectionOffset,
                LoopAngleOffset = LoopAngleOffset,
                CurrentBaseAngle = CurrentBaseAngle
            };
            result.RandomSpeedRange = null;
            if (RandomSpeedRange != null) {
                result.RandomSpeedRange = new Vector2(((Vector2)RandomSpeedRange).x, ((Vector2)RandomSpeedRange).y);
            }
            return result;
        }
    }
}