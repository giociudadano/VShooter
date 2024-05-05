using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_CivilizationFeather : MonoBehaviour {

	[SerializeField] public float bonusAttackSpeed;

	public void ApplyPassive(float bonusAttackSpeed) {
		this.bonusAttackSpeed = bonusAttackSpeed;
	}
}