using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField] private GameObject Boss;

    [Header("Spawn Properties")]
    [SerializeField] private float spawnPositionXRange = 7f;
    [SerializeField] private float spawnPositionY = 1.25f;
    [SerializeField] private float spawnPositionZ = 40f;
    [SerializeField] private float spawnDelay = 10f;
    // Start is called before the first frame update

    [Header("Boss UI")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject healthBar;

    [SerializeField] private GameObject BossUI;

    void Start()
    {
        Invoke("SpawnBoss", spawnDelay);
    }

    // Update is called once per frame
    private void SpawnBoss(){
        float spawnPositionX = UnityEngine.Random.Range(-spawnPositionXRange, spawnPositionXRange);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
        Instantiate(Boss, spawnPosition, Boss.transform.rotation);

        Boss.GetComponent<BossHealthManager>().gameManager = gameManager;
        Boss.GetComponent<BossHealthManager>().healthText = healthText;
        Boss.GetComponent<BossHealthManager>().healthBar = healthBar;
        Boss.GetComponent<BossUIManager>().BossUI = BossUI;
        BossUI.SetActive(true);
    }
}
