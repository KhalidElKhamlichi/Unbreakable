using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.Combat {
    [CreateAssetMenu]
    public class FiringStrategy : ScriptableObject {
    
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileSpread = 0.2f;
        [SerializeField] private float projSpeed = 500.0f;
        [SerializeField] private int nbrOfProjectiles;
        [SerializeField] private float spreadAngle;

        public void shoot(Transform emissionPoint) {
            float spread = 0;
            for (int i = 0; i < nbrOfProjectiles; i++) {
                // Increment for every other extra projectile
                if (i % 2 != 0) {
                    spread += Mathf.Sign(spread) * spreadAngle;
                }

                // permute between each side
                spread *= -1;
                spread += Random.Range(-projectileSpread, projectileSpread);
                instantiateProjectile(projectile, spread, emissionPoint);
            }
        }

        private void instantiateProjectile(GameObject projectile, float spread, Transform emissionPoint) {
            // rotate weapon
            emissionPoint.Rotate(0, 0, spread);
            
            GameObject projectileClone = Instantiate(projectile, emissionPoint.position, emissionPoint.rotation);
            Rigidbody2D rbody = projectileClone.GetComponent<Rigidbody2D>();
            rbody.AddForce(emissionPoint.transform.right * projSpeed);
            
            // reset rotation
            emissionPoint.Rotate(0, 0, -spread);
        }
    }
}
