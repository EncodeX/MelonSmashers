using UnityEngine;

namespace Code.Danmaku {
    /// <summary>
    /// Bullet Class, stores all data for a flying bullet
    /// </summary>
    public class Bullet : FlyObject, Collider2DIntf {
        public bool Destroyable = true;

        public void OnCollision(GameObject gObject) {
            if (gObject.layer == 9) {
//                var bullet = gObject.GetComponentInParent<Av_Bullet>();
                //if (bullet.PatternType == 1) return;
                //if (!gameObject.CompareTag("skewR"))
                //{
                if (Destroyable) Destroy(gameObject);
                //}
            }

            if (gObject.layer == 11 || gObject.layer == 14) {
                //if (!gameObject.CompareTag("skewR"))
                //{
                Destroy(gameObject);
                //}
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            OnCollision(other.gameObject);
        }
    }
}