using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private GameObject player;

    [SerializeField] private float collisionDamage = 3f;
    private bool collisionDamageInvulnerability = false;
    [SerializeField] private float collisionDamageInvulnerabilityTime = 1f;

    void Start() {
      player = GameObject.Find("Player");
    }

    void OnCollisionStay(Collision collision) {
      if (collision.gameObject.CompareTag("Player")){
        if (!collisionDamageInvulnerability){
          collisionDamageInvulnerability = true;
          Invoke("DisableCollisionDamageInvulnerability", collisionDamageInvulnerabilityTime);
          player.GetComponent<PlayerHealthManager>().Hurt(collisionDamage);
        }
      }
    }

    private void DisableCollisionDamageInvulnerability() {
      collisionDamageInvulnerability = false;
    }

    void Update() {
    }
}
