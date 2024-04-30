using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class UpgradeManager : MonoBehaviour {
	[SerializeField] private GameObject upgradeUI;

	[SerializeField] private GameObject upgradesListUI;
	[SerializeField] private GameObject upgradesListItem;

	[SerializeField] private GameObject upgradeScripts;
    
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
						{"1", 0.3f},{"2", 0.3f},{"3", 0.4f},{"4", 0.4f},{"5", 0.5f}
					}}
				}},
				{"HEAL_AMOUNT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP"},
					{"level", new Dictionary<string, float> () {
						{"1", 2f},{"2", 3f},{"3", 3f},{"4", 4f},{"5", 4f}
					}}
				}}
			}}
		}},
		{"MORICALLIOPE_DEATH", new Dictionary<string, dynamic>() {
			{"title", "Death"},
			{"description", "Defeating an enemy has a {EXPLOSION_CHANCE} chance to create an explosion, dealing {EXPLOSION_DAMAGE} damage. Non-boss enemies caught in the explosion have a {INSTANTKILL_CHANCE} chance of immediately dying."},
			{"type", "Character Passive"},
			{"icon", "Calliope_Death"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"EXPLOSION_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f},{"2", 0.25f},{"3", 0.25f},{"4", 0.3f},{"5", 0.3f}
					}}
				}},
				{"EXPLOSION_DAMAGE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 3f},{"2", 3f},{"3", 4f},{"4", 4f},{"5", 5f}
					}}
				}},
				{"INSTANTKILL_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.08f},{"2", 0.09f},{"3", 0.1f},{"4", 0.11f},{"5", 0.12f}
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
		for (int i = 0; i < 2; i++) {
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
		foreach (var (name, index) in upgradesActive.Keys.Select((value, i) => (value, i))) {
			GameObject upgradeItem = Instantiate(upgradesListItem, upgradesListUI.transform);
			upgradeItem.name = name;
			upgradeItem.transform.localPosition = new Vector3(index * 25, 0, 0);
			GameObject upgradeItemImage = upgradeItem.transform.Find("Image").gameObject;
			upgradeItemImage.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrades[name]["icon"]}");
			GameObject upgradeItemLevel = upgradeItem.transform.Find("Level").gameObject;
			upgradeItemLevel.GetComponent<TMP_Text>().text = upgradesActive[name]["level"].ToString();
		}
	}

	public void ApplyPassive(Dictionary<string, dynamic> passives){
		foreach (string passiveName in passives.Keys){
			ApplyPassive(passiveName, passives[passiveName]);
		}
	}

	public void ApplyPassive(string passiveName, Dictionary<string, dynamic> parameters) {
		if (!upgradesActive.ContainsKey(passiveName)){
			return;
		}
		string level = upgradesActive[passiveName]["level"].ToString();
		switch (passiveName) {
			case "MORICALLIOPE_SOULHARVESTER":
				float chance = upgrades["MORICALLIOPE_SOULHARVESTER"]["parameters"]["HEAL_CHANCE"]["level"][level];
				float amount = upgrades["MORICALLIOPE_SOULHARVESTER"]["parameters"]["HEAL_AMOUNT"]["level"][level];
				upgradeScripts.GetComponent<MoriCalliope_SoulHarvester>().ApplyPassive(chance, amount);
				break;
			case "MORICALLIOPE_DEATH":
				float explosionChance = upgrades["MORICALLIOPE_DEATH"]["parameters"]["EXPLOSION_CHANCE"]["level"][level];
				float explosionDamage = upgrades["MORICALLIOPE_DEATH"]["parameters"]["EXPLOSION_DAMAGE"]["level"][level];
				float instantkillChance = upgrades["MORICALLIOPE_DEATH"]["parameters"]["INSTANTKILL_CHANCE"]["level"][level];
				upgradeScripts.GetComponent<MoriCalliope_Death>().ApplyPassive(parameters["source"], explosionChance, explosionDamage, instantkillChance);
				break;
		}
	}
}
