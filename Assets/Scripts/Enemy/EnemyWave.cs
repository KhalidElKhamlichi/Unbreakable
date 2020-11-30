using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] 
public class EnemyWave {
    [SerializeField] private GameObject grunt;
    [SerializeField] private GameObject grunt2;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private int totalNbrOfGrunts;

    public EnemyWave(GameObject grunt, GameObject grunt2, int totalNbrOfGrunts, float spawnIntervalMin, float spawnIntervalMax) {
        this.grunt = grunt;
        this.grunt2 = grunt2;
        this.totalNbrOfGrunts = totalNbrOfGrunts;
        this.spawnIntervalMin = spawnIntervalMin;
        this.spawnIntervalMax = spawnIntervalMax;
    }

    public GameObject getEnemy() {
        if (totalNbrOfGrunts > 0) {
            totalNbrOfGrunts--;
            int index = Random.Range(0, 2);
            return index == 0 ? grunt : grunt2;
        }
        return null;
    }

    public float getSpawnInterval() {
        return Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    public bool isDone() {
        return totalNbrOfGrunts <= 0;
    }
}
