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
    [SerializeField] private float invincibilityAfterHurtTime;

    private PlayerController controller;
    private Vector2 velocity;
    private Vector2 directionalInput;
    private Animator animator;
    private Weapon weapon;
    private AimAtMouse aimAtMouse;
    private CollisionManager collisionManager;
    private bool isKnockedback;
    private bool isDashing;
    
    private static readonly int HorizontalVelocity = Animator.StringToHash("horizontalVelocity");
    private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");

    private void OnEnable() {
        aimAtMouse = GetComponent<AimAtMouse>();
        aimAtMouse.setWeapon(weaponGameObject);
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
            print("Weapon picked up");
            weaponGameObject = weaponToPickUp;
            weaponGameObject.transform.position = weaponWrapper.position;
            weaponGameObject.transform.rotation = weaponWrapper.rotation;
//            weaponGameObject.transform.localPosition = Vector2.zero;
            weaponGameObject.transform.parent = weaponWrapper;
            weapon = weaponGameObject.GetComponent<Weapon>();
            aimAtMouse.setWeapon(weaponGameObject);
        }
    }

    private void animate() {
        animator.SetInteger(HorizontalVelocity, (int)velocity.x);
        animator.SetInteger(VerticalVelocity, (int)velocity.y);
    }

    public void setDirectionalInput(Vector2 input) {
        directionalInput = Vector2.zero;
//        if (!isKnockedback || !isDashing)
            directionalInput = input;
    }
	
    private void calculateVelocity() {
        velocity.x = directionalInput.x * moveSpeed;
        velocity.y = directionalInput.y * moveSpeed;;
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
            weaponGameObject.transform.parent = null;
            weaponGameObject.transform.DOJump(transform.position + transform.right * 2f, 2, 1, .5f, false); 
            unreferenceWeapon();
        }
    }
    
    private void reactToHit(Collider2D collider2D) {
        if(isKnockedback) return;
        isKnockedback = true;
//        controller.knockback();
			
        StartCoroutine(collisionManager.disableCollisionWith(collider2D.tag, invincibilityAfterHurtTime));
			
        Invoke(nameof(unsetIsKnockedback), invincibilityAfterHurtTime/2);
			
        float? knockbackForce = collider2D.gameObject.GetComponent<Damager>()?.getKnockbackForce();
        if (!knockbackForce.HasValue) return;
        Vector2 collisionDirection =  transform.position - collider2D.transform.position;
        collisionDirection.Normalize();
        Vector2 knockbackVelocity = collisionDirection * new Vector2(knockbackForce.Value, Mathf.Abs(knockbackForce.Value/2));
//        velocity = knockbackVelocity;
        controller.knockback(knockbackVelocity);

//        StartCoroutine(vibrateGamepad(.3f));
    }
    
    private void unsetIsKnockedback() {
        isKnockedback = false;
    }
}