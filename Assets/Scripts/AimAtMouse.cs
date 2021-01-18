using System;
using UnityEngine;

namespace Unbreakable {
    public class AimAtMouse : MonoBehaviour {
    
        [SerializeField] private Transform weaponWrapper;
        [SerializeField] private Transform swordWrapper;
        private GameObject weapon;
        private float tempAngle;
        private SpriteRenderer spriteRenderer;
        private UnityEngine.Camera camera;
        private Transform emissionPoint;
        private Quaternion lastWeaponRotation;
        private Vector3 lookDir;
        private void Start() {
            spriteRenderer = weapon.GetComponent<SpriteRenderer>();
            camera = UnityEngine.Camera.main;
            emissionPoint = weapon.transform.GetChild(0);
        }

        void FixedUpdate() {
            float angle = getRotationAngle();
            weaponWrapper.transform.RotateAround(weaponWrapper.transform.position, Vector3.forward, angle - tempAngle);
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
            lookDir = Input.mousePosition - camera.WorldToScreenPoint(weaponWrapper.transform.position);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            return angle;
        }

        private void adjustWeaponSpriteDirection() {
            if(!weapon) return;
            if (weapon.GetComponent<Weapon>() is Sword) {
                spriteRenderer.flipX = Math.Sign(lookDir.x) < 0;
                float newWrapperX = spriteRenderer.flipX ? -.48f : .48f;
                swordWrapper.localPosition = new Vector2(newWrapperX, swordWrapper.localPosition.y);
            }
            else {
                spriteRenderer.flipY = weapon.transform.rotation.eulerAngles.z < 270 && weapon.transform.rotation.eulerAngles.z > 90;
                float newEmissionY = spriteRenderer.flipY ? 0.2f : -0.26f;
                float newWrapperX = spriteRenderer.flipY ? .48f : -.48f;
                emissionPoint.localPosition = new Vector3(emissionPoint.localPosition.x, newEmissionY);
                weaponWrapper.localPosition = new Vector2(newWrapperX, weaponWrapper.localPosition.y);
            }
        
        }
    
        public Vector3 getLookDirection() {
            return lookDir;
        }
    }
}
