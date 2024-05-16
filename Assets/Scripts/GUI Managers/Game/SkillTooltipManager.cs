using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField] private GameObject skillTooltipGUI;

    public void OnPointerEnter(PointerEventData eventData) {
        skillTooltipGUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        skillTooltipGUI.SetActive(false);
    }
}
