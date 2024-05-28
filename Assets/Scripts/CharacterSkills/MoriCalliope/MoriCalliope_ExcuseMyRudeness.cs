using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_ExcuseMyRudeness_Projectile : MonoBehaviour {
    [Header("Projectile Properties")]
    [SerializeField] private float rotateSpeed = 500f;
    private float rotateDegrees = 350f;
    private float counter = 0;
    [SerializeField] private float damage = 40f;

    
    [Header("Heal Properties")]
    [SerializeField] private float areaSize = 15.0f;
    [SerializeField] private float healAmount = 10.0f;
    [SerializeField] private float maxHealing = 60.0f;

    [Header("Spell Properties")]

    private GameObject player;
    
    private SfxManager sfx;
    [SerializeField] AudioClip spinSfx;

    // Start is called before the first frame update

    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        sfx.PlayOneShot(spinSfx);
        AOEHealing(healAmount);
    }
    
    void Update() {
        CastSkill();
        FollowPlayer();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().Hurt(damage);
        }
        if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void CastSkill() {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if(Mathf.Abs(transform.rotation.eulerAngles.y)>= rotateDegrees) {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer() {
        transform.position = player.transform.position;
    }

    private void AOEHealing(float healAmount){
	  var colliders = Physics.OverlapSphere(player.transform.position, areaSize);
      foreach (var col in colliders){
          if (col.GetComponent<Collider>().CompareTag("Enemy")){
            counter++;
          };
      };
      healAmount *= counter;
      healAmount = Math.Min(healAmount, maxHealing);
      if(healAmount > 0)
      {
        player.GetComponent<PlayerHealthManager>().Heal(healAmount);
      }
	}
}
