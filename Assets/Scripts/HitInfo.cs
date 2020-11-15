using UnityEngine;

namespace Unbreakable {
    public class HitInfo {
        private readonly Damager damager;
        private readonly string tag;
        private readonly Vector2 collisionDirection;

        public HitInfo(Damager damager, string tag, Vector2 collisionDirection) {
            this.damager = damager;
            this.tag = tag;
            this.collisionDirection = collisionDirection;
        }

        public Damager getDamager() {
            return damager;
        }

        public string getTag() {
            return tag;
        }

        public Vector2 getCollisionDirection() {
            return collisionDirection;
        }
        
    }
}