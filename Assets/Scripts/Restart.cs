using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    void Start() {
        GetComponent<Lifecycle>().onDeath(restart);
    }

    private void restart(GameObject obj) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
