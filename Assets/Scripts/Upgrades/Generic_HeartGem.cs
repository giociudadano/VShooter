using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_HeartGem : MonoBehaviour {

	[SerializeField] public float bonusHealthFlat;
	[SerializeField] public float bonusHealthRegenFlat;

	public void ApplyPassive(float bonusHealthFlat, float bonusHealthRegenFlat) {
		this.bonusHealthFlat = bonusHealthFlat;
		this.bonusHealthRegenFlat = bonusHealthRegenFlat;
	}
}