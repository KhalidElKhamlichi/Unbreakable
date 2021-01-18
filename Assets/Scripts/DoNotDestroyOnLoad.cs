using UnityEngine;

namespace Unbreakable {
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        private static DoNotDestroyOnLoad instance;
        private void Awake() {        
            if (instance != null) {
                Destroy(gameObject);
            } else {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

    }
}
