using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] 
public class EnemyWave {
    [SerializeField] private GameObject grunt;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private int totalNbrOfGrunts;

    public EnemyWave(GameObject grunt, int totalNbrOfGrunts, float spawnIntervalMin, float spawnIntervalMax) {
        this.grunt = grunt;
        this.totalNbrOfGrunts = totalNbrOfGrunts;
        this.spawnIntervalMin = spawnIntervalMin;
        this.spawnIntervalMax = spawnIntervalMax;
    }

    public GameObject getEnemy() {
        if (totalNbrOfGrunts > 0) {
            totalNbrOfGrunts--;
            return grunt;
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
