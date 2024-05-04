using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectIconManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

		[SerializeField] private GameObject characterSelectManager;
    [SerializeField] private GameObject selectIcon;

    private bool isSelected = false;

    public void OnPointerEnter(PointerEventData eventData) {
      if (!isSelected){
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color((float) 73/255, 1, 1, (float) 138/255);
				characterSelectManager.GetComponent<CharacterSelectManager>().OnIconHoverIn(selectIcon.name);
      }
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (!isSelected){    
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(1, 1, 1, (float) 138/255);
				characterSelectManager.GetComponent<CharacterSelectManager>().OnIconHoverOut();
      }
    }
    
    public void OnPointerDown(PointerEventData eventData) {
      if (!isSelected){
        isSelected = true;
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color((float) 73/255, 1, (float) 73/255, (float) 138/255);
        characterSelectManager.GetComponent<CharacterSelectManager>().SelectCharacter(selectIcon.name);
      } else {
        isSelected = false;
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color((float) 73/255, 1, 1, (float) 138/255);
        characterSelectManager.GetComponent<CharacterSelectManager>().SelectCharacter(null);
      }
    }
}
