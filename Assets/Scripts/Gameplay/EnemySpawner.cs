using UnityEngine;

namespace Gameplay {
    public class EnemySpawner : MonoBehaviour {
        [Header("Configuration")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private float spawnRangeX = 8f;
        [SerializeField] private float spawnHeight = 6f;

        private float _nextSpawnTime;
        private int _nbSpawned;

        public static EnemySpawner Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        void Update() {
            if (Time.time >= _nextSpawnTime) {
                SpawnEnemy();
                _nextSpawnTime = Time.time + spawnRate;
            }
        }

        private void SpawnEnemy() {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);                // Random position above screen
            Vector3 spawnPos = new Vector3(randomX, spawnHeight, 0);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
            _nbSpawned++;
            enemy.name = "Enemy_" + _nbSpawned;
        }

        public void Reset() {
            _nextSpawnTime = 0;
            _nbSpawned = 0;
            // Delete all children (enemies)
        }
    }
}
