using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour {
    [SerializeField] private Transform spawnLocations;
    [SerializeField] private GameObject grunt;
    [SerializeField] private GameObject grunt2;
    [SerializeField] private int initialNbrOfGruntsPerWave;
    [SerializeField] private float gruntsMultiplierPerWave = 1;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private float spawnIntervalMaxReductionPerWave = 0;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private UIManager uiManager;
    
    private List<EnemyWave> waves;
    private EnemyWave currentWave;
    private int currentNbrOfGruntsPerWave;
    private GameManager gameManager;
    private float spawnTimer;
    private float betweenWavesTimer;
    private int spawnedEnemyCounter;

    private void Start() {
        gameManager = GetComponent<GameManager>();
        currentNbrOfGruntsPerWave = initialNbrOfGruntsPerWave;
        betweenWavesTimer = timeBetweenWaves;
        currentWave = new EnemyWave(grunt, grunt2, currentNbrOfGruntsPerWave, spawnIntervalMin, spawnIntervalMax);
    }


    private void Update() {
        if (!currentWave.isDone()) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                Vector3 spawnLocation = pickSpawnLocation();
                GameObject enemy = Instantiate(currentWave.getEnemy(), spawnLocation, Quaternion.identity);
                spawnedEnemyCounter++;
                enemy.GetComponent	<Lifecycle>().onDeath(reduceEnemyCounter);
                gameManager.subscribeToEnemyDeath(enemy);
                spawnTimer = currentWave.getSpawnInterval();
            }
        } 
        if(spawnedEnemyCounter <= 0){
            betweenWavesTimer -= Time.deltaTime;
            if (betweenWavesTimer <= 0) {
                initializeNextWave();
            }
        }
        uiManager.updateWaveTimer(betweenWavesTimer);
    }

    private void reduceEnemyCounter(GameObject obj) {
        spawnedEnemyCounter--;
    }

    private void initializeNextWave() {
        spawnTimer = 0;
        betweenWavesTimer = timeBetweenWaves;
        currentNbrOfGruntsPerWave = (int) (currentNbrOfGruntsPerWave * gruntsMultiplierPerWave);
        spawnIntervalMax -= spawnIntervalMaxReductionPerWave;
        spawnIntervalMax = Math.Max(spawnIntervalMin, spawnIntervalMax);
        currentWave = new EnemyWave(grunt, grunt2, currentNbrOfGruntsPerWave, spawnIntervalMin, spawnIntervalMax);
    }
    
    private Vector3 pickSpawnLocation() {
        int index = Random.Range(0, spawnLocations.childCount);
        return spawnLocations.GetChild(index).position;
    }
}
