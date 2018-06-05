using System.Collections.Generic;
using UnityEngine;

namespace Code.Danmaku {
    public class Shooter : MonoBehaviour, Collider2DIntf {
        public ShooterController ParentController;
        public Shooter ParentShooter;
        public List<Shooter> Children = new List<Shooter>();

//        public BulletPattern BulletPattern;
        public Action Action = new Action();

        public int LivedFrame = 0;
        public int FullHealth = 35;
        public int Health = 35;
        public bool Dead = false;
        public float Direction = 0;
        public string Color = "magenta"; //default to magenta
        public Avatar_Control _parentAv;
        public DropItemType DropItemType;
        public int DropRate;
        private GameObject _healthItemPrefab;
        private GameObject _powerPrefab;
        public bool powered;
        private Color _currentColor;
        private bool _isShot = false;
        private int _colorChanged = 0;
        private SpriteRenderer _sprite;
        public AudioClip boom;
        public AudioSource music;

        private void Start() {
            _healthItemPrefab = Resources.Load<GameObject>("HealthItem");
            _powerPrefab = Resources.Load<GameObject>("PowerUp");

            _parentAv = FindObjectOfType<Avatar_Control>();
            _sprite = GetComponent<SpriteRenderer>();
            _currentColor = _sprite.color;
            powered = true;
            music = GameObject.FindGameObjectWithTag("sfx").GetComponent<AudioSource>();
            boom = Resources.Load<AudioClip>("kaboom");
        }

        public void UpdatePatterns() {
            if (_isShot) {
                _sprite.color = UnityEngine.Color.red;
                _isShot = false;
                _colorChanged = 7;
            }

            if (_colorChanged < 0) {
                _sprite.color = _currentColor;
                _colorChanged = 0;
            }
            else if (_colorChanged > 0) {
                _colorChanged -= 2;
            }

            var completed = true;
            foreach (var pattern in Action.BulletPatterns) {
                if (!pattern.Completed) {
                    completed = false;
                    break;
                }
            }

            if (completed) return;

            foreach (var pattern in Action.BulletPatterns) {
                if (!pattern.Playing) {
                    // try to activate pattern
                    if (pattern.Trigger.Activate(pattern)) {
                        pattern.Playing = true;
                        pattern.CurrentShootAngle = pattern.Angle;
                        pattern.CurrentBaseAngle = pattern.Angle;
                    }
                }
                else {
                    if (IsCoolingDown(pattern)) {
//                        if (pattern.Trigger.CanShoot(pattern.CurrentFrame)) {
//                            CalcAngle(pattern);
//                        }
                        return;
                    }


                    // shoot according to pattern
                    if (pattern.Trigger.CanShoot(pattern.CurrentFrame)) {
                        // Shoot!!!!!
                        ShootBullet(pattern);
                    }

                    // When we get to the last frame of the pattern
                    if (pattern.CurrentFrame + 1 == pattern.IntervalFrame) {
                        // This is a Repeat style of shooting.
                        // We can also set a `end` flag in Pattern to make it
                        // just shoot once or fixed times
                        var triggerType = pattern.Trigger.Type;
                        if (triggerType == TriggerType.ONCE) {
                            pattern.Completed = true;
                        }
                        else if (triggerType == TriggerType.REPEAT) {
                            // Needs to cool down
                            //                        BulletPattern.CurrentFrame = 0;
                            pattern.CoolingDown = true;
                            pattern.CurrentFrame = pattern.CoolDownFrame;
                            pattern.CurrentBaseAngle += pattern.LoopAngleOffset;
                            pattern.CurrentBaseAngle = pattern.CurrentBaseAngle > 360
                                ? pattern.CurrentBaseAngle - 360
                                : pattern.CurrentBaseAngle;
                            pattern.CurrentShootAngle = pattern.CurrentBaseAngle;
                        }
                    }
                    else {
                        ++pattern.CurrentFrame;
                    }
                }
            }
        }

