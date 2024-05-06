using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_TopazStaff : MonoBehaviour {

	[SerializeField] public float bonusAbilityHasteFlat;

	public void ApplyPassive(float bonusAbilityHasteFlat) {
		this.bonusAbilityHasteFlat = bonusAbilityHasteFlat;
	}
}