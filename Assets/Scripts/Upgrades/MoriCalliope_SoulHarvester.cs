using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_SoulHarvester : MonoBehaviour {
	/*
		[CHARACTER PASSIVE]
		Character: Mori Calliope
		Name: Soul Harvester

		Defeating an enemy has an X% chance to restore X HP.
	*/

	[SerializeField] private GameObject player;

	public void ApplyPassive(float chance, float amount) {
		float roll = Random.Range(0f, 1f);
		if (chance < roll) {
			player.GetComponent<PlayerHealthManager>().Heal(amount);
		}
	}
}
