using UnityEngine;

public class AimAtMouse : MonoBehaviour {
    
    [SerializeField] private Transform weaponWrapper;
    private GameObject weapon;
    private float tempAngle;
    private SpriteRenderer spriteRenderer;
    private Camera camera;
    private Transform emissionPoint;
    private Quaternion lastWeaponRotation;
    private void Start() {
        spriteRenderer = weapon.GetComponent<SpriteRenderer>();
        camera = Camera.main;
        emissionPoint = weapon.transform.GetChild(0);
    }

    void Update() {
        float angle = getRotationAngle();
        weaponWrapper.transform.RotateAround(transform.position, Vector3.forward, angle - tempAngle);
        tempAngle = angle;
        adjustWeaponSpriteDirection();
    }

    public void setWeapon(GameObject weapon) {
        this.weapon = weapon;
        if (!weapon) return;
        spriteRenderer = weapon.GetComponent<SpriteRenderer>();
        emissionPoint = weapon.transform.GetChild(0);
    }

    private float getRotationAngle() {
        Vector3 dir = Input.mousePosition - camera.WorldToScreenPoint(weaponWrapper.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    private void adjustWeaponSpriteDirection() {
        if(!weapon) return;
        spriteRenderer.flipY = weapon.transform.rotation.eulerAngles.z < 270 && weapon.transform.rotation.eulerAngles.z > 90;
        float newEmissionY = spriteRenderer.flipY ? -.2f : .3f;
        emissionPoint.localPosition = new Vector3(emissionPoint.localPosition.x, newEmissionY);
    }
}
