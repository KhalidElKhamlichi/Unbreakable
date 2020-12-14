using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private bool isKnockedback;
    

    private void Start() {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void move(Vector2 moveAmount) {
//        rbody.MovePosition(rbody.position + moveAmount * Time.deltaTime);
        rbody.velocity = moveAmount * Time.deltaTime;
    }

    public void knockback(Vector2 knockbackVelocity, float duration) {
//        rbody.AddForce(knockbackVelocity, ForceMode2D.Impulse); 
        rbody.DOMove((Vector2)transform.position + knockbackVelocity, duration);
    }
}
