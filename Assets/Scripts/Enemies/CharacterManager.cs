using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

		public string selectedCharacter;

		[Header("GUI")]
		[SerializeField] private GameObject characterNameGUI;
		[SerializeField] private GameObject characterImageGUI;
		[SerializeField] private GameObject skillQ;
		[SerializeField] private GameObject skillE;

		[Header("GameObjects")]
		[SerializeField] private GameObject playerObject;
		[SerializeField] private CharacterData characterData = null;

		void Start()
		{
				characterData = FindObjectOfType<CharacterData>();
				if (characterData != null){
						selectedCharacter = characterData.selectedCharacter;
				} else {
						selectedCharacter = "MoriCalliope";
				}
				InitGUI();
				GetUpgrades();
		}
		void Update()
		{

		}

		private void InitGUI() {
				// Loads player icon and player model
				Sprite playerIcon = Resources.Load<Sprite>($"PlayerIcons/PI_{selectedCharacter}");
				GameObject playerModel = Resources.Load($"Characters/{selectedCharacter}") as GameObject;
				playerModel.name = selectedCharacter;
				Instantiate(playerModel, playerObject.transform);
				characterImageGUI.GetComponent<UnityEngine.UI.Image>().sprite = playerIcon;

				// Loads player skills
				Dictionary<string, dynamic> activeInfo = characterData.GetComponent<CharacterData>().GetActiveInfo(selectedCharacter);
				skillQ.transform.Find("Skill Icon Mask/Skill Icon").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{activeInfo["active_2"]["icon"]}");
				skillQ.transform.Find("Skill Tooltip/Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{activeInfo["active_2"]["icon"]}");
				skillQ.transform.Find("Skill Tooltip/Title").GetComponent<TMP_Text>().text = activeInfo["active_2"]["title"];
				skillQ.transform.Find("Skill Tooltip/Description").GetComponent<TMP_Text>().text = activeInfo["active_2"]["description"];
				skillQ.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}", activeInfo["active_2"]["cooldown"])}s Cooldown";
				
				skillE.transform.Find("Skill Icon Mask/Skill Icon").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{activeInfo["active_1"]["icon"]}");
				skillE.transform.Find("Skill Tooltip/Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{activeInfo["active_1"]["icon"]}");
				skillE.transform.Find("Skill Tooltip/Title").GetComponent<TMP_Text>().text = activeInfo["active_1"]["title"];
				skillE.transform.Find("Skill Tooltip/Description").GetComponent<TMP_Text>().text = activeInfo["active_1"]["description"];
				skillE.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}", activeInfo["active_1"]["cooldown"])}s Cooldown";
		}

		public void GetUpgrades(){
			// Loads player upgrades
			switch (selectedCharacter) {
					case "MoriCalliope":
							characterNameGUI.GetComponent<TMP_Text>().text = "MORI CALLIOPE";
							// List<string> characterUpgrades = new List<string>(){"MORICALLIOPE_TASTEOFDEATH", "MORICALLIOPE_ENDOFALIFE", "MORICALLIOPE_SOULHARVESTER"};
							gameObject.GetComponent<UpgradeManager>().GetCharacterUpgrades("MoriCalliope");
							break;
					case "NinomaeInanis":
							characterNameGUI.GetComponent<TMP_Text>().text = "NINOMAE INA'NIS";
							// List<string> characterUpgradesIna = new List<string>(){"INA_DARKAURA", "INA_VIOLETBLOOM", "INA_THEANCIENTONE"};
							gameObject.GetComponent<UpgradeManager>().GetCharacterUpgrades("NinomaeInanis");
							break;
			}
		}
}
