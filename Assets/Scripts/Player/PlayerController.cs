using DG.Tweening;
using UnityEngine;

namespace Unbreakable.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour {
        
        private Rigidbody2D rbody;
        private bool isKnockedBack;
        
        private void Start() {
            rbody = GetComponent<Rigidbody2D>();
        }

        public void move(Vector2 moveAmount) {
            rbody.velocity = moveAmount * Time.deltaTime;
        }

        public void knockback(Vector2 knockbackVelocity, float duration) {
            rbody.DOMove((Vector2)transform.position + knockbackVelocity, duration);
        }
    }
}
