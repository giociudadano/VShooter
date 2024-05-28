using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuGenericButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("SFX")]
     private SfxManager sfxManager;
     [SerializeField] private AudioClip pointerEnterSfx;
     [SerializeField] private AudioClip pointerSelectSfx;
     [SerializeField] private AudioClip pointerExitSfx;

    private bool isButtonSoundsOn = true;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        sfxManager = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   
        if (isButtonSoundsOn) {
            sfxManager.PlayOneShot(pointerEnterSfx);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonSoundsOn) {
            sfxManager.PlayOneShot(pointerExitSfx);
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        sfxManager.PlayOneShot(pointerSelectSfx);
        //  Prevent further beeps upon pressing the button
        if (!isButtonSoundsOn) {
            isButtonSoundsOn = false;
        }
    }
}
