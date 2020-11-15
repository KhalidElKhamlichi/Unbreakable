using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class Goopstein : MonoBehaviour, Damager {
	
//    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float searchRadius;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float firerate;
    [SerializeField] private float projSpeed = 500.0f;
    [SerializeField] private float knockbackForce;
	
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rbody;
    private bool isTargetDetected = false;
    private Animator animator;
    private float timer;
    private Vector2 direction;
    private bool isShooting;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
//        timer = 1 / firerate;
//        GetComponent<CollisionManager>().onHit(reactToHit);
    }

    private void Update() {
        timer -= Time.deltaTime;
//        if (collider.CompareTag("Player")) {
//            animator.SetTrigger("Attack");
//            setIsTargetDetected(true);
//        }
//        else {
//            setIsTargetDetected(false);
//        }
        bool playerFound = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);
        foreach (Collider2D hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player")) {
                playerFound = true;
            }
           
        }
        setIsTargetDetected(playerFound);
        
    }

    void FixedUpdate () {
        Vector2 force = target.position - transform.position;
        direction = force;
        force.Normalize();
        lookAtTarget();
        if (isTargetDetected || isShooting) {
            rbody.velocity = Vector2.zero;
            if (timer <= 0) {
                shoot(force);
                timer = 1 / firerate;
            }
        }
        else {
            rbody.velocity = force * speed;
        }

    }

    private void shoot(Vector2 force) {
        animator.SetTrigger("Attack");
        isShooting = true;
        Invoke(nameof(instantiateProj), .5f);
    }

    private void instantiateProj() {
        GameObject projClone = Instantiate(projectile, transform.position, projectile.transform.rotation);
//        projClone.transform.LookAt(target);
//        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        projClone.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Rigidbody2D projRbdy = projClone.GetComponent<Rigidbody2D>();

        projRbdy.AddForce(Time.deltaTime * projSpeed * direction.normalized);
        //rbody.SetRotation(Quaternion.LookRotation(rbody.velocity));
        isShooting = false;
    }

    public void setIsTargetDetected(bool isTargetDetected) {
        this.isTargetDetected = isTargetDetected;
    }

//    private void reactToHit(Collider2D collider2D) {
//        IDamager damager = collider2D.gameObject.GetComponent<IDamager>();
//        if (damager == null) return;
//        float knockbackForce = damager.getKnockbackForce();
//        knockback.knockback(new Vector2(knockbackForce, knockbackForce));
//        Quaternion colliderRotation = collider2D.transform.rotation;
//        Quaternion hitEffectRot = Quaternion.Euler(colliderRotation.eulerAngles.x, colliderRotation.eulerAngles.y + 180, colliderRotation.eulerAngles.z);
//        GameObject hitEffectClone = Instantiate(hitEffect, collider2D.ClosestPoint(transform.position), hitEffectRot);
//        Destroy(hitEffectClone, 0.13f);
//    }

    private void lookAtTarget() {
        spriteRenderer.flipX = (transform.InverseTransformPoint(target.position).x > 0.1);
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}

