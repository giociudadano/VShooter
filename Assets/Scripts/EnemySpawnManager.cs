using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnPositionXRange = 8.5f;
    [SerializeField] private float spawnPositionY = 1.55f;
    [SerializeField] private float spawnPositionZ = 40f;

    [SerializeField] private float spawnInterval = 1.5f;

    [SerializeField] private float firstSpawnDelay = 1f;

    void Start() {
      //InvokeRepeating("SpawnEnemy", 1f, 1.5f);
        StartCoroutine(SpawnEnemy());
    }

    void Update() {
      
    }

    //  Ren's notes: I refactored this to use a coroutine in case we need to decorate SpawnEnemy() by passing it parameters (coroutines are also more performant apparently)
    private IEnumerator SpawnEnemy() {
      yield return new WaitForSeconds(firstSpawnDelay);
      while (true) {
        float spawnPositionX = Random.Range(-spawnPositionXRange, spawnPositionXRange);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
        Instantiate(enemies[0], spawnPosition, enemies[0].transform.rotation);
        yield return new WaitForSeconds(spawnInterval);
      }
    }
}
