using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private GameObject player;
    [SerializeField] private float collisionDamage = 30f;

    void Start() {
      player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision other) {
      if (other.gameObject.CompareTag("Player")){
          player.GetComponent<PlayerHealthManager>().Hurt(collisionDamage);
          Destroy(gameObject);
      }
    }

    void Update() {
    }
}
