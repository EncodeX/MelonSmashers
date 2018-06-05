using System.Collections.Generic;
using Code.Danmaku.Triggers;
using UnityEngine;

namespace Code.Danmaku {
    public enum DropItemType {
        Health,
        Powerup,
        Random,
        None
    }
    
    public class SceneAction {
        public int Delay = 0;
        public int CrowdDelay = 0;
        public int Angle = -90;
        public int Speed = 5;
        public int AngleOffset = 0;
        public float SpeedOffset = 0;
		public string EnemyColor = "magenta"; //default to magenta
        public bool Acted = false;
        public Vector2 EnterPosition = new Vector2(11, 11);
        public List<BulletPattern> Patterns = new List<BulletPattern>();
        public int Health = 190;
        public DropItemType ItemType = DropItemType.Random;
        public int DropRate = 0;
        public string ShooterPrefabName = "Enemy";
        public List<SceneAction> Children = new List<SceneAction>();
        public bool RandomEnterPosition = false;
        public Vector2 RandXRange = new Vector2();
        public Vector2 RandYRange = new Vector2();
        public bool RandomColor = false;
        public float ShootProbability = 1;
        public bool Shoots = true;

        public int ActionTime = -1;
        public float ActionHealthThreshold = 0;
        public SceneAction NextAction = null;

        public SceneAction Duplicate() {
            var result = new SceneAction {
                Delay = Delay,
                CrowdDelay = CrowdDelay,
                Angle = Angle,
                Speed = Speed,
                AngleOffset = AngleOffset,
                SpeedOffset = SpeedOffset,
                EnemyColor = EnemyColor,
                Acted = Acted,
                EnterPosition = new Vector2(EnterPosition.x, EnterPosition.y),
                ActionTime = ActionTime,
                ActionHealthThreshold = ActionHealthThreshold,
                NextAction = NextAction == null ? null : NextAction.Duplicate(),
                Patterns = new List<BulletPattern>(),
                Health = Health,
                ItemType = ItemType,
                DropRate = DropRate,
                ShooterPrefabName = ShooterPrefabName,
                Children = new List<SceneAction>(),
                RandomEnterPosition = RandomEnterPosition,
                RandomColor = RandomColor,
                RandXRange = new Vector2(RandXRange.x, RandXRange.y),
                RandYRange = new Vector2(RandYRange.x, RandYRange.y),
                ShootProbability = ShootProbability,
                Shoots = Shoots
            };
            foreach (var pattern in Patterns) {
                result.Patterns.Add(pattern.Duplicate());
            }

            foreach (var action in Children) {
                result.Children.Add(action.Duplicate());
            }
            return result;
        }
    }
}