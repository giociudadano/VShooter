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
    
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
