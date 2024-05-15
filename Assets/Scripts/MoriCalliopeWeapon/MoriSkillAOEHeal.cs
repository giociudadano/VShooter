using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriSkillAOEHeal : MonoBehaviour
{
    [SerializeField] private float areaSize = 15.0f;
    [SerializeField] private float healAmount = 10.0f;
    [SerializeField] private float maxHealing = 60.0f;
    private GameObject player;
    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AOEHealing(healAmount);
    }

    // Update is called once per frame
    private void AOEHealing(float healAmount){
	  var colliders = Physics.OverlapSphere(player.transform.position, areaSize);
      foreach (var col in colliders){
          if (col.GetComponent<Collider>().CompareTag("Enemy") || col.GetComponent<Collider>().CompareTag("Boss")){
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
