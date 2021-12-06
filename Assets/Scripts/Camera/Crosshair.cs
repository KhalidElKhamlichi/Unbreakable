using UnityEngine;

namespace Unbreakable.Camera {
    public class Crosshair : MonoBehaviour
    {
        private UnityEngine.Camera cameraInstance;

        private void Awake() {
            cameraInstance = UnityEngine.Camera.main;
            Cursor.visible = false;
        }

        void Update() {
            Vector3 mouseScreenPoint = Input.mousePosition;
            mouseScreenPoint.z = 10;
            transform.position = cameraInstance.ScreenToWorldPoint(mouseScreenPoint);
        }
    }
}