using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;
using System.Drawing.Text;

public class UpgradeManager : MonoBehaviour {

	[Header("Scripts")]
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject upgradeUI;
	[SerializeField] private GameObject upgradesListUI;
	[SerializeField] private GameObject upgradesListItem;
	[SerializeField] private GameObject upgradeScripts;

	[Header("Bonus Stats")]
	[SerializeField] public float bonusAttackPercent;
	[SerializeField] public float bonusHealthFlat;
	[SerializeField] public float bonusHealthRegenFlat;
	[SerializeField] public float bonusDefense;
	
	public Dictionary<string, dynamic> upgrades = new Dictionary<string, dynamic>() {
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
						{"1", 0.3f},{"2", 0.4f},{"3", 0.5f}
					}}
				}},
				{"HEAL_AMOUNT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP"},
					{"level", new Dictionary<string, float> () {
						{"1", 20f},{"2", 30f},{"3", 40f}
					}}
				}}
			}}
		}},
		{"MORICALLIOPE_TASTEOFDEATH", new Dictionary<string, dynamic>() {
			{"title", "Taste of Death"},
			{"description", "Defeating an enemy has a {EXPLOSION_CHANCE} chance to create an explosion, dealing {EXPLOSION_DAMAGE} damage. Non-boss enemies caught in the explosion have a {INSTANTKILL_CHANCE} chance of being immediately executed."},
			{"type", "Character Passive"},
			{"icon", "Calliope_TasteOfDeath"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"EXPLOSION_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.15f},{"2", 0.2f},{"3", 0.25f}
					}}
				}},
				{"EXPLOSION_DAMAGE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 60f},{"2", 80f},{"3", 100f}
					}}
				}},
				{"INSTANTKILL_CHANCE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.08f},{"2", 0.1f},{"3", 0.12f}
					}}
				}}
			}}
		}},
		{"MORICALLIOPE_ENDOFALIFE", new Dictionary<string, dynamic>() {
			{"title", "End of a Life"},
			{"description", "Attacks apply a {EFFECT_BURN} that deals {BURN_DAMAGE} damage over 3 seconds. While under the effects of {EFFECT_BURN}, targets that fall below {EXECUTE_THRESHOLD} of their maximum health are immediately executed."},
			{"type", "Character Passive"},
			{"icon", "Calliope_EndOfALife"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"EFFECT_BURN", new Dictionary<string, dynamic> () {
					{"color", "#FDA"},
					{"text", "Burn"}
				}},
				{"BURN_DAMAGE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 15f},{"2", 25f},{"3", 35f}
					}}
				}},
				{"EXECUTE_THRESHOLD", new Dictionary<string	, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0.0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.08f},{"2", 0.12f},{"3", 0.15f}
					}}
				}},
			}}
		}},
		{"GENERIC_IRONSWORD", new Dictionary<string, dynamic>() {
			{"title", "Iron Sword"},
			{"description", "Increases total attack by {ATTACK_PERCENT}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_IronSword"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"ATTACK_PERCENT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f},{"2", 0.3f},{"3", 0.4f},{"4", 0.5f},{"5", 0.6f}
					}}
				}},
			}}
		}},
		{"GENERIC_HEARTGEM", new Dictionary<string, dynamic>() {
			{"title", "Heart Gem"},
			{"description", "Increases base health by {HEALTH_FLAT}. Increases base health regeneration by {HEALTH_REGEN_FLAT}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_HeartGem"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"HEALTH_FLAT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP"},
					{"level", new Dictionary<string, float> () {
						{"1", 50f},{"2", 100f},{"3", 150f},{"4", 200f},{"5", 250f}
					}}
				}},
				{"HEALTH_REGEN_FLAT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"suffix", "HP/SEC"},
					{"level", new Dictionary<string, float> () {
						{"1", 2f},{"2", 4f},{"3", 6f},{"4", 8f},{"5", 10f}
					}}
				}},
			}}
		}},
		{"GENERIC_IRONARMOR", new Dictionary<string, dynamic>() {
			{"title", "Iron Armor"},
			{"description", "Increases base defense by {DEFENSE_FLAT}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_IronArmor"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"DEFENSE_FLAT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 30f},{"2", 60f},{"3", 90f},{"4", 120f},{"5", 150f}
					}}
				}},
			}}
		}},
		{"GENERIC_ETERNALFLAME", new Dictionary<string, dynamic>() {
			{"title", "Eternal Flame"},
			{"description", "Increases CRIT Rate by {CRIT_RATE}. Increases CRIT Damage by {CRIT_DAMAGE}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_IronArmor"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"CRIT_RATE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.06f},{"2", 0.12f},{"3", 0.18f},{"4", 0.24f},{"5", 0.3f}
					}}
				}},
				{"CRIT_DAMAGE", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f},{"2", 0.4f},{"3", 0.6f},{"4", 8f},{"5", 1f}
					}}
				}},
			}}
		}},
	};

	public List<string> upgradesAvailable;

	public Dictionary<string, dynamic> upgradesActive = new Dictionary<string, dynamic>();

	void Start(){
		GetGenericUpgrades();
	}
		
	void Update(){
		
	}

	public void GetCharacterUpgrades(List<string> characterUpgrades){
		upgradesAvailable.AddRange(characterUpgrades);
	}

	private void GetGenericUpgrades() {
		List<string> genericUpgrades = new List<string>(){
			"GENERIC_IRONSWORD", "GENERIC_HEARTGEM", "GENERIC_IRONARMOR", "GENERIC_ETERNALFLAME"
		};
		upgradesAvailable.AddRange(genericUpgrades);
	}

	public void DrawUpgrades() {
		List<int> upgradeIndices = new List<int>();
		for (int i = 0; i < 4; i++) {
			int upgradeIndex;
			while (true){
				upgradeIndex = UnityEngine.Random.Range(0, upgradesAvailable.Count);
				if (!upgradeIndices.Contains(upgradeIndex)){
					upgradeIndices.Add(upgradeIndex);
					break;
				}
			}
			RenderUpgrade(i+1, upgradesAvailable[upgradeIndex], upgrades[upgradesAvailable[upgradeIndex]]);
			//RenderUpgrade(i+1, upgrades.ElementAt(upgradeIndex).Key, upgrades.ElementAt(upgradeIndex).Value);
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
			case "Common Equipment":
				return "<color=#AAA>" + type + "</color>";
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
			string result;
			// For text parameters
			if (parameter.Value.ContainsKey("text")) {
				result = parameter.Value["text"];
				if (parameter.Value.ContainsKey("color")) {
					result = $"<color={parameter.Value["color"]}>" + result + "</color>";
				}
			} else {
			// For variable parameters
				result = $"{parameter.Value["level"][$"{GetCurrentUpgradeLevel(name)+offset}"]}";
				if (parameter.Value.ContainsKey("format")) {
					result = String.Format("{" + parameter.Value["format"] + "}", float.Parse(result));
				}
				if (parameter.Value.ContainsKey("suffix")) {
					result += parameter.Value["suffix"];
				}
				if (parameter.Value.ContainsKey("color")) {
					result = $"<color={parameter.Value["color"]}>" + result + "</color>";
				}
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
		if (upgrades[name]["type"] == "Common Equipment"){
			ApplyPassive(name, null);
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
			case "MORICALLIOPE_TASTEOFDEATH":
				float explosionChance = upgrades["MORICALLIOPE_TASTEOFDEATH"]["parameters"]["EXPLOSION_CHANCE"]["level"][level];
				float explosionDamage = upgrades["MORICALLIOPE_TASTEOFDEATH"]["parameters"]["EXPLOSION_DAMAGE"]["level"][level];
				float instantkillChance = upgrades["MORICALLIOPE_TASTEOFDEATH"]["parameters"]["INSTANTKILL_CHANCE"]["level"][level];
				upgradeScripts.GetComponent<MoriCalliope_TasteOfDeath>().ApplyPassive(parameters["source"], explosionChance, explosionDamage, instantkillChance);
				break;
			case "MORICALLIOPE_ENDOFALIFE":
				float burnDamage = upgrades["MORICALLIOPE_ENDOFALIFE"]["parameters"]["BURN_DAMAGE"]["level"][level];
				float executeThreshold = upgrades["MORICALLIOPE_ENDOFALIFE"]["parameters"]["EXECUTE_THRESHOLD"]["level"][level];
				upgradeScripts.GetComponent<MoriCalliope_EndOfALife>().ApplyPassive(parameters["source"], burnDamage, executeThreshold);
				break;
			case "GENERIC_IRONSWORD":
				float bonusAttackPercent = upgrades["GENERIC_IRONSWORD"]["parameters"]["ATTACK_PERCENT"]["level"][level];
				upgradeScripts.GetComponent<Generic_IronSword>().ApplyPassive(bonusAttackPercent);
				break;
			case "GENERIC_HEARTGEM":
				float bonusHealthFlat = upgrades["GENERIC_HEARTGEM"]["parameters"]["HEALTH_FLAT"]["level"][level];
				float bonusHealthRegenFlat = upgrades["GENERIC_HEARTGEM"]["parameters"]["HEALTH_REGEN_FLAT"]["level"][level];
				upgradeScripts.GetComponent<Generic_HeartGem>().ApplyPassive(bonusHealthFlat, bonusHealthRegenFlat);
				break;
			case "GENERIC_IRONARMOR":
				float bonusDefense = upgrades["GENERIC_IRONARMOR"]["parameters"]["DEFENSE_FLAT"]["level"][level];
				upgradeScripts.GetComponent<Generic_IronArmor>().ApplyPassive(bonusDefense);
				break;
		}
		UpdatePlayerStats();
	}

	public void UpdatePlayerStats() {
		/*
			Recalculates player stats. Called when obtaining an upgrade which would change player stats.
		*/

		// Bonus Attack% Calculation
		float bonusAttackPercent = 0f;
		bonusAttackPercent += upgradeScripts.GetComponent<Generic_IronSword>().bonusAttackPercent;
		this.bonusAttackPercent = bonusAttackPercent;
		string attackText = $"{(int) 20 * (1+bonusAttackPercent)} <color=#FFA>(+{String.Format("{0:0%}", bonusAttackPercent)})</color>";
		upgradeUI.transform.Find("Attack/Value").GetComponent<TMP_Text>().text = attackText;

		// Bonus Health Calculation
		float bonusHealthFlat = 0f;
		bonusHealthFlat += upgradeScripts.GetComponent<Generic_HeartGem>().bonusHealthFlat;
		this.bonusHealthFlat = bonusHealthFlat;
		player.GetComponent<PlayerHealthManager>().SetBonusHealth(bonusHealthFlat);
		string healthText = $"{(int) 500 + bonusHealthFlat} <color=#FFA>(+0%)</color>";
		upgradeUI.transform.Find("Health/Value").GetComponent<TMP_Text>().text = healthText;

		// Bonus Health Regen Calculation
		float bonusHealthRegenFlat = 0f;
		bonusHealthRegenFlat += upgradeScripts.GetComponent<Generic_HeartGem>().bonusHealthRegenFlat;
		this.bonusHealthRegenFlat = bonusHealthRegenFlat;
		player.GetComponent<PlayerHealthManager>().SetBonusHealthRegen(bonusHealthRegenFlat);

		// Bonus Defense Calculation
		float bonusDefense = 0f;
		bonusDefense += upgradeScripts.GetComponent<Generic_IronArmor>().bonusDefense;
		this.bonusDefense = bonusDefense;
		player.GetComponent<PlayerHealthManager>().SetBonusDefense(bonusDefense);
		string defenseText = $"{(int) 35 + bonusDefense} <color=#FFA>(+0%)</color>";
		upgradeUI.transform.Find("Defense/Value").GetComponent<TMP_Text>().text = defenseText;

	}
}
