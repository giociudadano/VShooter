using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private GameObject player;
    [SerializeField] private float collisionDamage = 3f;
    //[SerializeField] private float collisionDamageInvulnerabilityTime = 1f;

    void Start() {
      //player = GameObject.Find("Player");
      player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionStay(Collision collision) {
      if (collision.gameObject.CompareTag("Player")){
          player.GetComponent<PlayerHealthManager>().Hurt(collisionDamage);
          Destroy(gameObject);
      }
    }

    void Update() {
    }
}
