using System;
using DG.Tweening;
using Unbreakable;
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
    private event Action weaponPickedUpEvent;
    
    private static readonly int HorizontalVelocity = Animator.StringToHash("HorizontalVelocity");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int HorizontalLook = Animator.StringToHash("HorizontalLook");
    private static readonly int VerticalLook = Animator.StringToHash("VerticalLook");
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int HasWeapon = Animator.StringToHash("HasWeapon");

    private void Awake() {
        aimAtMouse = GetComponent<AimAtMouse>();
        aimAtMouse.setWeapon(weaponGameObject);
        weapon = weaponGameObject.GetComponent<Weapon>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        
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
            weaponPickedUpEvent?.Invoke();
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
        if (weapon == null) return;
        weapon.attack();
        if(!weapon.isUsable()) unreferenceWeapon();
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
    
    public void onAttack(Action action) => weapon?.onAttack(action);
    
    public void onWeaponPickedUp(Action action) => weaponPickedUpEvent += action;

    public int? getRemainingWeaponUses() {
        return weapon?.getRemainingUses();
    }
    
    public int? getInitialWeaponUses() {
        return weapon?.getInitialUses();
    }
    
    private void reactToHit(HitInfo hit) {
        if(isKnockedback) return;
        isKnockedback = true;
        StartCoroutine(collisionManager.disableCollisionWith(hit.getTag(), invincibilityAfterHurtTime));
			
        Invoke(nameof(unsetIsKnockedback), invincibilityAfterHurtTime/4);
			
        float? knockbackForce = hit.getDamager()?.getKnockbackForce();
        if (!knockbackForce.HasValue) return;

        Vector2 knockbackVelocity = hit.getCollisionDirection().normalized * new Vector2(knockbackForce.Value, Mathf.Abs(knockbackForce.Value/2));
        controller.knockback(knockbackVelocity, invincibilityAfterHurtTime/4);

//        StartCoroutine(vibrateGamepad(.3f));
    }
    
    private void unsetIsKnockedback() {
        isKnockedback = false;
    }
}