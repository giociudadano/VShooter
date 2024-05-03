using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_IronSword : MonoBehaviour {

	[SerializeField] public float bonusAttackPercent = 0;

	public void ApplyPassive(float bonusAttackPercent) {
		this.bonusAttackPercent = bonusAttackPercent;
	}
}