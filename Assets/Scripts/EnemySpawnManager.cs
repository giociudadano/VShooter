using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnPositionXRange = 8.5f;
    [SerializeField] private float spawnPositionY = 1.25f;

    [SerializeField] private float spawnPositionZ = 40f;

    void Start() {
      InvokeRepeating("SpawnEnemy", 1f, 1.5f);
    }

    void Update() {
      
    }

    void SpawnEnemy() {
      int randomEnemy = Random.Range(0, enemies.Length);
      float spawnPositionX = Random.Range(-spawnPositionXRange, spawnPositionXRange);
      Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
      Instantiate(enemies[randomEnemy], spawnPosition, enemies[randomEnemy].transform.rotation);
    }
}
