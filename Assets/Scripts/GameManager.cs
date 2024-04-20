using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool isGameOver = false;

    [SerializeField] public GameObject gameOverUI;

    void Start() {
        
    }

    void Update() {
        
    }

    public void GameOver() {
      isGameOver = true;
      Time.timeScale = 0;
      gameOverUI.SetActive(true);
    }
}
