using UnityEngine;

namespace Code {
    public class Collider2D : MonoBehaviour {
        private Collider2DIntf _parent;
        
        private void Start() {
            _parent = transform.parent.gameObject.GetComponent<Collider2DIntf>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            //if (!other.gameObject.CompareTag("skewR"))
            //{
                _parent.OnCollision(other.gameObject);
            //}

        }
    }
}