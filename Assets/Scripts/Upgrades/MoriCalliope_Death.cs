using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_Death : MonoBehaviour {

	[SerializeField] private float explosionSize = 14.0f;

	public void ApplyPassive(GameObject source, float explosionChance, float explosionDamage, float instantkillChance) {
		float explosionRoll = Random.Range(0f, 1f);
		explosionChance = 1f; //DEBUG MODE
		if (explosionRoll < explosionChance){
			CreateExplosion(source, explosionDamage, instantkillChance);
		}
		print($"Rolled {explosionRoll}");
	}

	private void CreateExplosion(GameObject source, float explosionDamage, float instantkillChance){
	  var colliders = Physics.OverlapSphere(source.transform.position, explosionSize);
        foreach (var col in colliders){
          if (col.GetComponent<Collider>().CompareTag("Enemy")){
			float instantkillRoll = Random.Range(0f, 1f);
            Destroy(col.GetComponent<Collider>().gameObject);
			print("Instakilled target");
          };
      };
	}
}
