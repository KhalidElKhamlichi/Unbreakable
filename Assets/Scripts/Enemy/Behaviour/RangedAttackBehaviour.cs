using System.Collections;
using System.Collections.Generic;
using Unbreakable.Enemy.Behaviour;
using UnityEngine;
[CreateAssetMenu]
public class RangedAttackBehaviour : EnemyBehavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firerate;
    [SerializeField] private float projSpeed;
    
    private static readonly int Attack = Animator.StringToHash("Attack");
    
    private Vector2 direction;
    private Rigidbody2D rbody;
    private float timer;
    private Animator animator;
    private bool isShooting;
    private Transform emissionPointLeft;
    private Transform emissionPointRight;
    
    public override void initialize(MonoBehaviour enemy, Transform target) {
        base.initialize(enemy, target);
        rbody = enemy.GetComponent<Rigidbody2D>();
        animator = enemy.GetComponent<Animator>();
        emissionPointLeft = enemy.transform.GetChild(0);
        emissionPointRight = enemy.transform.GetChild(1);
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
        isShooting = true;
        enemy.StartCoroutine(instantiateProj());
    }

    private IEnumerator instantiateProj() {
        yield return new WaitForSeconds(.5f);
        Vector3 emissionPosition = direction.x > 0 ? emissionPointRight.position : emissionPointLeft.position;
        GameObject projClone = Instantiate(projectile, emissionPosition, projectile.transform.rotation);

        Rigidbody2D projectileRbody = projClone.GetComponent<Rigidbody2D>();

        projectileRbody.AddForce(direction * (projSpeed * Time.deltaTime), ForceMode2D.Impulse);
        isShooting = false;
    }
}
