using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.Enemy {
    [Serializable] 
    public class EnemyWave {
        private List<GameObject> enemyPrefabs;
        private float spawnIntervalMin;
        private float spawnIntervalMax;
        private int totalNbrOfEnemies;

        public EnemyWave(List<GameObject> enemyPrefabs, int totalNbrOfEnemies, float spawnIntervalMin, float spawnIntervalMax) {
            this.enemyPrefabs = enemyPrefabs;
            this.totalNbrOfEnemies = totalNbrOfEnemies;
            this.spawnIntervalMin = spawnIntervalMin;
            this.spawnIntervalMax = spawnIntervalMax;
        }

        public GameObject getEnemy() {
            if (totalNbrOfEnemies > 0) {
                totalNbrOfEnemies--;
                int index = Random.Range(0, enemyPrefabs.Count);
                return enemyPrefabs[index];
            }
            return null;
        }

        public float getSpawnInterval() {
            return Random.Range(spawnIntervalMin, spawnIntervalMax);
        }

        public bool isDone() {
            return totalNbrOfEnemies <= 0;
        }
    }
}
