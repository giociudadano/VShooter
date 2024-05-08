using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInformationTabManager : MonoBehaviour, IPointerDownHandler {

    [SerializeField] GameObject tab;

    private bool isSelected = false;
    
    public void OnPointerDown(PointerEventData eventData) {
      if (!isSelected){
        SetTabSelected(true);
      }
    }

    public void SetTabSelected(bool newState) {
        if (newState) {
            isSelected = true;
            gameObject.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color((float) 73/255, 1, 1, (float) 138/255);
            tab.SetActive(true);
            transform.parent.gameObject.GetComponent<CharacterInformationManager>().DeselectAllTabs(gameObject.name);
            if (gameObject.name == "PassivesTabButton") {
              transform.parent.gameObject.GetComponent<CharacterInformationManager>().RenderPassives(transform.parent.GetComponent<CharacterSelectManager>().characterSelected);
            }
        } else {
            isSelected = false;
            gameObject.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = Color.black;
             tab.SetActive(false);
        }
    }

}
