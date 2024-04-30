using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
  // Manages health of the object.

  [SerializeField] private GameObject gameManager;
  [SerializeField] private GameObject canvas;
  [SerializeField] private GameObject healthbar;
  [SerializeField] private float maxHealth = 3f;
  private float currentHealth;
  [SerializeField] private float XPReward = 5f;
  [SerializeField] private GameObject player;

  void Start() {
    currentHealth = maxHealth;
    canvas.SetActive(false);
    player = GameObject.Find("Player");
    gameManager = GameObject.Find("GameManager");
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("AllyProjectile")){
      Destroy(collision.gameObject);
      Hurt(1f);
    }
  }

  void Update() {
        
  }

  public void Hurt(float value) {
    canvas.SetActive(true);
    currentHealth -= value;
    if (currentHealth <= 0f){
      Kill();
    }
    healthbar.transform.localScale = new Vector3(currentHealth/maxHealth, 1f, 1f);
  }

  private void Kill() {
    Dictionary<string, dynamic> onKillPassives = new Dictionary<string, dynamic>() {
      {"MORICALLIOPE_SOULHARVESTER", null},
      {"MORICALLIOPE_DEATH", new Dictionary<string, dynamic>() {
        {"source", gameObject}
      }}
    };
    gameManager.GetComponent<UpgradeManager>().ApplyPassive(onKillPassives);
    player.GetComponent<PlayerXPManager>().GainXP(XPReward);
    Destroy(gameObject);
  }
}
