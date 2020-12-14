using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Droppable : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField]
    [MinMaxSlider(0f, 10f)] private MinMax groundSpawnDelay;

    private GameObject groundInstance;

    void Start() {
        Invoke(nameof(instantiateGround), groundSpawnDelay.RandomValue);
    }

    private void FixedUpdate() {
        if(groundInstance == null) return;
        groundInstance.transform.position = new Vector2(transform.position.x, groundInstance.transform.position.y);
    }

    private void instantiateGround() {
        groundInstance = Instantiate(ground, transform.position + Vector3.down, Quaternion.identity);
    }

    private void OnDestroy() {
        Destroy(groundInstance);
    }
}
