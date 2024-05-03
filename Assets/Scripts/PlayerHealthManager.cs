using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour {
  
    [Header("GUI")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject healText;
    [SerializeField] private GameObject healthBar;

    [Header("Health")]
    [SerializeField] public float currentHealth;
    [SerializeField] private float baseHealth = 50;
    [SerializeField] public float maximumHealth;

    [Header("Health Regen")]
    [SerializeField] private float healthRegen = 1f;

    void Start() {
      maximumHealth = baseHealth;
      currentHealth = maximumHealth;
      StartCoroutine(Regen());
      UpdateHealthbar();
    }

    void Update() {
        
    }

    private void UpdateHealthbar() {
      healthText.text = Mathf.CeilToInt(currentHealth).ToString() + " / " + maximumHealth.ToString();
      healthBar.transform.localScale = new Vector3(currentHealth/maximumHealth, 1f, 1f);
    }

    public void SetBonusHealth(float bonusHealth){
      currentHealth += baseHealth + bonusHealth - maximumHealth;
      maximumHealth = baseHealth + bonusHealth;
      UpdateHealthbar();
    }

    public void Hurt(float damage) {
      currentHealth -= damage;
      if (currentHealth <= 0) {
        currentHealth = 0;
        gameManager.GetComponent<GameManager>().GameOver();
      }
      UpdateHealthbar();
    }
    
    public void Heal(float healAmount) {
      currentHealth += healAmount;
      if (currentHealth > maximumHealth) {
        currentHealth = maximumHealth;
      }
      GameObject healPopup = Instantiate(healText, new Vector3(gameObject.transform.position.x + 0.2f, 1.5f, gameObject.transform.position.z), Quaternion.identity);
      healPopup.transform.Find("HealText").GetComponent<TMP_Text>().text = $"+{healAmount.ToString("0")}HP";
      UpdateHealthbar();
    }

    public void SetBonusHealthRegen(float bonusHealthRegen){
      healthRegen = bonusHealthRegen;
    }

    public IEnumerator Regen() {
      while (true) {
        currentHealth += healthRegen;
        if (currentHealth > maximumHealth) {
          currentHealth = maximumHealth;
        }
        UpdateHealthbar();
        yield return new WaitForSeconds(1f);
      }
    }

}
