using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Danmaku {
    public class SceneActionBuilder {
        private BulletPatternBuilder _patternManager;
        private static readonly string[] _colors = {"cyan", "yellow", "magenta"};
        private readonly SceneAction _action;
        private readonly SceneActionBuilder _parent;
        
        private SceneActionBuilder() {
            _action= new SceneAction();
            _parent = null;
            _patternManager = BulletPatternBuilder.GetInstance();
        }
        
        private SceneActionBuilder(SceneActionBuilder parent) {
            _action= new SceneAction();
            _parent = parent;
            _patternManager = BulletPatternBuilder.GetInstance();
        }

        public static void AddSequence(Scene scene, SceneAction action, int interval = 15, int number = 20, Vector2? intervalRange = null) {
            var startFrame = action.Delay;
            for (int i = 0; i < number; i++) {
                var newAction = action.Duplicate();
                newAction.Delay = startFrame;
                if (newAction.RandomEnterPosition) {
                    var xRange = newAction.RandXRange;
                    var yRange = newAction.RandYRange;
                    var randx = Math.Abs(xRange.x - xRange.y) < 0.01 ? xRange.x :Random.Range(xRange.x, xRange.y);
                    var randy = Math.Abs(yRange.x - yRange.y) < 0.01 ? yRange.x : Random.Range(yRange.x, yRange.y);
                    newAction.EnterPosition = new Vector2(randx, randy);
                }

                if (newAction.RandomColor)
                    newAction.EnemyColor = _colors[Random.Range(0, 3)];

                newAction.Shoots = Random.Range(0.0f, 1.0f) <= newAction.ShootProbability;
                
                scene.AddAction(newAction);
                if (intervalRange != null) {
                    var range = (Vector2)intervalRange;
                    startFrame += Random.Range((int)range.x, (int)range.y);
                }
                else {
                    startFrame += interval;
                }
            }
        }

        public static SceneActionBuilder NewAction() {
            return new SceneActionBuilder();
        }

        public SceneActionBuilder SetEnterPosition(Vector2 enterPosition) {
            if (_action.RandomEnterPosition) return this;
            _action.EnterPosition = enterPosition;
            return this;
        }

        public SceneActionBuilder SetRandomEnterPosition(Vector2 xRange, Vector2 yRange) {
            var randx = Math.Abs(xRange.x - xRange.y) < 0.01 ? xRange.x :Random.Range(xRange.x, xRange.y);
            var randy = Math.Abs(yRange.x - yRange.y) < 0.01 ? yRange.x : Random.Range(yRange.x, yRange.y);
            _action.RandomEnterPosition = true;
            _action.EnterPosition = new Vector2(randx, randy);
            _action.RandXRange = xRange;
            _action.RandYRange = yRange;
            return this;
        }

        public SceneActionBuilder SetEnemyColor(string color) {
            _action.EnemyColor = color;
            return this;
        }

        public SceneActionBuilder SetRandomEnemyColor() {
            _action.EnemyColor = _colors[Random.Range(0, 3)];
            _action.RandomColor = true;
            return this;
        }

        public SceneActionBuilder SetAngle(int angle) {
            _action.Angle = angle;
            return this;
        }

        public SceneActionBuilder SetSpeed(int speed) {
            _action.Speed = speed;
            return this;
        }

        public SceneActionBuilder SetAngleOffset(int angleOffset) {
            _action.AngleOffset = angleOffset;
            return this;
        }

        public SceneActionBuilder SetSpeedOffset(int speedOffset) {
            _action.SpeedOffset = speedOffset;
            return this;
        }

        public SceneActionBuilder SetSpeedOffset(float speedOffset) {
            _action.SpeedOffset = speedOffset;
            return this;
        }

        public SceneActionBuilder SetDelay(int delay) {
            _action.Delay = delay;
            return this;
        }

        public SceneActionBuilder AddPattern(BulletPattern pattern) {
            _action.Patterns.Add(pattern);
            if (Random.Range(0.0f, 1.0f) <= _action.ShootProbability) {
                _action.Shoots = true;
            }
            return this;
        }

        public SceneActionBuilder AddPattern(string patternName) {
            _action.Patterns.Add(_patternManager.GetPattern(patternName));
            if (Random.Range(0.0f, 1.0f) <= _action.ShootProbability) {
                _action.Shoots = true;
            }
            return this;
        }

        public SceneActionBuilder SetActionTime(int time) {
            _action.ActionTime = time;
            return this;
        }

        public SceneActionBuilder AddAction() {
            return new SceneActionBuilder(this);
        }

        public SceneActionBuilder SetHealth(int health) {
            _action.Health = health;
            return this;
        }

        public SceneActionBuilder SetHealthThreshold(float threshold) {
            _action.ActionHealthThreshold = threshold;
            return this;
        }

        public SceneActionBuilder SetDropItem(DropItemType type, int dropRate = 0) {
            _action.ItemType = type;
            _action.DropRate = dropRate;
            return this;
        }

        public SceneActionBuilder SetPrefabName(string name) {
            _action.ShooterPrefabName = name;
            return this;
        }

        public SceneActionBuilder AddChild(SceneAction action) {
            _action.Children.Add(action);
            return this;
        }

        public SceneActionBuilder SetShootProbability(float probability) {
            _action.ShootProbability = probability;
            if (Random.Range(0.0f, 1.0f) > probability) {
                _action.Shoots = false;
            }
            return this;
        }

        public SceneAction Build() {
            var result = this;
            while (result._parent != null) {
                result._parent._action.NextAction = result._action;
                result = result._parent;
            }
            
            return result._action;
        }
    }
}