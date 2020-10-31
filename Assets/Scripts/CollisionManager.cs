using System;
using System.Collections;
using UnityEngine;

public class CollisionManager : MonoBehaviour {
    [SerializeField] string hitTriggerTag;
    private event Action<Collider2D> hitEvent;
    private string ignoreTag = string.Empty;

    private void OnTriggerEnter2D(Collider2D other) {
        invokeHitEvent(other);
    }
        
    private void OnTriggerStay2D(Collider2D other) {
        invokeHitEvent(other);
    }

    private void invokeHitEvent(Collider2D other) {
        if (other.tag.Equals(hitTriggerTag) && !ignoreTag.Equals(other.tag)) {
            hitEvent?.Invoke(other);
        }
    }
        
    public IEnumerator disableCollisionWith(string tag, float duration) {
        ignoreTag = tag;
			
        yield return new WaitForSeconds(duration);

        ignoreTag = string.Empty;
    }

    public void onHit(Action<Collider2D> onHit) => hitEvent += onHit;
            
}