        private void ShootBullet(BulletPattern pattern) {
            if (ParentController != null) {
                var defaultAngle = pattern.CurrentShootAngle;
                // Are we locking on avatar?
                if (pattern.LockedOnAvatar) {
                    var avaPos = ParentController.GetAvartarPosition();
                    var pos = new Vector2(
                        transform.position.x + pattern.PosOffset.x,
                        transform.position.y + pattern.PosOffset.y);

                    if (avaPos != null) {
                        var targetVec = new Vector3(((Vector2) avaPos).x - pos.x, ((Vector2) avaPos).y - pos.y, 0);
                        var baseVec = new Vector3(1, 0, 0);
                        // Change our angle and Aim to Avatar
                        if (Vector3.Cross(baseVec, targetVec).z >= 0) {
                            defaultAngle = (int) Vector2.Angle(baseVec, targetVec);
                        }
                        else {
                            defaultAngle = -(int) Vector2.Angle(baseVec, targetVec);
                        }
                    }
                }

                defaultAngle += Random.Range(pattern.RandomAngleRange.x, pattern.RandomAngleRange.y);

                // code to deal with N-way bullet
                if (pattern.BulletCount % 2 == 0) {
                    var angleCorrection = pattern.BulletAngle / 2;
                    var stepper = 0;
                    var pos = new Vector2(
                        transform.position.x + pattern.PosOffset.x,
                        transform.position.y + pattern.PosOffset.y);
                    for (int i = 0; i < pattern.BulletCount; i = i + 2) {
                        var angle = defaultAngle + angleCorrection + stepper;
                        ParentController.ShootBullet(
                            pos, angle, pattern.DestroyableProportion, 
                            pattern.SpeedCorrection, pattern.SpeedCorrectionOffset, pattern.RandomSpeedRange);
                        angle = defaultAngle - angleCorrection - stepper;
                        ParentController.ShootBullet(
                            pos, angle, pattern.DestroyableProportion, 
                            pattern.SpeedCorrection, pattern.SpeedCorrectionOffset, pattern.RandomSpeedRange);
                        stepper += pattern.BulletAngle;
                    }
                }
                else {
                    var stepper = pattern.BulletAngle;
                    var pos = new Vector2(
                        transform.position.x + pattern.PosOffset.x,
                        transform.position.y + pattern.PosOffset.y);
                    ParentController.ShootBullet(
                        pos, defaultAngle, pattern.DestroyableProportion, 
                        pattern.SpeedCorrection, pattern.SpeedCorrectionOffset, pattern.RandomSpeedRange);
                    for (int i = 1; i < pattern.BulletCount; i = i + 2) {
                        var angle = defaultAngle + stepper;
                        ParentController.ShootBullet(
                            pos, angle, pattern.DestroyableProportion, 
                            pattern.SpeedCorrection, pattern.SpeedCorrectionOffset, pattern.RandomSpeedRange);
                        angle = defaultAngle - stepper;
                        ParentController.ShootBullet(
                            pos, angle, pattern.DestroyableProportion, 
                            pattern.SpeedCorrection, pattern.SpeedCorrectionOffset, pattern.RandomSpeedRange);
                        stepper += pattern.BulletAngle;
                    }
                }
                CalcAngle(pattern);
            }
        }

        private void CalcAngle(BulletPattern pattern) {
            pattern.CurrentShootAngle += pattern.AngleOffset;
            pattern.AngleOffset += pattern.AngleOffsetStepper;
            pattern.AngleOffset = pattern.AngleOffset > 360 ? pattern.AngleOffset - 360 : pattern.AngleOffset;
            pattern.CurrentShootAngle = pattern.CurrentShootAngle > 360
                ? pattern.CurrentShootAngle - 360
                : pattern.CurrentShootAngle;
        }

        private static bool IsCoolingDown(BulletPattern pattern) {
            if (!pattern.CoolingDown) return false;

            pattern.CurrentFrame--;
            if (pattern.CurrentFrame <= 0) {
                pattern.CoolingDown = false;
            }

            return true;
        }

