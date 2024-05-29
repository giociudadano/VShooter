using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinomaeInanis_Spellcaster : MonoBehaviour {

	[SerializeField] public float bonusAbilityHasteFlat;

	[SerializeField] public float bonusAbilityHastePercent;

	public void ApplyPassive(float bonusAbilityHasteFlat, float bonusAbilityHastePercent) {
		this.bonusAbilityHasteFlat = bonusAbilityHasteFlat;
		this.bonusAbilityHastePercent = bonusAbilityHastePercent;
	}
}
