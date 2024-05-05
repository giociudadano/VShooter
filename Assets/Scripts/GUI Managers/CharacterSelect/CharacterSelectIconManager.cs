using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CharacterSelectIconManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [Header("SELECTION")]
		[SerializeField] private GameObject characterSelectManager;
    [SerializeField] private GameObject selectIcon;
    private bool isSelected = false;

    [Header("SFX")]
     private SfxManager sfxManager;
     [SerializeField] private AudioClip pointerEnterSfx;
     [SerializeField] private AudioClip pointerSelectSfx;

     [SerializeField] private AudioClip pointerExitSfx;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sfxManager = GameObject.Find("SfxPlayer").GetComponent<SfxManager>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (!isSelected){
        sfxManager.PlayOneShot(pointerEnterSfx);
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color((float) 73/255, 1, 1, (float) 138/255);
				characterSelectManager.GetComponent<CharacterSelectManager>().OnIconHoverIn(selectIcon.name);
      }
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (!isSelected){
        sfxManager.PlayOneShot(pointerExitSfx);
        selectIcon.transform.Find("Background").GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(1, 1, 1, (float) 138/255);
				characterSelectManager.GetComponent<CharacterSelectManager>().OnIconHoverOut();
      }
    }
    
    public void OnPointerDown(PointerEventData eventData) {
      if (!isSelected){
        sfxManager.PlayOneShot(pointerSelectSfx);
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
