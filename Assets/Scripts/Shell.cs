using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shell : MonoBehaviour
{
    [SerializeField] private GameObject shellGround;
//    [SerializeField] private float groundSpawnDelay;
    [SerializeField]
    [MinMaxSlider(1f, 10f)] private MinMax groundSpawnDelay;

    private GameObject shellGroundInstance;

    void Start() {
        Invoke(nameof(instantiateGround), groundSpawnDelay.RandomValue);
    }

    private void FixedUpdate() {
        if(shellGroundInstance == null) return;
        shellGroundInstance.transform.position = new Vector2(transform.position.x, shellGroundInstance.transform.position.y);
    }

    private void instantiateGround() {
        shellGroundInstance = Instantiate(shellGround, transform.position + Vector3.down, Quaternion.identity);
    }

    private void OnDestroy() {
        Destroy(shellGroundInstance);
    }
}
