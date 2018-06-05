using Code.Danmaku;
using UnityEngine;

namespace Code {
    public class HealthItem: FlyObject {
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.layer == 11) {
                Destroy(gameObject);
            }
        }
    }
}