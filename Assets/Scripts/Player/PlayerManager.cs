using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject weaponGameObject;
    [SerializeField] private Transform weaponWrapper;
    [SerializeField] private Transform swordWrapper;
    [SerializeField] private float invincibilityAfterHurtTime;

    private PlayerController controller;
    private Vector2 velocity;
    private Vector2 directionalInput;
    private Animator animator;
    private Weapon weapon;
    private AimAtMouse aimAtMouse;
    private CollisionManager collisionManager;
    private Interactable interactable;
    private bool isKnockedback;
    private bool isDashing;
    private SpriteRenderer spriteRenderer;
    
    private static readonly int HorizontalVelocity = Animator.StringToHash("HorizontalVelocity");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int HorizontalLook = Animator.StringToHash("HorizontalLook");
    private static readonly int VerticalLook = Animator.StringToHash("VerticalLook");
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int HasWeapon = Animator.StringToHash("HasWeapon");

    private void OnEnable() {
        aimAtMouse = GetComponent<AimAtMouse>();
        aimAtMouse.setWeapon(weaponGameObject);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        weapon = weaponGameObject.GetComponent<Weapon>();
        collisionManager = GetComponent<CollisionManager>();
        collisionManager.onHit(reactToHit);
    }
	
    void FixedUpdate() {	
        calculateVelocity();
        if (!isKnockedback && !isDashing) controller.move(velocity);
        animate();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Weapon>() != null) {
            if(other.gameObject.GetComponent<Weapon>().isPickable()) pickUpWeapon(other.gameObject);
        }
    }

    private void pickUpWeapon(GameObject weaponToPickUp) {
        if (weapon == null) {
            weaponGameObject = weaponToPickUp;
            weapon = weaponGameObject.GetComponent<Weapon>();
            Transform wrapper = weapon is Sword ? swordWrapper : weaponWrapper;
            weaponGameObject.transform.position = wrapper.position;
            weaponGameObject.transform.rotation = wrapper.rotation;
            weaponGameObject.transform.parent = wrapper;
            weapon.pickUp();
            aimAtMouse.setWeapon(weaponGameObject);
        }
    }

    private void animate() {
        animator.SetFloat(HorizontalVelocity, Math.Sign(velocity.x));
        animator.SetFloat(VerticalVelocity, Math.Sign(velocity.y));
        bool isMoving = velocity.magnitude > 0;
        animator.SetBool(Moving, isMoving);
        animator.SetBool(HasWeapon, weapon != null);
        Vector3 lookDirection = aimAtMouse.getLookDirection();
        spriteRenderer.flipX = lookDirection.x < 0;
        animator.SetFloat(HorizontalLook, Math.Sign(lookDirection.x));
        animator.SetFloat(VerticalLook, Math.Sign(lookDirection.y));
    }

    public void setDirectionalInput(Vector2 input) {
        directionalInput = Vector2.zero;
//        if (!isKnockedback || !isDashing)
            directionalInput = input;
    }
	
    private void calculateVelocity() {
        Vector2 direction = Vector2.ClampMagnitude(directionalInput	, 1f);
        velocity.x = direction.x * moveSpeed;
        velocity.y = direction.y * moveSpeed;
    }

    public void attack() {
        weapon?.attack();
        unreferenceWeapon();
    }

    private void unreferenceWeapon() {
        weapon = null;
        aimAtMouse.setWeapon(null);
    }

    public void dropWeapon() {
        if (weapon != null) {
            weapon.setPickable(false);
            weapon.drop();
            weaponGameObject.transform.parent = null;
            weaponGameObject.transform.DOJump(transform.position + transform.right * 2f, 2, 1, .5f, false); 
            unreferenceWeapon();
        }
    }

    public void interact() {
        interactable?.interact();
    }
    public void setInteractable(Interactable interactable) {
        this.interactable = interactable;
    }
    
    private void reactToHit(Collider2D collider2D) {
        if(isKnockedback) return;
        isKnockedback = true;
//        controller.knockback();
			
        StartCoroutine(collisionManager.disableCollisionWith(collider2D.tag, invincibilityAfterHurtTime));
			
        Invoke(nameof(unsetIsKnockedback), invincibilityAfterHurtTime/4);
			
        float? knockbackForce = collider2D.gameObject.GetComponent<Damager>()?.getKnockbackForce();
        if (!knockbackForce.HasValue) return;
        Vector2 collisionDirection =  transform.position - collider2D.transform.position;
        collisionDirection.Normalize();
        Vector2 knockbackVelocity = collisionDirection * new Vector2(knockbackForce.Value, Mathf.Abs(knockbackForce.Value/2));
//        velocity = knockbackVelocity;
        controller.knockback(knockbackVelocity, invincibilityAfterHurtTime/4);

//        StartCoroutine(vibrateGamepad(.3f));
    }
    
    private void unsetIsKnockedback() {
        isKnockedback = false;
    }
}