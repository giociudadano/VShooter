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
	[SerializeField] public float bonusCritRate;
	[SerializeField] public float bonusCritDamage;
	[SerializeField] public float bonusAttackSpeed;
	[SerializeField] public float bonusAbilityHasteFlat;

	private float MAXIMUM_PASSIVE_LEVEL = 3;
	private string characterName;
	private List<string> characterUpgrades;
	
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
		{"INA_BLESSINGSOFTHEGODS", new Dictionary<string, dynamic>() {
			{"title", "Blessings of the Gods"},
			{"description", "Every {SUMMON_RATE} seconds, summon a <color=#DAF>Takodachi</color> at the player's location for 12 seconds. <color=#DAF>Takodachis</color> deal <color=#FFA>30</color> damage to all enemies hit and restores <color=#AFA>20HP</color> to the player on expiry."},
			{"type", "Character Passive"},
			{"icon", "Ina_BlessingsOfTheGods"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"SUMMON_RATE", new Dictionary<string	, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 14f},{"2", 12f},{"3", 10f}
					}}
				}},
			}}
		}},
		{"INA_DARKAURA", new Dictionary<string, dynamic>() {
			{"title", "Dark Aura"},
			{"description", "Enemies with {AURA_SIZE} units of you take {DAMAGE_TICK} damage per second."},
			{"type", "Character Passive"},
			{"icon", "Ina_DarkAura"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"AURA_SIZE", new Dictionary<string	, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 150f},{"2", 200f},{"3", 250f}
					}}
				}},
				{"DAMAGE_TICK", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 6f},{"2", 9f},{"3", 12f}
					}}
				}},
			}}
		}},
		{"INA_SPELLCASTER", new Dictionary<string, dynamic>() {
			{"title", "Spellcaster"},
			{"description", "Gain <color=#AFA>25/50/75</color> ability haste. Additionally, gain <color=#AFA>20/40/60%</color> more ability haste from all sources."},
			{"type", "Character Passive"},
			{"icon", "Ina_Spellcaster"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"ABILITYHASTE_FLAT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 25f},{"2", 50f},{"3", 75f}
					}}
				}},
				{"ABILITYHASTE_PERCENT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.2f},{"2", 0.4f},{"3", 0.6f}
					}}
				}},
			}}
		}},
		{"GENERIC_IRONSWORD", new Dictionary<string, dynamic>() {
			{"title", "Iron Sword"},
			{"description", "Increase attack by {ATTACK_PERCENT}."},
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
			{"description", "Increase Health by {HEALTH_FLAT} and Health Regen by {HEALTH_REGEN_FLAT}."},
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
					{"format", "0:0.#"},
					{"suffix", "HP/SEC"},
					{"level", new Dictionary<string, float> () {
						{"1", 1.5f},{"2", 3f},{"3", 4.5f},{"4", 6f},{"5", 7.5f}
					}}
				}},
			}}
		}},
		{"GENERIC_IRONARMOR", new Dictionary<string, dynamic>() {
			{"title", "Iron Armor"},
			{"description", "Increase Defense by {DEFENSE_FLAT}."},
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
			{"description", "Increase CRIT Rate by {CRIT_RATE} and CRIT Damage by {CRIT_DAMAGE}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_EternalFlame"},
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
						{"1", 0.2f},{"2", 0.4f},{"3", 0.6f},{"4", 0.8f},{"5", 1f}
					}}
				}},
			}}
		}},
		{"GENERIC_CIVILIZATIONFEATHER", new Dictionary<string, dynamic>() {
			{"title", "Civilization Feather"},
			{"description", "Increase Attack Speed by {ATTACKSPEED_PERCENT}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_CivilizationFeather"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"ATTACKSPEED_PERCENT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"format", "0:0%"},
					{"level", new Dictionary<string, float> () {
						{"1", 0.15f},{"2", 0.3f},{"3", 0.45f},{"4", 0.6f},{"5", 0.75f}
					}}
				}},
			}}
		}},
		{"GENERIC_TOPAZSTAFF", new Dictionary<string, dynamic>() {
			{"title", "Topaz Staff"},
			{"description", "Increase Ability Haste by {ABILITYHASTE_FLAT}."},
			{"type", "Common Equipment"},
			{"icon", "Generic_TopazStaff"},
			{"parameters", new Dictionary<string, dynamic> () {
				{"ABILITYHASTE_FLAT", new Dictionary<string, dynamic> () {
					{"color", "#AFA"},
					{"level", new Dictionary<string, float> () {
						{"1", 15f},{"2", 30f},{"3", 45f},{"4", 60f},{"5", 75f}
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

	public void GetCharacterUpgrades(string selectedCharacter){
		switch(selectedCharacter){
			case "MoriCalliope":
				List<string> characterUpgrades_Mori = new List<string>(){"MORICALLIOPE_TASTEOFDEATH", "MORICALLIOPE_ENDOFALIFE", "MORICALLIOPE_SOULHARVESTER"};
				upgradesAvailable.AddRange(characterUpgrades_Mori);
				characterName = selectedCharacter;
				characterUpgrades = characterUpgrades_Mori;
				break;
			case "NinomaeInanis":
				List<string> characterUpgrades_Ina = new List<string>(){"INA_BLESSINGSOFTHEGODS", "INA_DARKAURA", "INA_SPELLCASTER"};
				upgradesAvailable.AddRange(characterUpgrades_Ina);
				characterName = selectedCharacter;
				characterUpgrades = characterUpgrades_Ina;
				break;
			default:
				throw new ArgumentException("Character could not be found", selectedCharacter);
		}
		
	}

	private void GetGenericUpgrades() {
		List<string> genericUpgrades = new List<string>(){
			"GENERIC_IRONSWORD", "GENERIC_HEARTGEM", "GENERIC_IRONARMOR", "GENERIC_ETERNALFLAME", "GENERIC_CIVILIZATIONFEATHER", "GENERIC_TOPAZSTAFF"
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
		GameObject upgradeUI = this.upgradeUI.transform.Find("_UpgradeUI").gameObject;

		GameObject upgradeCard = upgradeUI.transform.Find("Upgrade Card " + slotIndex.ToString()).gameObject;
		GameObject upgradeNameUI = upgradeCard.transform.Find("Name").gameObject;
		upgradeNameUI.GetComponent<TMP_Text>().text = name;

		GameObject upgradeTitleUI = upgradeCard.transform.Find("Upgrade Title").gameObject;
		upgradeTitleUI.GetComponent<TMP_Text>().text = $"{upgrade["title"]} <color=#FFA>LV {GetCurrentUpgradeLevel(name)+1}</color>";

		GameObject upgradeDescriptionUI = upgradeCard.transform.Find("Upgrade Description").gameObject;
		upgradeDescriptionUI.GetComponent<TMP_Text>().text = ParseAbilityDescription(name, $"{upgrade["description"]}", upgrade["parameters"], 1);

		GameObject upgradeTypeUI = upgradeCard.transform.Find("Upgrade Type").gameObject;
		upgradeTypeUI.GetComponent<TMP_Text>().text = ParseAbilityType(upgrade["type"]);

		if (upgrade.ContainsKey("icon")){
			GameObject upgradeImageUI = upgradeCard.transform.Find("Upgrade Image").gameObject;
			upgradeImageUI.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrade["icon"]}");
		}
	}


	public string ParseAbilityType(string type) {
		switch (type) {
			case "Character Passive":
				return "<color=#FFA>" + type + "</color>";
			case "Common Equipment":
				return "<color=#AAA>" + type + "</color>";
			case "Uncommon Equipment":
				return "<color=#AFA>" + type + "</color>";
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

			if (characterUpgrades.Contains(name)) {
				if(upgradesActive[name]["level"] == MAXIMUM_PASSIVE_LEVEL){
					upgradesAvailable.Remove(name);
				};
			}
		} else {
			upgradesActive.Add(name, new Dictionary<string, dynamic> () {
				{"level", 1}
			});
		}
		if (upgrades[name]["type"] == "Common Equipment" || upgrades[name]["type"] == "Uncommon Equipment" || name == "INA_BLESSINGSOFTHEGODS" || name == "INA_DARKAURA" || name == "INA_SPELLCASTER"){
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
		float bonusAbilityHasteFlat = 0f;
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
			case "INA_BLESSINGSOFTHEGODS":
				float summonRate = upgrades["INA_BLESSINGSOFTHEGODS"]["parameters"]["SUMMON_RATE"]["level"][level];
				upgradeScripts.GetComponent<NinomaeInanis_BlessingsOfTheGods>().ApplyPassive(summonRate);
				break;
			case "INA_DARKAURA":
				float auraSize = upgrades["INA_DARKAURA"]["parameters"]["AURA_SIZE"]["level"][level];
				float damageTick = upgrades["INA_DARKAURA"]["parameters"]["DAMAGE_TICK"]["level"][level];
				upgradeScripts.GetComponent<NinomaeInanis_DarkAura>().ApplyPassive(auraSize, damageTick);
				break;
			case "INA_SPELLCASTER":
				bonusAbilityHasteFlat = upgrades["INA_SPELLCASTER"]["parameters"]["ABILITYHASTE_FLAT"]["level"][level];
				float bonusAbilityHastePercent = upgrades["INA_SPELLCASTER"]["parameters"]["ABILITYHASTE_PERCENT"]["level"][level];
				upgradeScripts.GetComponent<NinomaeInanis_Spellcaster>().ApplyPassive(bonusAbilityHasteFlat, bonusAbilityHastePercent);
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
			case "GENERIC_ETERNALFLAME":
				float bonusCritRate = upgrades["GENERIC_ETERNALFLAME"]["parameters"]["CRIT_RATE"]["level"][level];
				float bonusCritDamage = upgrades["GENERIC_ETERNALFLAME"]["parameters"]["CRIT_DAMAGE"]["level"][level];
				upgradeScripts.GetComponent<Generic_EternalFlame>().ApplyPassive(bonusCritRate, bonusCritDamage);
				break;
			case "GENERIC_CIVILIZATIONFEATHER":
				float bonusAttackSpeed = upgrades["GENERIC_CIVILIZATIONFEATHER"]["parameters"]["ATTACKSPEED_PERCENT"]["level"][level];
				upgradeScripts.GetComponent<Generic_CivilizationFeather>().ApplyPassive(bonusAttackSpeed);
				break;
			case "GENERIC_TOPAZSTAFF":
				bonusAbilityHasteFlat = upgrades["GENERIC_TOPAZSTAFF"]["parameters"]["ABILITYHASTE_FLAT"]["level"][level];
				upgradeScripts.GetComponent<Generic_TopazStaff>().ApplyPassive(bonusAbilityHasteFlat);
				break;
		}
		UpdatePlayerStats();
	}

	public void UpdatePlayerStats() {
		/*
			Recalculates player stats. Called when obtaining an upgrade which would change player stats.
		*/

		GameObject upgradeUI = this.upgradeUI.transform.Find("_UpgradeUI").gameObject;

		// Bonus Attack% Calculation
		float bonusAttackPercent = 0f;
		bonusAttackPercent += upgradeScripts.GetComponent<Generic_IronSword>().bonusAttackPercent;
		this.bonusAttackPercent = bonusAttackPercent;
		string attackText = $"{(int) 20 * (1+bonusAttackPercent)}";
		upgradeUI.transform.Find("Attack/Value").GetComponent<TMP_Text>().text = attackText;

		// Bonus Health Calculation
		float bonusHealthFlat = 0f;
		bonusHealthFlat += upgradeScripts.GetComponent<Generic_HeartGem>().bonusHealthFlat;
		this.bonusHealthFlat = bonusHealthFlat;
		player.GetComponent<PlayerHealthManager>().SetBonusHealth(bonusHealthFlat);
		string healthText = $"{(int) 500 + bonusHealthFlat}";
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
		string defenseText = $"{(int) 35 + bonusDefense}";
		upgradeUI.transform.Find("Defense/Value").GetComponent<TMP_Text>().text = defenseText;

		// Bonus Crit Rate Calculation
		float bonusCritRate = 0f;
		bonusCritRate += upgradeScripts.GetComponent<Generic_EternalFlame>().bonusCritRate;
		this.bonusCritRate = bonusCritRate;
		string critRateText = $"{String.Format("{0:0%}", 0.1f + bonusCritRate)}";
		upgradeUI.transform.Find("CRIT Rate/Value").GetComponent<TMP_Text>().text = critRateText;

		// Bonus Crit Damage Calculation
		float bonusCritDamage = 0f;
		bonusCritDamage += upgradeScripts.GetComponent<Generic_EternalFlame>().bonusCritDamage;
		this.bonusCritDamage = bonusCritDamage;
		string critDamageText = $"{String.Format("{0:0%}", 1.5f + bonusCritDamage)}";
		upgradeUI.transform.Find("CRIT Damage/Value").GetComponent<TMP_Text>().text = critDamageText;

		// Bonus Attack Speed Calculation
		float bonusAttackSpeed = 0f;
		bonusAttackSpeed += upgradeScripts.GetComponent<Generic_CivilizationFeather>().bonusAttackSpeed;
		this.bonusAttackSpeed = bonusAttackSpeed;
		player.GetComponent<PlayerProjectileManager>().SetBonusAttackSpeed(bonusAttackSpeed);
		string attackSpeedText = $"{String.Format("{0:0.00}", 1.5f * (1f + bonusAttackSpeed))} / SEC";
		upgradeUI.transform.Find("Attack Speed/Value").GetComponent<TMP_Text>().text = attackSpeedText;

		// Bonus Ability Haste Calculation
		float bonusAbilityHasteFlat = 0f;
		bonusAbilityHasteFlat += upgradeScripts.GetComponent<Generic_TopazStaff>().bonusAbilityHasteFlat;
		bonusAbilityHasteFlat += upgradeScripts.GetComponent<NinomaeInanis_Spellcaster>().bonusAbilityHasteFlat;
		this.bonusAbilityHasteFlat = bonusAbilityHasteFlat;

		float bonusAbilityHastePercent = 0f;
		bonusAbilityHastePercent += upgradeScripts.GetComponent<NinomaeInanis_Spellcaster>().bonusAbilityHastePercent;
		bonusAbilityHasteFlat *= (1 + bonusAbilityHastePercent);

		player.GetComponent<PlayerSkillManager>().SetBonusAbilityHaste(bonusAbilityHasteFlat);
		string abilityHasteText = $"{bonusAbilityHasteFlat} | {String.Format("{0:0.00%}", bonusAbilityHasteFlat / (bonusAbilityHasteFlat + 100))}";
		upgradeUI.transform.Find("Ability Haste/Value").GetComponent<TMP_Text>().text = abilityHasteText;
	}
}
