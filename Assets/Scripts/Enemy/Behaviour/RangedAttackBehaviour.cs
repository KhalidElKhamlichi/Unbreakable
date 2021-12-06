using System.Collections;
using UnityEngine;

namespace Unbreakable.Enemy.Behaviour {
    [CreateAssetMenu]
    public class RangedAttackBehaviour : EnemyBehaviour
    {
        private const string LEFT_EMISSION_POINT_NAME = "emissionPointLeft";
        private const string RIGHT_EMISSION_POINT_NAME = "emissionPointRight";
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private GameObject projectile;
        [SerializeField] private float firerate;
        [SerializeField] private float projSpeed;

        private Vector2 direction;
        private Rigidbody2D rbody;
        private float timer;
        private Animator animator;
        private Transform emissionPointLeft;
        private Transform emissionPointRight;
    
        public override void initialize(EnemyAI enemy, Transform target) {
            base.initialize(enemy, target);
            rbody = enemy.GetComponent<Rigidbody2D>();
            animator = enemy.GetComponent<Animator>();
            emissionPointLeft = enemy.transform.Find(LEFT_EMISSION_POINT_NAME);
            emissionPointRight = enemy.transform.Find(RIGHT_EMISSION_POINT_NAME);
        }

        public override void update() {
            direction = ((Vector2)target.position - rbody.position).normalized;
            timer -= Time.deltaTime;
            rbody.velocity = Vector2.zero;
            if (timer <= 0) {
                shoot();
                timer = 1 / firerate;
            }
        }
    
        private void shoot() {
            animator.SetTrigger(Attack);
            enemy.StartCoroutine(instantiateProj());
        }

        private IEnumerator instantiateProj() {
            // Give time for animation to play
            yield return new WaitForSeconds(.5f);
            Vector3 emissionPosition = direction.x > 0 ? emissionPointRight.position : emissionPointLeft.position;
            GameObject projClone = Instantiate(projectile, emissionPosition, projectile.transform.rotation);

            Rigidbody2D projectileRbody = projClone.GetComponent<Rigidbody2D>();

            projectileRbody.AddForce(direction * (projSpeed * Time.deltaTime), ForceMode2D.Impulse);
        }
    }
}
