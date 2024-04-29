using System.Collections;
using System.Collections.Generic;
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
    player = GameObject.FindGameObjectWithTag("Player");
    gameManager = GameObject.Find("GameManager");
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("AllyProjectile")){
      Destroy(collision.gameObject);
      Hurt(collision.gameObject, 1f);
    }
  }

  void Update() {
        
  }

  private void Hurt(GameObject projectile, float value) {
    canvas.SetActive(true);
    currentHealth -= value;
    healthbar.transform.localScale = new Vector3(currentHealth/maxHealth, 1f, 1f);
    if (currentHealth <= 0f){
      Kill();
    }
  }

  private void Kill() {
    gameManager.GetComponent<UpgradeManager>().ApplyPassive("MORICALLIOPE_SOULHARVESTER");
    player.GetComponent<PlayerXPManager>().GainXP(XPReward);
    Destroy(gameObject);
  }
}
