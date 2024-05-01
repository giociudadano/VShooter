using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_EndOfALife : MonoBehaviour {
	/*
		[CHARACTER PASSIVE]
		Character: Mori Calliope
		Name: Taste Of Death

		Attacks apply a Burn that deals X damage over 3 seconds. While under the effects of Burn, targets that fall below
		X% of their maximum health are immediately executed.
	*/

	[SerializeField] private float effectDuration = 3f;

	public void ApplyPassive(GameObject source, float burnDamage, float executionThreshold) {
		source.gameObject.GetComponent<EnemyHealthManager>().ApplyBurn(burnDamage, effectDuration);
		source.gameObject.GetComponent<EnemyHealthManager>().ApplyExecuteThreshold(executionThreshold);
	}
}
