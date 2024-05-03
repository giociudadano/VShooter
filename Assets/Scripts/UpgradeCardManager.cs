using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject card;

    private string upgradeName;

    void Start() {
      
    }

    public void OnPointerEnter(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = false;
    }
    
    public void OnPointerDown(PointerEventData eventData) {
      upgradeName = this.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text;
      gameManager.GetComponent<UpgradeManager>().GetUpgrade(upgradeName);
      gameManager.GetComponent<GameManager>().ShowUpgradeUI(false);
    }
     
}
