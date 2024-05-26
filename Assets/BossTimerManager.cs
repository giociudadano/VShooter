using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossTimerManager : MonoBehaviour {

    [SerializeField] private GameObject bossSpawnManager;

    private float secondsUntilSpawn;

    void Start() {
       secondsUntilSpawn = bossSpawnManager.GetComponent<BossSpawnManager>().spawnDelay;
       transform.Find("Text").GetComponent<TMP_Text>().text = $"{TimeSpan.FromSeconds(secondsUntilSpawn).Minutes}:{TimeSpan.FromSeconds(secondsUntilSpawn).Seconds:00}";
       StartCoroutine("UpdateTimer");
    }

    private IEnumerator UpdateTimer() {
        while (secondsUntilSpawn > 0) {
            yield return new WaitForSeconds(1f);   
            secondsUntilSpawn--;
            transform.Find("Text").GetComponent<TMP_Text>().text = $"{TimeSpan.FromSeconds(secondsUntilSpawn).Minutes}:{TimeSpan.FromSeconds(secondsUntilSpawn).Seconds:00}";
        }
        yield return null;
    }
}
