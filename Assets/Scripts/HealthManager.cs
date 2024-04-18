using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
  // Manages health of the object.
  [SerializeField] private GameObject canvas;
  [SerializeField] private GameObject healthbar;
  [SerializeField] private float maxHealth = 3f;
  private float currentHealth;
  [SerializeField] private float XPReward = 5f;
  [SerializeField] private GameObject XPManager;

  void Start() {
    currentHealth = maxHealth;
    canvas.SetActive(false);
    XPManager = GameObject.Find("XPManager");
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("AllyProjectile")){
      Destroy(collision.gameObject);
      Damage(collision.gameObject, 1f);
    }
  }

  void Update() {
        
  }

  private void Damage(GameObject projectile, float value) {
    canvas.SetActive(true);
    currentHealth -= value;
    healthbar.transform.localScale = new Vector3(currentHealth/maxHealth, 1f, 1f);
    if (currentHealth <= 0f){
      Kill();
    }
  }

  private void Kill() {
    XPManager.GetComponent<XPManager>().GainXP(XPReward);
    Destroy(gameObject);
  }
}
