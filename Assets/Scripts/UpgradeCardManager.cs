using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject card;

    private float lastTimeClicked;

    public void OnPointerEnter(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = false;
    }
    
    public void OnPointerDown(PointerEventData eventData) {
      GameObject name = this.transform.Find("Name").gameObject;
      gameManager.GetComponent<UpgradeManager>().GetUpgrade(name.GetComponent<TMP_Text>().text);
      gameManager.GetComponent<GameManager>().HideUpgradeUI();
    }
     
}
