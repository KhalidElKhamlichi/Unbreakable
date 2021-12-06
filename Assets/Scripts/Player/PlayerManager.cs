using System;
using DG.Tweening;
using Unbreakable.Combat;
using Unbreakable.Common;
using UnityEngine;

namespace Unbreakable.Player {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerManager : MonoBehaviour {
        
        private static readonly int HorizontalVelocity = Animator.StringToHash("HorizontalVelocity");
        private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
        private static readonly int HorizontalLook = Animator.StringToHash("HorizontalLook");
        private static readonly int VerticalLook = Animator.StringToHash("VerticalLook");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int HasWeapon = Animator.StringToHash("HasWeapon");
        
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
        private bool isKnockedBack;
        private SpriteRenderer spriteRenderer;
        private event Action weaponPickedUpEvent;

        private void Awake() {
            aimAtMouse = GetComponent<AimAtMouse>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            controller = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            collisionManager = GetComponent<CollisionManager>();

            collisionManager.onHit(reactToHit);
        }

        private void Start() {
            pickUpWeapon(weaponGameObject);
        }

        private void FixedUpdate() {	
            calculateVelocity();
            if (!isKnockedBack) controller.move(velocity);
            animate();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Weapon weaponToPickup = other.gameObject.GetComponent<Weapon>();
            if (weaponToPickup == null) return;
            if(weaponToPickup.isPickable()) pickUpWeapon(other.gameObject);
        }

        public void setDirectionalInput(Vector2 input) {
            directionalInput = Vector2.zero;
            directionalInput = input;
        }

        public void attack() {
            if (weapon == null) return;
            weapon.attack();
            if(!weapon.isUsable()) unreferenceWeapon();
        }

        public void dropWeapon() {
            if (weapon == null) return;
            weapon.drop();
            weaponGameObject.transform.parent = null;
            weaponGameObject.transform.DOJump(transform.position + transform.right * 2f, 2, 1, .5f, false); 
            unreferenceWeapon();
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

        private void pickUpWeapon(GameObject weaponToPickUp) {
            if (weapon != null) return;
            weaponGameObject = weaponToPickUp;
            weapon = weaponGameObject.GetComponent<Weapon>();
            Transform wrapper = weapon is Sword ? swordWrapper : weaponWrapper;
            weaponGameObject.transform.SetPositionAndRotation(wrapper.position, wrapper.rotation);
            weaponGameObject.transform.parent = wrapper;
            weapon.pickUp();
            aimAtMouse.setWeapon(weaponGameObject);
            weaponPickedUpEvent?.Invoke();
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

        private void calculateVelocity() {
            Vector2 direction = Vector2.ClampMagnitude(directionalInput	, 1f);
            velocity.x = direction.x * moveSpeed;
            velocity.y = direction.y * moveSpeed;
        }

        private void unreferenceWeapon() {
            weapon = null;
            aimAtMouse.setWeapon(null);
        }

        private void reactToHit(HitInfo hit) {
            if(isKnockedBack) return;
            isKnockedBack = true;
            StartCoroutine(collisionManager.disableCollisionWith(hit.getTag(), invincibilityAfterHurtTime));
			
            Invoke(nameof(unsetIsKnockedBack), invincibilityAfterHurtTime/4);
			
            float? knockBackForce = hit.getDamager()?.getKnockBackForce();
            if (!knockBackForce.HasValue) return;

            Vector2 knockBackVelocity = hit.getCollisionDirection().normalized * new Vector2(knockBackForce.Value, Mathf.Abs(knockBackForce.Value/2));
            controller.knockback(knockBackVelocity, invincibilityAfterHurtTime/4);

        }
    
        private void unsetIsKnockedBack() {
            isKnockedBack = false;
        }
    }
}