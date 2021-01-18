using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unbreakable {
    public class Start : MonoBehaviour
    {
        public void startGame() {
            SceneManager.LoadScene(1);
        }
    }
}
