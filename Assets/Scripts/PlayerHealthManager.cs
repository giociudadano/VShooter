using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour {

    [SerializeField] private TMP_Text healthText;
    [SerializeField] public float maxHealth = 50;
    [SerializeField] public float currentHealth;
    [SerializeField] private GameObject healthBar;


    void Start() {
      currentHealth = maxHealth;
      healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
      healthBar.transform.localScale = new Vector3(currentHealth/maxHealth, 1f, 1f);
    }

    void Update() {
        
    }
}
