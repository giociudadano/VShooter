using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject card;

    public void OnPointerEnter(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        card.GetComponent<UnityEngine.UI.Outline>().enabled = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount == 2) {
            gameManager.GetComponent<GameManager>().HideUpgradeUI();
        }
    }
     
}
