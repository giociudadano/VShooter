using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    private string selectedCharacter;

    [Header("GUI")]
    
    [SerializeField] private GameObject characterNameGUI;

    [SerializeField] private GameObject characterImageGUI;

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
