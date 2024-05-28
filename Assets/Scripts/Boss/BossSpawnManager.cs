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
    [SerializeField] public float spawnDelay = 10f;
    // Start is called before the first frame update

    [Header("Boss UI")]
    private GameObject gameManager;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject healthBar;

    public GameObject BossUI;

    [Header("Others")]

    [SerializeField] private int bossMusic;
    private MusicPlayer musicPlayer;
    private GameObject mookSpawner;

    void Start()
    {
        mookSpawner = GameObject.FindGameObjectWithTag("Spawner");
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
        Invoke("SpawnBoss", spawnDelay);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject;
    }
    public void SpawnBoss()
    {
        DisableMooks();
        Debug.Log("Disabled mooks!");

        SetBossMusic();

        float spawnPositionX = UnityEngine.Random.Range(-spawnPositionXRange, spawnPositionXRange);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
        GameObject currentBoss = Instantiate(Boss, spawnPosition, Boss.transform.rotation);

        currentBoss.GetComponent<BossHealthManager>().gameManager = gameManager;
        currentBoss.GetComponent<BossHealthManager>().healthText = healthText;
        currentBoss.GetComponent<BossHealthManager>().healthBar = healthBar;
        currentBoss.GetComponent<BossUIManager>().BossUI = BossUI;
        currentBoss.GetComponent<BossHealthManager>().bossSpawner = gameObject;
    }

    private void DisableMooks()
    {
        
        mookSpawner.SetActive(false);
    }

    private void SetBossMusic()
    {
        musicPlayer.PlayTrack(bossMusic);
    }
}
