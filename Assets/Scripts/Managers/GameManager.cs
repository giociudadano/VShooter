using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Static instance of GameManager
    public static GameManager Instance { get; private set; }

    public bool isGameOver = false;
    [SerializeField] public GameObject gameOverUI;
    [SerializeField] private GameObject upgradeUI;
		[SerializeField] private GameObject gameManager;

		[Header("Cheats")]
    [SerializeField] private bool isCheats = false;
		[SerializeField] private GameObject player;
		[SerializeField] private GameObject bossSpawnManager;



    private void Awake()
    {
        // If an instance already exists and it's not this, destroy this
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this instance
        Instance = this;

        // Make sure this object is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Your start logic here
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isGameOver)
        {
            ActivateCheats(!isCheats);
        }
        if (isCheats && !isGameOver)
        {
            GetKeyDownCheats();
        }
    }

    void GetKeyDownCheats()
    {
				// SHIFT + L: Level up player
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
        {
          player.GetComponent<PlayerXPManager>().LevelUp();
        }
				
				// SHIFT + B: Spawn boss
				if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
					bossSpawnManager.GetComponent<BossSpawnManager>().CancelInvoke("SpawnBoss");
          bossSpawnManager.GetComponent<BossSpawnManager>().SpawnBoss();
        }
				
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void ShowUpgradeUI(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            gameManager.GetComponent<UpgradeManager>().DrawUpgrades();
            upgradeUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            upgradeUI.SetActive(false);
        }
    }

    public void ActivateCheats(bool value)
    {
        isCheats = value;
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void UnpauseGame() {
        Time.timeScale = 1;
    }
}
