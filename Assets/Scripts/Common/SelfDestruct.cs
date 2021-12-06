using UnityEngine;

namespace Unbreakable.Common {
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float lifetime;

        private void Start() {
            Destroy(gameObject, lifetime);
        }
    }
}
