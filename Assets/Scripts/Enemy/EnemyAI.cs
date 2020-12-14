using System;
using System.Collections;
using Unbreakable;
using Unbreakable.Enemy.Behaviour;
using UnityEngine;

public class EnemyAI : MonoBehaviour, Damager {
	
    [SerializeField] [MinMaxSlider(0f, 20f)] private MinMax searchRadius;
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
    private Coroutine temporaryFreezeCoroutine;
    private EnemyBehavior trackingBehaviour;
    private EnemyBehavior attackingBehaviour;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(trackingBehaviourTemplate) trackingBehaviour = Instantiate(trackingBehaviourTemplate);
        if(attackingBehaviourTemplate) attackingBehaviour = Instantiate(attackingBehaviourTemplate);
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       
        GetComponent<Lifecycle>().onDeath(o => {
            if (temporaryFreezeCoroutine != null) StopCoroutine(temporaryFreezeCoroutine);
            StartCoroutine(temporaryFreeze(5));
        });
        GetComponent<CollisionManager>().onHit(freeze);
        trackingBehaviour?.initialize(this, target);
        attackingBehaviour?.initialize(this, target);
    }

    private void Update() {
        direction = (target.position - transform.position).normalized;
        isPlayerInFov();
        lookAtTarget();
        
    }

    private void FixedUpdate() {
        if (isFrozen) return;
        
        if (playerFound) {
            attackingBehaviour?.update();
        }
        else {
            trackingBehaviour?.update();
        }

    }

    private void isPlayerInFov() {
        
        Vector2 rbodyPosition = rbody.position;
        RaycastHit2D hit = Physics2D.Raycast(rbodyPosition, direction, searchRadius.RandomValue, layerMask);
        
        playerFound = hit && hit.collider.CompareTag("Player");
        if(playerFound) 
            Debug.DrawLine(rbodyPosition, hit.point, Color.green);
        else 
            Debug.DrawLine(rbodyPosition, rbodyPosition + direction * searchRadius.Max, Color.red);
    }
    
    private void freeze(HitInfo obj) {
        if (isFrozen) return;
        temporaryFreezeCoroutine = StartCoroutine(temporaryFreeze(freezeOnHitTime));
    }
    
    private IEnumerator temporaryFreeze(float time) {
        isFrozen = true;
        rbody.velocity = Vector2.zero;
        animator.speed = 0;
        yield return new WaitForSeconds(time);
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

