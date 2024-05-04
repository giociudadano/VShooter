using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    private string selectedCharacter;

    [Header("GUI")]
    
    [SerializeField] private GameObject characterNameGUI;

    [SerializeField] private GameObject characterImageGUI;

    [Header("GameObjects")]

    [SerializeField] private GameObject playerObject;


    void Start()
    {
        CharacterData characterData = FindObjectOfType<CharacterData>();
        selectedCharacter = characterData.selectedCharacter;
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
                
                break;
            case "NinomaeInanis":
                characterNameGUI.GetComponent<TMP_Text>().text = "NINOMAE INA'NIS";
                break;
        }
    }
}
