using UnityEngine;

namespace Unbreakable {
    public class Firearm : Weapon {
        [SerializeField] private float firerate;
        [SerializeField] private FiringStrategy firingStrategy;

        private Transform emissionPoint;
        private float timer;

        protected override void Start() {
            base.Start();
            emissionPoint = transform.GetChild(0);
        }

        private void Update() {
            timer -= Time.deltaTime;
        }
        public override void attack() {
            if (timer > 0) return;
            firingStrategy.shoot(emissionPoint);
        
            timer = 1 / firerate;
            base.attack();
            if (remainingUses == 0) {
                usable = false;
                Destroy(gameObject);
            }
        }
    }
}
