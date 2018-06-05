using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Danmaku {
    public class Scene {
        public int EnterFrame;
        public int MaxDuration;
        public int MinDuration;
        public int CoolingTime;
        public bool Ended = false;
        public bool Acting = false;
        public bool Activated = false;

        private int _currentFrame;
        private int _coolingFrame = 0;
        private int _startFrame;
        private bool _isEnded;
        private bool _noActor = false;
        private int _currentDelayOffset;
        private List<SceneAction> _actions = new List<SceneAction>();

        public Scenario Parent;

        public int UpdateActions(int currentFrame, bool noActor) {
            if (Ended) return currentFrame;
            if (currentFrame < EnterFrame && !Activated) return currentFrame;

            Ended = true;
            if (noActor) _noActor = true;

            if (MaxDuration < _currentFrame) return currentFrame;

            foreach (SceneAction action in _actions) {
                if (action.Acted) continue;

                if (action.Delay <= _currentFrame || _noActor) {
                    AddShooter(action);
                    
                    action.Acted = true;

                    if (!Acting) {
                        Acting = true;
                        _startFrame = currentFrame;
                    }
            
                    if (_noActor) {
                        _noActor = false;
                        _currentFrame += action.Delay + _startFrame - currentFrame;
                        currentFrame = action.Delay + _startFrame;
                    }
                }

                Ended = false;
            }
            
            _actions.RemoveAll(a => a == null || a.Acted);

            if (MinDuration > _currentFrame) Ended = false;

            if (!_noActor) Ended = false;

            if (Ended && _coolingFrame < CoolingTime) {
                _coolingFrame++;
                Ended = false;
            }

            if (Ended) Acting = false;

            ++_currentFrame;

            return currentFrame;
        }

        private Shooter AddShooter(SceneAction action, Shooter parent = null) {
            GameObject go = Object.Instantiate(Resources.Load<GameObject>(action.ShooterPrefabName));
            var s = go.AddComponent<Shooter>();
            
            var color = action.EnemyColor;
            if (color.Equals("magenta")) {
                go.GetComponent<SpriteRenderer>().color = Color.magenta;
                s.Color = "Magenta";
            }else if (color.Equals("cyan")) {
                go.GetComponent<SpriteRenderer>().color = Color.cyan;
                s.Color = "Cyan";
            }else if (color.Equals("yellow")) {
                go.GetComponent<SpriteRenderer>().color = Color.yellow;
                s.Color = "Yellow";
            }
            s.gameObject.layer = 10;

            s.transform.position = new Vector3(action.EnterPosition.x, action.EnterPosition.y);
//                    s.BulletPattern = action.Pattern.Duplicate();

            var a = s.Action;
            var sa = action;

            s.Health = sa.Health;
            s.FullHealth = sa.Health;
            s.DropItemType = sa.ItemType;
            s.DropRate = sa.DropRate;
            s.ParentShooter = parent;
            
            a.Angle = sa.Angle;
            a.AngleOffset = sa.AngleOffset;
            a.CurrentAngle = sa.Angle;
            a.Speed = sa.Speed;
            a.SpeedOffset = sa.SpeedOffset;
            a.CurrentSpeed = sa.Speed;
            a.ActionTime = sa.ActionTime;
            a.ActionHealthThreshold = sa.ActionHealthThreshold;
            a.BulletPatterns = new List<BulletPattern>();
            foreach (var saPattern in sa.Patterns) {
                a.BulletPatterns.Add(saPattern.Duplicate());
            }

            while (sa.NextAction != null) {
                sa = sa.NextAction;
                a.NextAction = new Action();
                a = a.NextAction;
                
                a.Angle = sa.Angle;
                a.AngleOffset = sa.AngleOffset;
                a.CurrentAngle = sa.Angle;
                a.Speed = sa.Speed;
                a.SpeedOffset = sa.SpeedOffset;
                a.CurrentSpeed = sa.Speed;
                a.ActionTime = sa.ActionTime;
                a.ActionHealthThreshold = sa.ActionHealthThreshold;
                a.BulletPatterns = new List<BulletPattern>();
                foreach (var saPattern in sa.Patterns) {
                    a.BulletPatterns.Add(saPattern.Duplicate());
                }
            }
            

            Parent.AddShooter(s);

            foreach (var actionChild in action.Children) {
                s.Children.Add(AddShooter(actionChild, s));
            }

            return s;
        }

        public void SortActions() {
            _actions = _actions.OrderBy(a => a.Delay).ToList();
        }

        public void AddAction(SceneAction action) {
            _actions.Add(action);
        }
    }
}