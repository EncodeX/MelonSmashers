using System.Collections.Generic;
using Code.Danmaku.Triggers;
using UnityEngine;

namespace Code.Danmaku {
    public class ShooterController : MonoBehaviour {
        private List<Shooter> _shooters = new List<Shooter>();
        private DanmakuController _danmakuController;
        private FlyObjectsController _flyObjectsController;

        private GameObject _bulletParticlePrefab;
        private GameObject _bulletHollowPrefab;

        public void Init(DanmakuController danmakuController) {
            _danmakuController = danmakuController;
            _bulletParticlePrefab = Resources.Load("_En_Bullet_Particle") as GameObject;
            _bulletHollowPrefab = Resources.Load<GameObject>("_En_Bullet_Hollow");
            _flyObjectsController = _danmakuController.FlyObjectsController;
        }

        public void ClearShooters() {
            // get rid of all remaining shooters
            foreach (Shooter shooter in _shooters) {
                if (shooter) {
                    Destroy(shooter.gameObject);
                }
            }

            _shooters.Clear();
        }

        public void AddShooter(Shooter shooter) {
            shooter.ParentController = this;
            shooter.transform.parent = transform;
            _shooters.Add(shooter);
        }

        public void UpdateShooters() {
            List<Shooter> deadShooters = new List<Shooter>();
            Rect shootingRect = Utils.GetGamePlayRect(_danmakuController.MainCamera);
            Rect livingRect = Utils.ExpandRect(Utils.GetGamePlayRect(_danmakuController.MainCamera), 2.0f);

            foreach (Shooter shooter in _shooters) {
                if (shooter == null)
                    continue;

                if (shooter.Dead) {
                    deadShooters.Add(shooter);
                }
                else {
                    if (shooter.Action.ActionTime != -1) {
                        shooter.Action.CurrentFrame++;
                        if (shooter.Action.CurrentFrame == shooter.Action.ActionTime) {
                            shooter.Action = shooter.Action.NextAction;
                        }
                    }

                    if (shooter.Action.ActionHealthThreshold > 0) {
                        if (shooter.Health < shooter.FullHealth * shooter.Action.ActionHealthThreshold) {
                            if (shooter.Action.NextAction != null) {
                                shooter.Action = shooter.Action.NextAction;
                            }
                        }
                    }
                    shooter.Action.CalcDirection();
                    shooter.Action.CalcSpeed();
                    
                    Vector3 pos = shooter.gameObject.transform.position;
                    float dist = shooter.Action.CurrentSpeed * Utils.unitPerPixel;

                    shooter.gameObject.transform.position = new Vector3(
                        pos.x + dist * Mathf.Cos(shooter.Action.Direction),
                        pos.y + dist * Mathf.Sin(shooter.Action.Direction),
                        pos.z
                    );

                    pos = shooter.gameObject.transform.position;

                    if (!livingRect.Contains(new Vector2(pos.x, pos.y))) {
                        shooter.Dead = true;
                        deadShooters.Add(shooter);
                    }
                }

                Vector3 shooterPos = shooter.gameObject.transform.position;
                if (shootingRect.Contains(new Vector2(shooterPos.x, shooterPos.y)))
                    shooter.UpdatePatterns();
            }

            foreach (Shooter shooter in deadShooters) {
                if (shooter != null) {
                    Destroy(shooter.gameObject);
                }
            }

            _shooters.RemoveAll(s => s == null || s.Dead);

            if (_shooters.Count == 0) {
                DanmakuController.Instance.OnNoShooter();
            }
        }

        public void ShootBullet(Vector2 position, float angle, int desProportion, float spdCorrection, float spdCorrectionOffset, Vector2? speedRange) {
            // create a new bullet and leave them to bullet controller
            var destroyable = !(desProportion == 0 || Random.Range(0, 100) > desProportion);
            var bulletObject = Instantiate(destroyable ? _bulletParticlePrefab : _bulletHollowPrefab);

            var bullet = bulletObject.AddComponent<Bullet>();

            bullet.Direction = angle * Mathf.Deg2Rad;
            bullet.gameObject.layer = 8;
            bullet.transform.position = new Vector3(position.x, position.y, 0);
            if (speedRange != null) bullet.Speed = Random.Range(((Vector2) speedRange).x, ((Vector2) speedRange).y);
            bullet.SpeedCorrection = spdCorrection;
            bullet.SpeedCorrectionOffset = spdCorrectionOffset;

            if (!destroyable) {
                bullet.Destroyable = false;
//                bulletObject.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
            }

            _flyObjectsController.AddObject(bullet);
        }

        public Vector2? GetAvartarPosition() {
            if (_danmakuController.Avatar == null)
                return null;
            var pos = _danmakuController.Avatar.transform.position;
            return new Vector2(pos.x, pos.y);
        }

        public void AddHealthItem(HealthItem item) {
            _flyObjectsController.AddObject(item);
        }
    }
}