using System;
using System.Collections;
using UnityEngine;

public class CollisionManager : MonoBehaviour {
    [SerializeField] string hitTriggerTag;
    private event Action<Collider2D> hitEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals(hitTriggerTag)) {
            hitEvent?.Invoke(other);
        }
    }
        
    private void OnTriggerStay2D(Collider2D other) {
        invokeHitEvent(other);
    }

    private void invokeHitEvent(Collider2D other) {
        if (other.tag.Equals(hitTriggerTag)) {
            hitEvent?.Invoke(other);
        }
    }
        
    public IEnumerator disableCollisionWith(int layer, float duration)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 9, true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 12, true);
			
        yield return new WaitForSeconds(duration);

        Physics2D.IgnoreLayerCollision(gameObject.layer, 9, false);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 12, false);
    }

    public void onHit(Action<Collider2D> onHit) => hitEvent += onHit;
            
}
