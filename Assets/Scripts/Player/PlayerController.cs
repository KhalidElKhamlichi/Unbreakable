using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;

    private void Start() {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void move(Vector2 moveAmount) {
        rbody.MovePosition(rbody.position + moveAmount * Time.fixedDeltaTime);
    }
}
