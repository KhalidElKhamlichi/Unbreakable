using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tracker))]
public class Goopstein : MonoBehaviour, Damager {
	
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int damage;
    [SerializeField] private float firerate;
    [SerializeField] private float projSpeed;
    [SerializeField] private float knockbackForce;
	
    private Transform target;
    private Tracker tracker;
    private Rigidbody2D rbody;
    private bool isTargetDetected;
    private Animator animator;
    private float timer;
    private Vector2 direction;
    private bool isShooting;
    private static readonly int Attack = Animator.StringToHash("Attack");

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        tracker = GetComponent<Tracker>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        timer -= Time.deltaTime;
        bool playerFound = false;

        RaycastHit2D hit = Physics2D.Raycast(rbody.position, direction, searchRadius, layerMask);
        if (hit) {
            Debug.DrawLine(rbody.position, hit.point, Color.green);
            playerFound = hit.collider.CompareTag("Player");
        }
        else {
            Debug.DrawLine(rbody.position, rbody.position + direction * searchRadius, Color.red);
        }
        
        setIsTargetDetected(playerFound);
    }

    void FixedUpdate () {
        direction = (target.position - transform.position).normalized;
        if (isTargetDetected || isShooting) {
            tracker.stop();
            rbody.velocity = Vector2.zero;
            if (timer <= 0) {
                shoot();
                timer = 1 / firerate;
            }
        }
        else {
            tracker.start();
        }
    }

    private void shoot() {
        animator.SetTrigger(Attack);
        isShooting = true;
        Invoke(nameof(instantiateProj), .5f);
    }

    private void instantiateProj() {
        GameObject projClone = Instantiate(projectile, transform.position, projectile.transform.rotation);

        Rigidbody2D projectileRbody = projClone.GetComponent<Rigidbody2D>();

        projectileRbody.AddForce(direction * (projSpeed * Time.deltaTime), ForceMode2D.Impulse);
        isShooting = false;
    }

    private void setIsTargetDetected(bool isTargetDetected) {
        this.isTargetDetected = isTargetDetected;
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
    
    private void OnDrawGizmos() {
//        Gizmos.color = Color.red;
//        Vector2 forward = transform.TransformDirection(direction) * searchRadius;
//        Gizmos.DrawLine(transform.position, forward);
    }
}