        public void OnCollision(GameObject gObject) {
            
            if (gObject.layer == 9 || gObject.layer == 14) {
                if (gObject.layer == 9) {
                    var bullet = gObject.GetComponentInParent<Av_Bullet>();
                    if (bullet.Color.Equals(Color)) {
                        switch (bullet.PowerLevel) {
                            case 0:
                                Health -= 100;
                                break;
                            case 1:
                                Health -= 60;
                                break;
                            case 2:
                                Health -= 50;
                                break;
                        }

                        _parentAv.Score += 0.5f;
                        _isShot = true;

                        float position = 18f * Health / FullHealth - 18f;
                        DanmakuController.Instance.updateEnemyHealth(position);
                    }
                    else {
                        switch (bullet.PowerLevel) {
                            case 0:
                                Health -= 40;
                                break;
                            case 1:
                                Health -= 30;
                                break;
                            case 2:
                                Health -= 25;
                                break;
                        }

                        _isShot = true;

                        float position = 18f * Health / FullHealth - 18f;
                        DanmakuController.Instance.updateEnemyHealth(position);
                    }
                }
                else {
                    // Special Attack
                    Health -= 3000;

                    float position = 18f * Health / FullHealth - 18f;
                    DanmakuController.Instance.updateEnemyHealth(position);
                }

                if (Health <= 0) {
                    var rng = Random.Range(0, 100);
                    switch (DropItemType) {
                        case DropItemType.Health:
                            if (rng < DropRate) {
                                var healthObject = Instantiate(_healthItemPrefab, transform.position,
                                    Quaternion.identity);
                                var healthItem = healthObject.AddComponent<HealthItem>();

                                healthItem.Direction = -90 * Mathf.Deg2Rad;
                                healthItem.gameObject.layer = 12;

                                ParentController.AddHealthItem(healthItem);
                                _parentAv.itemTime = Time.time;
                            }
                            break;
                        case DropItemType.Powerup:
                            if (rng < DropRate) {
                                var powObject = Instantiate(_powerPrefab, transform.position, Quaternion.identity);
                                var powItem = powObject.AddComponent<HealthItem>();

                                powItem.Direction = -90 * Mathf.Deg2Rad;
                                powItem.Speed = 3;
                                powItem.gameObject.layer = 13;

                                ParentController.AddHealthItem(powItem);
                                _parentAv.itemTime = Time.time;
                            }
                            break;
                        case DropItemType.Random:
                            if (Time.time > _parentAv.itemTime + 2f) {
                                if (rng < 10) {
                                    var healthObject = Instantiate(_healthItemPrefab, transform.position,
                                        Quaternion.identity);
                                    var healthItem = healthObject.AddComponent<HealthItem>();

                                    healthItem.Direction = -90 * Mathf.Deg2Rad;
                                    healthItem.gameObject.layer = 12;

                                    ParentController.AddHealthItem(healthItem);
                                    _parentAv.itemTime = Time.time;
                                }

                                // CHANCE TO GET POWER UP INSTEAD OF HEALTH ITEM
                                if (rng >= 10 && rng < 14) {
                                    var powObject = Instantiate(_powerPrefab, transform.position, Quaternion.identity);
                                    var powItem = powObject.AddComponent<HealthItem>();

                                    powItem.Direction = -90 * Mathf.Deg2Rad;
                                    powItem.Speed = 3;
                                    powItem.gameObject.layer = 13;

                                    ParentController.AddHealthItem(powItem);
                                    _parentAv.itemTime = Time.time;
                                }
                            }

                            break;
                    }


                    DieWell();
                }
            }

            if (gObject.layer == 11) {
                DiePoor();
            }

//            if (gObject.layer == 14)
//            {
//                DiePoor();
//            }
            if (ParentShooter != null) {
                ParentShooter.OnCollision(gObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            OnCollision(other.gameObject);
        }


        void DiePoor() {
            music.PlayOneShot(boom,0.6f);
            Debug.Log("die poor");
            DestroyChildren();
            Destroy(gameObject);
        }

        void DieWell() {
            music.PlayOneShot(boom,0.6f);
            Debug.Log("die well");
            _parentAv.Score += 5f;
            if (Time.time <= _parentAv.comboTime + 3f) {
                _parentAv.Score += 5f;
            }

            _parentAv.comboTime = Time.time;
            DestroyChildren();
            Destroy(gameObject);
        }

        void DestroyChildren() {
            Children.RemoveAll(s => s == null);
            foreach (var shooter in Children) {
                shooter.DieWell();
            }
        }
    }
}