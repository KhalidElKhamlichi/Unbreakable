using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerManager : MonoBehaviour
{
    public float moveSpeed;

    private PlayerController controller;
    private Vector2 velocity;
    private Vector2 directionalInput;
    private Animator animator;
    private Weapon weapon;
    
    private static readonly int HorizontalVelocity = Animator.StringToHash("horizontalVelocity");
    private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");

    void Start() {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }
	
    void FixedUpdate() {	
        calculateVelocity();
        controller.move(velocity * Time.deltaTime);
        animate();
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
        weapon = null;
    }
}