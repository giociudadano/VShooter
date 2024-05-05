using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_EternalFlame : MonoBehaviour {

	[SerializeField] public float bonusCritRate = 0f;
	[SerializeField] public float bonusCritDamage = 0f;

	public void ApplyPassive(float bonusCritRate, float bonusCritDamage) {
		this.bonusCritRate = bonusCritRate;
		this.bonusCritDamage = bonusCritDamage;
	}
}