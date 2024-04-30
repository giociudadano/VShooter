using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_Death : MonoBehaviour {
	/*
		[CHARACTER PASSIVE]
		Character: Mori Calliope
		Name: Death

		Defeating an enemy has an X% chance to create an explosion, dealing X damage. Non-boss enemies caught in the explosion
		have an X% chance of instantly dying.
	*/

	[SerializeField] private float explosionSize = 15.0f;

	public void ApplyPassive(GameObject source, float explosionChance, float explosionDamage, float instantkillChance) {
		float explosionRoll = Random.Range(0f, 1f);
		if (explosionRoll < explosionChance){
			CreateExplosion(source, explosionDamage, instantkillChance);
		}
	}

	private void CreateExplosion(GameObject source, float explosionDamage, float instantkillChance){
	  var colliders = Physics.OverlapSphere(source.transform.position, explosionSize);
      foreach (var col in colliders){
          if (col.GetComponent<Collider>().CompareTag("Enemy")){
            col.GetComponent<EnemyHealthManager>().Hurt(source, explosionDamage);
			float instantkillRoll = Random.Range(0f, 1f);
			if (instantkillRoll < instantkillChance){
				col.GetComponent<EnemyHealthManager>().Kill(source);
			}
          };
      };
	}
}
