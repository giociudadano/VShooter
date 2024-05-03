using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_IronArmor : MonoBehaviour {

	[SerializeField] public float bonusDefense = 0;

	public void ApplyPassive(float bonusDefense) {
		this.bonusDefense = bonusDefense;
	}	
}