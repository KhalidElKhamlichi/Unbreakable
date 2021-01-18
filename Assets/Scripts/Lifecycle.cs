using System;
using UnityEngine;

namespace Unbreakable {
    [RequireComponent(typeof(CollisionManager))]
    public class Lifecycle : MonoBehaviour {
        [SerializeField] int maxHP;
    
        private event Action<GameObject> deathEvent;
        private event Action<int> takeDamageEvent;
        private int currentHP;

        protected virtual void Start() {
            currentHP = maxHP;
            GetComponent<CollisionManager>().onHit(takeDamage);
        }

        public void onDeath(Action<GameObject> onDeath) {
            deathEvent += onDeath;
        }
    
        public void onTakeDamage(Action<int> onTakeDamage) {
            takeDamageEvent += onTakeDamage;
        }

        protected virtual void takeDamage(HitInfo hit) {
            Damager damager = hit.getDamager();
            if (damager == null) return;
            currentHP -= damager.getDamage();
            takeDamageEvent?.Invoke(currentHP);
            checkLife();
        }

        private void checkLife() {
            if (currentHP <= 0) {
                takeDamageEvent = null;
                onDeath();
            }
        }

        protected virtual void onDeath() {
            GetComponent<Collider2D>().enabled = false;
            deathEvent?.Invoke(gameObject);
            deathEvent = null;
            Destroy(gameObject, .5f);
        }

        public float getMaxHp() {
            return maxHP;
        }

    }
}
