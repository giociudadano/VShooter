using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinomaeInanis_DarkAura : MonoBehaviour {

	[SerializeField] private float auraSize;

	[SerializeField] private float damageTick;

	[SerializeField] private GameObject player;

	private bool isActive = false;

	public void ApplyPassive(float auraSize, float damageTick) {
		this.auraSize = auraSize;
		this.damageTick = damageTick;
		if (!isActive) {
			isActive = true;
			StartCoroutine("TickDarkAura");
		}
	
	}

	private IEnumerator TickDarkAura() {
		while (true) {
			var colliders = Physics.OverlapSphere(player.transform.position, auraSize/12);
			foreach (var col in colliders){
				if (col.GetComponent<Collider>().CompareTag("Enemy") || col.GetComponent<Collider>().CompareTag("Boss")){
					col.GetComponent<EnemyHealthManager>().Hurt(damageTick);
				};
      		};
			yield return new WaitForSeconds(0.5f);
		}
	}
}
