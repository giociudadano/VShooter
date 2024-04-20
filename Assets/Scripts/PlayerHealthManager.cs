using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour {
  
    [SerializeField] private GameObject gameManager;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maximumHealth = 50;
    [SerializeField] private GameObject healthBar;


    void Start() {
      currentHealth = maximumHealth;
      UpdateHealthbar();
    }

    void Update() {
        
    }

    private void UpdateHealthbar() {
      healthText.text = currentHealth.ToString() + " / " + maximumHealth.ToString();
      healthBar.transform.localScale = new Vector3(currentHealth/maximumHealth, 1f, 1f);
    }

    public void Hurt(float damage) {
      currentHealth -= damage;
      if (currentHealth <= 0) {
        currentHealth = 0;
        gameManager.GetComponent<GameManager>().GameOver();
      }
      UpdateHealthbar();
    }
    
}
