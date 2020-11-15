using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public int maxLookAhead = 5;

    private GameObject target;
    private CinemachineVirtualCamera camera;

    private void OnEnable() {
        camera = GetComponent<CinemachineVirtualCamera>();
        target = new GameObject();
        camera.Follow = target.transform;
    }

    void Update() {
        Vector3 mouseScreenPoint = Input.mousePosition;
        mouseScreenPoint.z = 10;
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);
        Vector3 mousePositionRelativeToPlayer = player.transform.InverseTransformPoint(mouseWorldPoint);
	    
		
        Vector2 targetPosition = new Vector2(mousePositionRelativeToPlayer.x/2, mousePositionRelativeToPlayer.y /2);

	    
        if(targetPosition.magnitude > maxLookAhead)
            targetPosition.Scale(new Vector2(maxLookAhead/targetPosition.magnitude, maxLookAhead/targetPosition.magnitude));
	    
        Vector2 transformPositionInWorld = player.transform.TransformPoint(targetPosition);
        target.transform.position = transformPositionInWorld;
    }
}