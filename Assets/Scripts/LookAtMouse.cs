using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private float tempAngle = 0;
    private SpriteRenderer spriteRenderer;
    private Camera camera;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
    }

    void Update() {
        float angle = getRotationAngle();
        transform.RotateAround(transform.parent.position, Vector3.forward, angle - tempAngle);
        tempAngle = angle;
        adjustSpriteDirection();
    }

    private float getRotationAngle() {
        Vector3 dir = Input.mousePosition - camera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    private void adjustSpriteDirection() {
        spriteRenderer.flipY = transform.rotation.eulerAngles.z < 270 && transform.rotation.eulerAngles.z > 90;
    }
}
