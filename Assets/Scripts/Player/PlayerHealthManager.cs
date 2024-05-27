using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour {
  
    [Header("GUI")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject healText;
    
    [SerializeField] private GameObject hurtText;
    [SerializeField] private GameObject healthBar;

    [Header("Health")]
    [SerializeField] public float currentHealth;
    [SerializeField] private float baseHealth = 500;
    [SerializeField] public float maximumHealth;

    [Header("Defense")]
    [SerializeField] public float defense;
    [SerializeField] private float baseDefense = 35;

    [Header("Health Regen")]
    [SerializeField] public float healthRegen = 0f;

    void Start() {
      maximumHealth = baseHealth;
      currentHealth = maximumHealth;
      defense = baseDefense;
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

    public void SetBonusDefense(float bonusDefense){
      defense = baseDefense + bonusDefense;
    }

    public void Hurt(float rawDamage) {
      /*
        Subtracts health from the player using the following formula:
          [Net Damage = Raw Damage * (100 / 100 + Defense)]

        Defense: For every 100 Armor, gain 1% Effective Health.

        Example:
          100 Raw Damage vs. 100 Defense = 100 * (100/100+100) = 50 Net Damage
          100 Raw Damage vs. 200 Defense = 100 * (100/100+200) = 33 Net Damage
      */
      float netDamage = rawDamage * (100 / (100 + defense));
      currentHealth -= netDamage;
      if (currentHealth <= 0) {
        currentHealth = 0;
        gameManager.GetComponent<GameManager>().GameOver();
      }
      GameObject hurtPopup = Instantiate(hurtText, new Vector3(gameObject.transform.position.x - 2f, 1.5f, gameObject.transform.position.z), Quaternion.identity);
      hurtPopup.transform.Find("HurtText").GetComponent<TMP_Text>().text = $"-{netDamage:0}HP";
      UpdateHealthbar();
    }

    public void TrueHurt(float trueDamage)
    {
      /*
          Endpoint for dealing true, unmitigated damage to player.
      */
      
      float newHealth = currentHealth - trueDamage;
      //  Clamp minimum health to 1
      if(newHealth <= 0){
        currentHealth = 1;
      } else {
        currentHealth = newHealth;
      }
      GameObject hurtPopup = Instantiate(hurtText, new Vector3(gameObject.transform.position.x - 2f, 1.5f, gameObject.transform.position.z), Quaternion.identity);
      hurtPopup.transform.Find("HurtText").GetComponent<TMP_Text>().text = $"-{trueDamage:0}HP";
      UpdateHealthbar();
    }
    
    public void Heal(float healAmount) {
      currentHealth += healAmount;
      if (currentHealth > maximumHealth) {
        currentHealth = maximumHealth;
      }
      GameObject healPopup = Instantiate(healText, new Vector3(gameObject.transform.position.x + 2f, 1.5f, gameObject.transform.position.z), Quaternion.identity);
      healPopup.transform.Find("HealText").GetComponent<TMP_Text>().text = $"+{healAmount:0}HP";
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
