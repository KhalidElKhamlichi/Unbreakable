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

    private PlayerController controller;
    private Vector2 velocity;
    private Vector2 directionalInput;
    private Animator animator;
    private Weapon weapon;
    private AimAtMouse aimAtMouse;
    
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
    }
	
    void FixedUpdate() {	
        calculateVelocity();
        controller.move(velocity * Time.deltaTime);
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
}