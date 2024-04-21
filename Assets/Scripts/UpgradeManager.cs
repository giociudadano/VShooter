using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class UpgradeManager : MonoBehaviour {
	[SerializeField] private GameObject upgradeUI;
    
  public Dictionary<string, Dictionary<string, dynamic>> upgrades = new Dictionary<string, Dictionary<string, dynamic>>() {
		{"MORICALLIOPE_SOULHARVESTER", new Dictionary<string, dynamic>() {
			{"title", "Soul Harvester"},
			{"description", "Defeating an enemy has a {HEAL_CHANCE} chance to restore {HEAL_AMOUNT}."},
			{"type", "Character Passive"},
			{"icon", "Calliope_SoulHarvester"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"HEAL_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFF"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f}
					}}
				}},
				{"HEAL_AMOUNT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP"},
					{"level", new Dictionary<string, float> () {
						{"1", 2f}
					}}
				}}
			}}
		}},
	};

  void Start(){
        
  }
		
  void Update(){
        
  }

	public void GetUpgradePool() {
		for (int i = 0; i < 4; i++) {
			int upgradeIndex = UnityEngine.Random.Range(0, upgrades.Count);
			RenderUpgrade(i+1, upgrades.ElementAt(upgradeIndex).Key, upgrades.ElementAt(upgradeIndex).Value);
		}      
	}

	private void RenderUpgrade(int slotIndex, string name, Dictionary<string, dynamic> upgrade) {
		GameObject upgradeTitleUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Title").gameObject;
		upgradeTitleUI.GetComponent<TMP_Text>().text = $"{upgrade["title"]} <color=#AAA>LV {GetUpgradeLevel(name)}</color>";
		GameObject upgradeDescriptionUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Description").gameObject;
		upgradeDescriptionUI.GetComponent<TMP_Text>().text = ParseAbilityDescription(name, $"{upgrade["description"]}", upgrade["parameters"]);
		GameObject upgradeTypeUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Type").gameObject;
		upgradeTypeUI.GetComponent<TMP_Text>().text = ParseAbilityType(upgrade["type"]);
		if (upgrade.ContainsKey("icon")){
			GameObject upgradeImageUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Image").gameObject;
			upgradeImageUI.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrade["icon"]}");
		}
	}

	private string ParseAbilityType(string type) {
		switch (type) {
			case "Character Passive":
				return "<color=#FFA>" + type + "</color>";
			default:
			  return "";
		}
	}

	private string ParseAbilityDescription(string name, string description, Dictionary<string, dynamic> parameters) {
	  foreach (var parameter in parameters) {
			string query = "{" + parameter.Key + "}";
			string result = $"{parameter.Value["level"][$"{GetUpgradeLevel(name)}"]}";
			if (parameter.Value.ContainsKey("format")) {
				result = String.Format("{" + parameter.Value["format"] + "}", float.Parse(result));
			}
			if (parameter.Value.ContainsKey("suffix")) {
				result += parameter.Value["suffix"];
			}
			if (parameter.Value.ContainsKey("color")) {
				result = $"<color={parameter.Value["color"]}>" + result + "</color>";
			}
			description = description.Replace(query, result);
	  }
	  return description;
	}

	private int GetUpgradeLevel(String name) {
		return 1;
	}

}
