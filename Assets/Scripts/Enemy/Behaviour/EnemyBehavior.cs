using UnityEngine;

namespace Unbreakable.Enemy.Behaviour {
    public abstract class EnemyBehavior : ScriptableObject {
        
        protected MonoBehaviour enemy;
        protected Transform target;

        public virtual void initialize(MonoBehaviour enemy, Transform target) {
            this.enemy = enemy;
            this.target = target;
        }
        public abstract void update();
    }
}