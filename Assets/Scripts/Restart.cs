using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Lifecycle>().onDeath(restart);
    }

    private void restart(GameObject obj) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
