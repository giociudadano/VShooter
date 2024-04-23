using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Drawing;

public class UpgradeManager : MonoBehaviour {
	[SerializeField] private GameObject upgradeUI;

	[SerializeField] private GameObject upgradesListUI;
	[SerializeField] private GameObject upgradesListItem;
    
  public Dictionary<string, Dictionary<string, dynamic>> upgrades = new Dictionary<string, Dictionary<string, dynamic>>() {
		{"MORICALLIOPE_SOULHARVESTER", new Dictionary<string, dynamic>() {
			{"title", "Soul Harvester"},
			{"description", "Defeating an enemy has a {HEAL_CHANCE} chance to restore {HEAL_AMOUNT}."},
			{"type", "Character Passive"},
			{"icon", "Calliope_SoulHarvester"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"HEAL_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f},{"2", 0.2f},{"3", 0.3f},{"4", 0.3f},{"5", 0.4f}
					}}
				}},
				{"HEAL_AMOUNT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP"},
					{"level", new Dictionary<string, float> () {
						{"1", 1.5f},{"2", 2.5f},{"3", 2.5f},{"4", 3.5f},{"5", 3.5f}
					}}
				}}
			}}
		}},
	};

	public Dictionary<string, Dictionary<string, dynamic>> upgradesActive = new Dictionary<string, Dictionary<string, dynamic>>();

  void Start(){
        
  }
		
  void Update(){
        
  }

	public void DrawUpgrades() {
		for (int i = 0; i < 1; i++) {
			int upgradeIndex = UnityEngine.Random.Range(0, upgrades.Count);
			RenderUpgrade(i+1, upgrades.ElementAt(upgradeIndex).Key, upgrades.ElementAt(upgradeIndex).Value);
		}      
	}

	private void RenderUpgrade(int slotIndex, string name, Dictionary<string, dynamic> upgrade) {
		GameObject upgradeNameUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Name").gameObject;
		upgradeNameUI.GetComponent<TMP_Text>().text = name;
		GameObject upgradeTitleUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Title").gameObject;
		upgradeTitleUI.GetComponent<TMP_Text>().text = $"{upgrade["title"]} <color=#FFA>LV {GetCurrentUpgradeLevel(name)+1}</color>";
		GameObject upgradeDescriptionUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Description").gameObject;
		upgradeDescriptionUI.GetComponent<TMP_Text>().text = ParseAbilityDescription(name, $"{upgrade["description"]}", upgrade["parameters"], 1);
		GameObject upgradeTypeUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Type").gameObject;
		upgradeTypeUI.GetComponent<TMP_Text>().text = ParseAbilityType(upgrade["type"]);
		if (upgrade.ContainsKey("icon")){
			GameObject upgradeImageUI = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString() + "/Upgrade Image").gameObject;
			upgradeImageUI.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrade["icon"]}");
		}
	}

	public string ParseAbilityType(string type) {
		switch (type) {
			case "Character Passive":
				return "<color=#FFA>" + type + "</color>";
			default:
			  return "";
		}
	}

	public string ParseAbilityDescription(string name, string description, Dictionary<string, dynamic> parameters) {
	  return ParseAbilityDescription(name, description, parameters, 0);
	}
	public string ParseAbilityDescription(string name, string description, Dictionary<string, dynamic> parameters, int offset) {
	  foreach (var parameter in parameters) {
			string query = "{" + parameter.Key + "}";
			string result = $"{parameter.Value["level"][$"{GetCurrentUpgradeLevel(name)+offset}"]}";
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
	public int GetCurrentUpgradeLevel(String name) {
		if (upgradesActive.ContainsKey(name)) {
			return upgradesActive[name]["level"];
		}
		return 0;
	}

	public void GetUpgrade(String name) {
		if (upgradesActive.ContainsKey(name)) {
			upgradesActive[name]["level"] += 1;
		} else {
			upgradesActive.Add(name, new Dictionary<string, dynamic> () {
				{"level", 1}
			});
		}
		RenderUpgradesListUI();
	}

	private void RenderUpgradesListUI() {
		foreach (Transform child in upgradesListUI.transform) {
      DestroyImmediate(child.gameObject);
    }
		foreach (string name in upgradesActive.Keys) {
			GameObject upgradeItem = Instantiate(upgradesListItem, upgradesListUI.transform);
			upgradeItem.name = name;
			GameObject upgradeItemImage = upgradeItem.transform.Find("Image").gameObject;
			upgradeItemImage.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrades[name]["icon"]}");
			GameObject upgradeItemLevel = upgradeItem.transform.Find("Level").gameObject;
			upgradeItemLevel.GetComponent<TMP_Text>().text = upgradesActive[name]["level"].ToString();
		}
	}

}
