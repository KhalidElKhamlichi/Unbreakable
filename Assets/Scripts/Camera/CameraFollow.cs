using Cinemachine;
using UnityEngine;

namespace Unbreakable.Camera {
    public class CameraFollow : MonoBehaviour {
        
        [SerializeField] private Transform player;
        [SerializeField] private int maxLookAhead = 5;

        private GameObject target;
        private CinemachineVirtualCamera virtualCamera;
        private UnityEngine.Camera camera;

        private void Start() {
            camera = UnityEngine.Camera.main;
        }

        private void OnEnable() {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            target = new GameObject();
            virtualCamera.Follow = target.transform;
        }

        private void Update() {
            Vector3 mouseScreenPoint = Input.mousePosition;
            mouseScreenPoint.z = 10;
            Vector3 mouseWorldPoint = camera.ScreenToWorldPoint(mouseScreenPoint);
            Vector3 mousePositionRelativeToPlayer = player.InverseTransformPoint(mouseWorldPoint);
            
            Vector2 targetPosition = new Vector2(mousePositionRelativeToPlayer.x, mousePositionRelativeToPlayer.y);

            if (targetPosition.magnitude > maxLookAhead) {
                targetPosition.Scale(new Vector2(maxLookAhead / targetPosition.magnitude,
                    maxLookAhead / targetPosition.magnitude));
            }

            Vector2 transformPositionInWorld = player.TransformPoint(targetPosition);
            target.transform.position = transformPositionInWorld;
        }
    }
}