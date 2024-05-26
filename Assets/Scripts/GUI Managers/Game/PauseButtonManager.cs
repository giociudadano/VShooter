using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonManager : MonoBehaviour {
    
    [SerializeField] private GameObject gameManager;

    [SerializeField] private Sprite icon_pauseGame;

    [SerializeField] private Sprite icon_unpauseGame;

    private bool isPaused = false;

    public void OnClick() {
        if (!isPaused) {
            gameManager.GetComponent<GameManager>().PauseGame();
            isPaused = true;
            GetComponent<UnityEngine.UI.Image>().sprite = icon_unpauseGame;
        } else {
            gameManager.GetComponent<GameManager>().UnpauseGame();
            isPaused = false;
            GetComponent<UnityEngine.UI.Image>().sprite = icon_pauseGame;
        }
    }
}
