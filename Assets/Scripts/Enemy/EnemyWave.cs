using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.Enemy {
    [Serializable] 
    public class EnemyWave {
        private List<GameObject> enemyPrefabs;
        private MinMax spawnInterval;
        private int totalNbrOfEnemies;

        public EnemyWave(List<GameObject> enemyPrefabs, int totalNbrOfEnemies, MinMax spawnInterval) {
            this.enemyPrefabs = enemyPrefabs;
            this.totalNbrOfEnemies = totalNbrOfEnemies;
            this.spawnInterval = spawnInterval;
        }

        public GameObject getEnemy() {
            if (totalNbrOfEnemies > 0) {
                totalNbrOfEnemies--;
                int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
                return enemyPrefabs[randomEnemyIndex];
            }
            return null;
        }

        public float getSpawnInterval() {
            return spawnInterval.RandomValue;
        }

        public bool isDone() {
            return totalNbrOfEnemies <= 0;
        }
    }
}
