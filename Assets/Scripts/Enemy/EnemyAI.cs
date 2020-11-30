using System;
using System.Collections;
using Unbreakable;
using Unbreakable.Enemy.Behaviour;
using UnityEngine;

public class EnemyAI : MonoBehaviour, Damager {
	
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float freezeOnHitTime;
    [SerializeField] private EnemyBehavior trackingBehaviourTemplate;
    [SerializeField] private EnemyBehavior attackingBehaviourTemplate;
    [SerializeField] private bool isFlipped;
	
    private Transform target;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen;
    private Vector2 direction;
    private bool playerFound;
    private Animator animator;
    private EnemyBehavior trackingBehaviour;
    private EnemyBehavior attackingBehaviour;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(trackingBehaviourTemplate) trackingBehaviour = Instantiate(trackingBehaviourTemplate);
        if(attackingBehaviourTemplate) attackingBehaviour = Instantiate(attackingBehaviourTemplate);
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GetComponent<CollisionManager>().onHit(freeze);
        trackingBehaviour?.initialize(this, target);
        attackingBehaviour?.initialize(this, target);
    }

    private void Update() {
        direction = (target.position - transform.position).normalized;
        lookAtTarget();
    }

    private void FixedUpdate() {
        if (isFrozen) return;
        isPlayerInFov();
        if (playerFound) {
            attackingBehaviour?.update();
        }
        else {
            trackingBehaviour?.update();
        }

    }

    private void isPlayerInFov() {
        
        Vector2 rbodyPosition = rbody.position;
        RaycastHit2D hit = Physics2D.Raycast(rbodyPosition, direction, searchRadius, layerMask);
        
        playerFound = hit && hit.collider.CompareTag("Player");
        if(playerFound) 
            Debug.DrawLine(rbodyPosition, hit.point, Color.green);
        else 
            Debug.DrawLine(rbodyPosition, rbodyPosition + direction * searchRadius, Color.red);
    }
    
    private void freeze(HitInfo obj) {
        StartCoroutine(temporaryFreeze());
    }
    
    private IEnumerator temporaryFreeze() {
        isFrozen = true;
        rbody.velocity = Vector2.zero;
        animator.speed = 0;
        yield return new WaitForSeconds(freezeOnHitTime);
        animator.speed = 1;
        isFrozen = false;
    }


    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
    
    private void lookAtTarget() {
        bool flipX;
        flipX = transform.InverseTransformPoint(target.position).x > 0.1;
        flipX = isFlipped ? flipX : !flipX;
        spriteRenderer.flipX = flipX;
    }
}

