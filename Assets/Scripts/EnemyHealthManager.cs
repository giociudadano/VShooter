using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
  // Manages health of the object.

  
  [SerializeField] private GameObject canvas;
  [SerializeField] private GameObject healthbar;

  [SerializeField] private GameObject damageText;
  [SerializeField] private float maxHealth = 3f;
  private float currentHealth;
  [SerializeField] private float XPReward = 5f;
  private GameObject player;

  [Header("Managers")]
  private UpgradeManager upgradeManager;
  private PlayerXPManager playerXPManager;
  private GameObject gameManager;
  void Start() {
    currentHealth = maxHealth;
    canvas.SetActive(false);
    player = GameObject.FindGameObjectWithTag("Player");
    gameManager = GameObject.FindGameObjectWithTag("GameManager");
    upgradeManager = gameManager.GetComponent<UpgradeManager>();
    playerXPManager = player.GetComponent<PlayerXPManager>();
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("AllyProjectile")){
      Destroy(collision.gameObject);
      Hurt(collision.gameObject, GetDamage(collision.gameObject.GetComponent<BasicProjectile>().projectileDamage));
    }
  }

  private float GetDamage(float basicDamage){
    float totalDamage = Random.Range(basicDamage * 0.9f, basicDamage * 1.1f);
    return totalDamage;
  }

  void Update() {
        
  }

  public void Hurt(GameObject source, float rawDamage) {
    // Net Damage Calculation
    float netDamage;
    if (source.CompareTag("AllyProjectile")){
      netDamage = Random.Range(rawDamage * 0.9f, rawDamage * 1.1f);
    } else {
      netDamage = rawDamage;
    }
    currentHealth -= netDamage;
    if (currentHealth <= 0f){
      Kill(source);
    }

    // Upgrade effects
    if (source.CompareTag("AllyProjectile")){
      Dictionary<string, dynamic> onHitPassives = new Dictionary<string, dynamic>() {
        {"MORICALLIOPE_ENDOFALIFE", new Dictionary<string, dynamic>() {
          {"source", gameObject}
        }}
      };
      upgradeManager.ApplyPassive(onHitPassives);
    }

    // Health bar and damage text display
    canvas.SetActive(true);
    healthbar.transform.localScale = new Vector3(currentHealth/maxHealth, 1f, 1f);
    GameObject damagePopup = Instantiate(damageText, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
    damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = netDamage.ToString("0");
  }

  public void Kill(GameObject source) {
    // Apply only these passives to projectile kills.
    if (source.CompareTag("AllyProjectile")){
      Dictionary<string, dynamic> onKillPassives = new Dictionary<string, dynamic>() {
        {"MORICALLIOPE_SOULHARVESTER", null},
        {"MORICALLIOPE_TASTEOFDEATH", new Dictionary<string, dynamic>() {
          {"source", gameObject}
        }}
      };
      upgradeManager.ApplyPassive(onKillPassives);
    }
    playerXPManager.GainXP(XPReward);
    Destroy(gameObject);
  }
}


