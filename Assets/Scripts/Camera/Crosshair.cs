using UnityEngine;

namespace Unbreakable.Camera {
    public class Crosshair : MonoBehaviour
    {
        void Update() {
            Vector3 mouseScreenPoint = Input.mousePosition;
            mouseScreenPoint.z = 10;
            transform.position = UnityEngine.Camera.main.ScreenToWorldPoint(mouseScreenPoint);
        }
    }
}