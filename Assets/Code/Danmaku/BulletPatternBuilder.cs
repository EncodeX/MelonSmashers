using System;
using System.Collections.Generic;
using Code.Danmaku.Triggers;
using UnityEngine;

namespace Code.Danmaku {
    public class BulletPatternBuilder {
        private static BulletPatternBuilder _instance;

        private readonly Dictionary<string, int> _patternDictionary;
        private readonly List<BulletPattern> _bulletPatterns;
        private BulletPattern _currentBuildingPattern;

        private BulletPatternBuilder() {
            _patternDictionary = new Dictionary<string, int>();
            _bulletPatterns = new List<BulletPattern>();
        }

        public static BulletPatternBuilder GetInstance() {
            if (_instance == null) {
                _instance = new BulletPatternBuilder();
            }

            return _instance;
        }

        public void Clear() {
            _bulletPatterns.Clear();
            _patternDictionary.Clear();
        }

        public BulletPattern GetPattern(string name) {
            if (_patternDictionary.ContainsKey(name)) {
                return _bulletPatterns[_patternDictionary[name]];
            }
            Debug.LogError("Can't find pattern " + name + ".");
            return null;
        }

        public BulletPatternBuilder CreatePattern(string name) {
            if (_patternDictionary.ContainsKey(name)) {
                Debug.LogError("Can't create pattern with existing name.");
                return null;
            }

            _currentBuildingPattern = new BulletPattern {Trigger = new ShootOnceAtFirst()};
            _patternDictionary.Add(name, _bulletPatterns.Count);
            _bulletPatterns.Add(_currentBuildingPattern);
            return this;
        }

        public BulletPatternBuilder SetDelayFrame(int delayFrame) {
            _currentBuildingPattern.DelayFrame = delayFrame;
            return this;
        }

        public BulletPatternBuilder SetIntervalFrame(int intervalFrame) {
            _currentBuildingPattern.IntervalFrame = intervalFrame;
            return this;
        }

        public BulletPatternBuilder SetCoolDownFrame(int coolDownFrame) {
            _currentBuildingPattern.CoolDownFrame = coolDownFrame;
            return this;
        }

        public BulletPatternBuilder SetBulletCount(int bulletCount) {
            _currentBuildingPattern.BulletCount = bulletCount;
            return this;
        }

        public BulletPatternBuilder SetAngle(int angle) {
            _currentBuildingPattern.Angle = angle;
            return this;
        }

        public BulletPatternBuilder SetBulletAngle(int bulletAngle) {
            _currentBuildingPattern.BulletAngle = bulletAngle;
            return this;
        }

        public BulletPatternBuilder SetAngleOffset(float angleOffset) {
            _currentBuildingPattern.AngleOffset = angleOffset;
            return this;
        }

        public BulletPatternBuilder SetAngleOffsetStepper(float stepper) {
            _currentBuildingPattern.AngleOffsetStepper = stepper;
            return this;
        }

        public BulletPatternBuilder SetLockedOnAvatar(bool lockedOnAvatar) {
            _currentBuildingPattern.LockedOnAvatar = lockedOnAvatar;
            return this;
        }

        public BulletPatternBuilder SetTrigger(Trigger trigger) {
            _currentBuildingPattern.Trigger = trigger;
            return this;
        }

        public BulletPatternBuilder SetTriggerType(TriggerType type) {
            _currentBuildingPattern.Trigger.Type = type;
            return this;
        }

        public BulletPatternBuilder SetDestroyableProportion(int proportion) {
            _currentBuildingPattern.DestroyableProportion = proportion;
            return this;
        }

        public BulletPatternBuilder SetPositionOffset(Vector2 offset) {
            _currentBuildingPattern.PosOffset = offset;
            return this;
        }

        public BulletPatternBuilder SetAngleRandomRange(Vector2 range) {
            _currentBuildingPattern.RandomAngleRange = range;
            return this;
        }

        public BulletPatternBuilder SetSpeedRandomRange(Vector2 range) {
            _currentBuildingPattern.RandomSpeedRange = range;
            return this;
        }

        public BulletPatternBuilder SetSpeedCorrection(float correction) {
            _currentBuildingPattern.SpeedCorrection = correction;
            return this;
        }

        public BulletPatternBuilder SetLoopAngleOffset(float offset) {
            _currentBuildingPattern.LoopAngleOffset = offset;
            return this;
        }

        public BulletPatternBuilder SetSpeedCorrectionOffset(float correctionOffset) {
            _currentBuildingPattern.SpeedCorrectionOffset = correctionOffset;
            return this;
        }
    }
}