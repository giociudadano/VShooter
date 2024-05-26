using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private GameObject player;
    [SerializeField] public float collisionDamage = 30f;

    private SfxManager sfx;

    void Start() {
      sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
      player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision other) {
      if (other.gameObject.CompareTag("Player")){
          sfx.PlayKillSfx();
          player.GetComponent<PlayerHealthManager>().Hurt(collisionDamage);
          Destroy(gameObject);
      }
    }

    void Update() {
    }
}
