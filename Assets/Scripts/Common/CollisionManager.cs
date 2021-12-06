using System;
using System.Collections;
using Unbreakable.Combat;
using UnityEngine;

namespace Unbreakable.Common {
    public class CollisionManager : MonoBehaviour {
        [SerializeField] private string hitTriggerTag;
        private event Action<HitInfo> hitEvent;
        private string ignoreTag = string.Empty;

        private void OnTriggerEnter2D(Collider2D other) {
            invokeHitEvent(new HitInfo(other.GetComponent<Damager>(), other.tag, transform.position - other.transform.position));
        }
        
        private void OnTriggerStay2D(Collider2D other) {
            invokeHitEvent(new HitInfo(other.GetComponent<Damager>(), other.tag, transform.position - other.transform.position));
        }

        public void invokeHitEvent(HitInfo hitInfo) {
            if (hitInfo.getTag().Contains(hitTriggerTag) && !ignoreTag.Contains(hitInfo.getTag())) {
                hitEvent?.Invoke(hitInfo);
            }
        }
        
        public IEnumerator disableCollisionWith(string tag, float duration) {
            ignoreTag = tag;
			
            yield return new WaitForSeconds(duration);

            ignoreTag = string.Empty;
        }

        public void onHit(Action<HitInfo> onHit) => hitEvent += onHit;
            
    }
}
