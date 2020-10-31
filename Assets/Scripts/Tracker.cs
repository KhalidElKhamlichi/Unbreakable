using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class Tracker : MonoBehaviour, Damager {
	
//    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float searchRadius;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;
	
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rbody;
    private bool isTargetDetected = true;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
//        GetComponent<CollisionManager>().onHit(reactToHit);
    }

    private void Update() {
//        Collider2D collider = Physics2D.OverlapCircle(transform.position, searchRadius, collisionMask); 
//        setIsTargetDetected(true);
    }

    void FixedUpdate () {
        if (isTargetDetected) {
//            if (knockback.isFrozen()) return;

            Vector2 force = target.position - transform.position;
            force.Normalize();
            rbody.velocity = force * speed;
            lookAtTarget();
        }
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
        spriteRenderer.flipX = !(transform.InverseTransformPoint(target.position).x > 0.1);
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
    
//    private void OnDrawGizmos() {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, searchRadius);
//    }
}
