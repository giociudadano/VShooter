using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinomaeInanis_BlessingsOfTheGods : MonoBehaviour {

	[SerializeField] private float summonRate;

	[SerializeField] private GameObject tako;

	private bool isActive = false;

	public void ApplyPassive(float summonRate) {
		this.summonRate = summonRate;
		if (!isActive) {
			isActive = true;
			StartCoroutine("SummonTakodachi");
		}
	
	}

	private IEnumerator SummonTakodachi() {
		while (true) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			Instantiate(tako, player.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(summonRate);
		}
	}
}
