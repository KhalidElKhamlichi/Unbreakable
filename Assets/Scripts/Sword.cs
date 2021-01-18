using UnityEngine;

namespace Unbreakable {
    public class Sword : Weapon, Damager
    {
        [SerializeField] private int damage;
    
        public override void attack() {
            base.attack();
            GetComponent<Collider2D>().enabled = true;
            Destroy(gameObject, .5f);
        }

        public int getDamage() {
            return damage;
        }

        public float getKnockbackForce() {
            return 50;
        }

        public override void drop() {
            base.drop();
            GetComponent<CapsuleCollider2D>().enabled = false;
        }

        public bool isPickable() {
            return pickable;
        }
    
        private void resetPickable() {
            pickable = true;
        }
    }
}
