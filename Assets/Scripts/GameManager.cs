using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public bool isGameOver = false;

  [SerializeField] public GameObject gameOverUI;
  [SerializeField] private GameObject upgradeUI;
  [SerializeField] private GameObject gameManager;
  [SerializeField] private bool isCheats = false;
	[SerializeField] private GameObject player;

  void Start() {
        
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.C) && !isGameOver) {
			ActivateCheats(!isCheats);
		}
		if (isCheats && !isGameOver){
			GetKeyDownCheats();
		}
  }

	void GetKeyDownCheats() {
		if (Input.GetKeyDown(KeyCode.L)) {
			player.GetComponent<PlayerXPManager>().LevelUp();
		}
	}

  public void GameOver() {
    isGameOver = true;
    Time.timeScale = 0;
    gameOverUI.SetActive(true);
  }
  public void ShowUpgradeUI(bool value) {
		if (value) {
			Time.timeScale = 0;
			gameManager.GetComponent<UpgradeManager>().DrawUpgrades();
			upgradeUI.SetActive(true);
		} else {
			Time.timeScale = 1;
    	upgradeUI.SetActive(false);
		}
  }

	public void ActivateCheats(bool value) {
		if (value) {
			isCheats = true;
		} else {
			isCheats = false;
		}
	}
}
