using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnPositionXRange = 8.5f;
    [SerializeField] private float spawnPositionY = 1.25f;

    [SerializeField] private float spawnPositionZ = 40f;

    void Start() {
      InvokeRepeating("SpawnEnemy", 1f, 3f);
    }

    void Update() {
        
    }

    void SpawnEnemy() {
      float spawnPositionX = Random.Range(-spawnPositionXRange, spawnPositionXRange);
      Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
      Instantiate(enemies[0], spawnPosition, enemies[0].transform.rotation);
    }
}
