using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaveManager : MonoBehaviour {
    [SerializeField] private Transform spawnLocations;
    [SerializeField] private GameObject grunt;
    [SerializeField] private int initialNbrOfGruntsPerWave;
    [SerializeField] private float gruntsMultiplierPerWave;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private float spawnIntervalMaxReductionPerWave;
    [SerializeField] private float timeBetweenWaves;
    
    private List<EnemyWave> waves;
    private EnemyWave currentWave;
    private int currentNbrOfGruntsPerWave;
    private GameManager gameManager;
    private float spawnTimer;
    private float betweenWavesTimer;

    private void Start() {
        gameManager = GetComponent<GameManager>();
        currentNbrOfGruntsPerWave = initialNbrOfGruntsPerWave;
        betweenWavesTimer = timeBetweenWaves;
        initializeNextWave();
    }

    private void initializeNextWave() {
        spawnTimer = 0;
        currentWave = new EnemyWave(grunt, currentNbrOfGruntsPerWave, spawnIntervalMin, spawnIntervalMax);
    }

    private void Update() {
        if (!currentWave.isDone()) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                Transform spawnLocation = pickSpawnLocation();
                GameObject enemy = Instantiate(currentWave.getEnemy(), spawnLocation);
                gameManager.subscribeToEnemyDeath(enemy);
                spawnTimer = currentWave.getSpawnInterval();
            }
        }
    }

    private Transform pickSpawnLocation() {
        int index = Random.Range(0, spawnLocations.childCount+1);
        return spawnLocations.GetChild(index);
    }
}
