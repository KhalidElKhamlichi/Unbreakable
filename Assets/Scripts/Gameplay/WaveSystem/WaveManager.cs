using System;
using System.Collections;
using System.Collections.Generic;
using Unbreakable.Common;
using Unbreakable.UI;
using Unbreakable.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.Gameplay.WaveSystem {
    public class WaveManager : MonoBehaviour {
        
        public event Action waveEndedEvent;
        public int waveIndex { get; private set; } = 1;
        
        [SerializeField] private Transform spawnLocations;
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private int initialNbrOfEnemiesPerWave;
        [SerializeField] private float enemiesMultiplierPerWave = 1;

        [SerializeField] [MinMaxSlider(0.1f, 10)] private MinMax spawnInterval;
        [SerializeField] private float spawnIntervalMaxReductionPerWave = 0;
        [SerializeField] private float timeBetweenWaves;
        [SerializeField] private HUDManager hudManager;
    
        private List<EnemyWave> waves;
        private EnemyWave currentWave;
        private int currentNbrOfEnemiesPerWave;
        private float spawnTimer;
        private int spawnedEnemyCounter;
        private bool isInTransition;

        private void Awake() {
            waveIndex = 1;
        }

        private void Start() {
            currentNbrOfEnemiesPerWave = initialNbrOfEnemiesPerWave;
            currentWave = new EnemyWave(enemyPrefabs, currentNbrOfEnemiesPerWave, spawnInterval);
        }

        private void Update() {
            if (!currentWave.isDone()) {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0) {
                    Vector3 spawnLocation = pickSpawnLocation();
                    GameObject enemy = Instantiate(currentWave.getEnemy(), spawnLocation, Quaternion.identity);
                    spawnedEnemyCounter++;
                    enemy.GetComponent<Lifecycle>().onDeath(reduceEnemyCounter);
                    spawnTimer = currentWave.getSpawnInterval();
                }
            } 
            if(spawnedEnemyCounter <= 0) {
                if(!isInTransition) StartCoroutine(waveTransition());
            }
        
        }

        private IEnumerator waveTransition() {
            isInTransition = true;
            waveEndedEvent?.Invoke();
            hudManager.updateWaveTimer(timeBetweenWaves);
            yield return new WaitForSeconds(timeBetweenWaves);
            initializeNextWave();
            isInTransition = false;
        }

        private void reduceEnemyCounter(GameObject obj) {
            spawnedEnemyCounter--;
        }

        private void initializeNextWave() {
            spawnTimer = 0;
            currentNbrOfEnemiesPerWave = (int) Math.Round(currentNbrOfEnemiesPerWave * enemiesMultiplierPerWave, 0);
            spawnInterval.Max -= spawnIntervalMaxReductionPerWave;
            spawnInterval.Max = Math.Max(spawnInterval.Min, spawnInterval.Max);
            currentWave = new EnemyWave(enemyPrefabs, currentNbrOfEnemiesPerWave, spawnInterval);
            waveIndex++;
        }
    
        private Vector3 pickSpawnLocation() {
            int index = Random.Range(0, spawnLocations.childCount);
            return spawnLocations.GetChild(index).position;
        }
    
    }
}
