using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public string selectedCharacter;

    [Header("GUI")]
    
    [SerializeField] private GameObject characterNameGUI;

    [SerializeField] private GameObject characterImageGUI;

    [Header("GameObjects")]

    [SerializeField] private GameObject playerObject;

    void Start()
    {
        CharacterData characterData = FindObjectOfType<CharacterData>();
        if (characterData != null){
            selectedCharacter = characterData.selectedCharacter;
        } else {
            selectedCharacter = "MoriCalliope";
        }
        InitGUI();
    }
    void Update()
    {
        
    }

    private void InitGUI() {
        Sprite playerIcon = Resources.Load<Sprite>($"PlayerIcons/PI_{selectedCharacter}");
        GameObject playerModel = Resources.Load($"Characters/{selectedCharacter}") as GameObject;
        playerModel.name = selectedCharacter;
        Instantiate(playerModel, playerObject.transform);
        characterImageGUI.GetComponent<UnityEngine.UI.Image>().sprite = playerIcon;  
        switch (selectedCharacter) {
            case "MoriCalliope":
                characterNameGUI.GetComponent<TMP_Text>().text = "MORI CALLIOPE";
                List<string> characterUpgrades = new List<string>(){"MORICALLIOPE_TASTEOFDEATH", "MORICALLIOPE_ENDOFALIFE", "MORICALLIOPE_SOULHARVESTER"};
                gameObject.GetComponent<UpgradeManager>().GetCharacterUpgrades(characterUpgrades);
                break;
            case "NinomaeInanis":
                characterNameGUI.GetComponent<TMP_Text>().text = "NINOMAE INA'NIS";
                break;
        }
    }
}
