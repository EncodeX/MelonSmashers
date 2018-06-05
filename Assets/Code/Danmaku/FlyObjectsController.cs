using System.Collections.Generic;
using UnityEngine;

namespace Code.Danmaku {
    public class FlyObjectsController : MonoBehaviour {
        private readonly List<FlyObject> _flyObjects = new List<FlyObject>();
        private DanmakuController _parent;

        public void Init(DanmakuController parent) {
            _parent = parent;
        }

        public void ClearObjects() {
            foreach (FlyObject flyObject in _flyObjects) {
                if (flyObject != null) {
                    Destroy(flyObject.gameObject);
                }
            }

            _flyObjects.Clear();
        }

        public void UpdateObjects() {
            List<FlyObject> deadObjects = new List<FlyObject>();
            Rect cameraRect = Utils.ExpandRect(Utils.GetGamePlayRect(_parent.MainCamera), 2.0f);

            foreach (FlyObject flyObject in _flyObjects) {
                // ?! null bullet in List??
                if (flyObject == null) {
                    continue;
                }

                // dead bullets are dead
                if (flyObject.Dead) {
                    deadObjects.Add(flyObject);
                }
                else {
                    // while other bullets are still flying
                    Vector3 pos = flyObject.gameObject.transform.position;

                    var speed = flyObject.Speed * flyObject.SpeedCorrection;
                    flyObject.SpeedCorrection += flyObject.SpeedCorrectionOffset;
                    
                    float dist = speed * Utils.unitPerPixel;

                    flyObject.gameObject.transform.position = new Vector3(
                        pos.x + dist * Mathf.Cos(flyObject.Direction),
                        pos.y + dist * Mathf.Sin(flyObject.Direction),
                        pos.z
                    );

                    if (!cameraRect.Contains(flyObject.gameObject.transform.position) ||
                        flyObject.LifeFrame != 0 && flyObject.LivedFrame > flyObject.LifeFrame) {
                        flyObject.Dead = true;
                        deadObjects.Add(flyObject);
                    }

                    ++flyObject.LivedFrame;
                }
            }

            // get rid of all dead bullets
            foreach (FlyObject flyObject in deadObjects) {
                if (flyObject != null) {
                    Destroy(flyObject.gameObject);
                }
            }

            _flyObjects.RemoveAll(b => b == null || b.Dead);
        }

        public void AddObject(FlyObject flyObject) {
            _flyObjects.Add(flyObject);
            flyObject.transform.parent = transform;
        }
    }
}