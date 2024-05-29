using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefreshUpgradesManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    
    [SerializeField] private GameObject card;

    [SerializeField] private GameObject text;

    [SerializeField] private GameObject gameManager;
    
    [SerializeField] private int refreshesLeft = 3;

    public void OnPointerEnter(PointerEventData eventData) {
        if (refreshesLeft > 0) {
           card.GetComponent<UnityEngine.UI.Outline>().enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (refreshesLeft > 0) {
            card.GetComponent<UnityEngine.UI.Outline>().enabled = false;
        } 
    }
    public void OnPointerDown(PointerEventData eventData) {
      if (refreshesLeft > 0) {
        gameManager.GetComponent<UpgradeManager>().DrawUpgrades();
        refreshesLeft--;
        if (refreshesLeft > 0) {
          text.GetComponent<TMP_Text>().text = $"Refresh Upgrades <color=#FFA>({refreshesLeft} Left)</color>";
        } else {
          text.GetComponent<TMP_Text>().text = $"<color=#AAA>Refresh Upgrades (0 Left)</color>";
          card.GetComponent<UnityEngine.UI.Outline>().enabled = false;
        }
        
      }
    }
}
