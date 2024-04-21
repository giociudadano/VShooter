using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class UpgradeManager : MonoBehaviour {
	[SerializeField] private GameObject upgradeUI;
    
  public Dictionary<string, Dictionary<string, dynamic>> upgrades = new Dictionary<string, Dictionary<string, dynamic>>() {
		{"MORICALLIOPE_SOULHARVESTER", new Dictionary<string, dynamic>() {
			{"title", "Soul Harvester"},
			{"description", "Killing an enemy has a 30% chance to restore 2HP."},
			{"type", "Character Passive"},
			{"level", 1},
			{"parameters", new Dictionary<string, dynamic> () {
				{"HEAL_CHANCE", 0.3},
				{"HEAL_AMOUNT", 2}
			}}
		}}
	};

  void Start(){
        
  }
		
  void Update(){
        
  }

	public void GetUpgradePool() {
		for (int i = 0; i < 4; i++) {
			int upgradeIndex = Random.Range(0, upgrades.Count);
			RenderUpgrade(i+1, upgrades.ElementAt(upgradeIndex).Value);
		}      
	}

	private void RenderUpgrade(int slotIndex, Dictionary<string, dynamic> upgrade) {
		GameObject upgradeTitleUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Title").gameObject;
		upgradeTitleUI.GetComponent<TMP_Text>().text = $"{upgrade["title"]} <color=#FFA>LV {upgrade["level"]}</color>";
		GameObject upgradeDescriptionUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Description").gameObject;
		upgradeDescriptionUI.GetComponent<TMP_Text>().text = $"{upgrade["description"]}";
	}

}
