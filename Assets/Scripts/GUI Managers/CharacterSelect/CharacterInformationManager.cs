using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInformationManager : MonoBehaviour
{
		[SerializeField] GameObject characterSelectManager;
    [SerializeField] GameObject[] tabButtons;

		[Header("Tabs")]
		[SerializeField] GameObject passivesTab;
		[SerializeField] GameObject activesTab;
    
    void Start() {
      // Renders the passive tooltips on startup.
      foreach (GameObject tabButton in tabButtons) {
        if (tabButton.name == "PassivesTabButton") {
          tabButton.GetComponent<CharacterInformationTabManager>().SetTabSelected(true);
        }
      }
    }

    public void DeselectAllTabs(string tabName) {
        foreach (GameObject tabButton in tabButtons) {
            if (tabButton.name != tabName) {
                tabButton.GetComponent<CharacterInformationTabManager>().SetTabSelected(false);
            }
        }
    }

		public void RenderPassives(string characterName) {
			Dictionary<string, dynamic> characterData = characterSelectManager.GetComponent<CharacterData>().GetPassiveInfo(characterName);
			for (int i = 1; i <= 3; i++) {
				GameObject passivesTabRow = passivesTab.transform.Find($"Passive {i}").gameObject;
				passivesTabRow.transform.Find("Title").GetComponent<TMP_Text>().text = characterData[$"passive_{i}"]["title"];
				passivesTabRow.transform.Find("Description").GetComponent<TMP_Text>().text = characterData[$"passive_{i}"]["description"];
				passivesTabRow.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{characterData[$"passive_{i}"]["icon"]}");
			}
		}

    public void RenderActives(string characterName) {
			Dictionary<string, dynamic> characterData = characterSelectManager.GetComponent<CharacterData>().GetActiveInfo(characterName);
			for (int i = 1; i <= 2; i++) {
				GameObject activesTabRow = activesTab.transform.Find($"Active {i}").gameObject;
				activesTabRow.transform.Find("Title").GetComponent<TMP_Text>().text = characterData[$"active_{i}"]["title"];
				activesTabRow.transform.Find("Description").GetComponent<TMP_Text>().text = characterData[$"active_{i}"]["description"];
        string cooldown = $"{characterData[$"active_{i}"]["cooldown"]}";
        activesTabRow.transform.Find("Cooldown").GetComponent<TMP_Text>().text = $"{cooldown}s Cooldown";
				activesTabRow.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{characterData[$"active_{i}"]["icon"]}");
			}
		}
}
