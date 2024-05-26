using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayButtonManager : MonoBehaviour, IPointerDownHandler {
    [SerializeField] GameObject characterSelectManager; 
    public void OnPointerDown(PointerEventData eventData) {
      if (characterSelectManager.GetComponent<CharacterSelectManager>().characterSelected != null) {
        SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
      }
    }
}
