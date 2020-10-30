using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Update() {
        Vector3 mouseScreenPoint = Input.mousePosition;
        mouseScreenPoint.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPoint);
    }
}