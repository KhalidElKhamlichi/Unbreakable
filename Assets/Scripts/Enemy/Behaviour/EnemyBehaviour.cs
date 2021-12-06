using UnityEngine;

namespace Unbreakable.Enemy.Behaviour {
    public abstract class EnemyBehaviour : ScriptableObject {
        
        protected EnemyAI enemy;
        protected Transform target;

        public virtual void initialize(EnemyAI enemy, Transform target) {
            this.enemy = enemy;
            this.target = target;
        }
        public abstract void update();
    }
}