using UnityEngine;

namespace Unbreakable {
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float lifetime;

        private void Start() {
            Destroy(gameObject, lifetime);
        }
    }
}
