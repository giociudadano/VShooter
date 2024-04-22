using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool isGameOver = false;

    [SerializeField] public GameObject gameOverUI;
    [SerializeField] private GameObject upgradeUI;

    [SerializeField] private GameObject gameManager;

    void Start() {
        
    }

    void Update() {
        
    }

    public void GameOver() {
      isGameOver = true;
      Time.timeScale = 0;
      gameOverUI.SetActive(true);
    }
    public void ShowUpgradeUI() {
      Time.timeScale = 0;
      gameManager.GetComponent<UpgradeManager>().DrawUpgrades();
      upgradeUI.SetActive(true);
    }
    
    public void HideUpgradeUI() {
      Time.timeScale = 1;
      upgradeUI.SetActive(false);
    }
}
