using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unbreakable {
    public class Restart : MonoBehaviour
    {
        void Start() {
            GetComponent<Lifecycle>().onDeath(restart);
        }

        private void restart(GameObject obj) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
