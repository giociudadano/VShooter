using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject characterSplash;

    [SerializeField] private GameObject characterNameObject;
    [SerializeField] private GameObject characterDescriptionObject;

    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject characterData;

    public bool isCharacterSelected = false;

    void Start() {
      header.gameObject.transform.localPosition = new Vector3(1800, 270, 0);
      characterSplash.gameObject.transform.localPosition = new Vector3(-1000, -220, 0);
      characterNameObject.GetComponent<TMP_Text>().text = "";
      characterDescriptionObject.GetComponent<TMP_Text>().text = "";
      StartCoroutine(SceneStart());
    }

    void Update() {
			
    }

		private IEnumerator SceneStart() {
      StartCoroutine(MoveObject(header.gameObject, header.gameObject.transform.localPosition, new Vector3(50, 270, 0), 0.4f));
      StartCoroutine(MoveObject(characterSplash.gameObject, characterSplash.gameObject.transform.localPosition, new Vector3(-290, -220, 0), 0.4f));
      yield return new WaitForSeconds(0.4f);
      StartCoroutine(SceneIdle());
		}

    private IEnumerator SceneIdle() {
      StartCoroutine(HoverObject(characterSplash.gameObject, characterSplash.gameObject.transform.localPosition, new Vector3(-290, -260, 0), new Vector3(-290, -180, 0), 3f));
      yield return null;
    }

    private IEnumerator MoveObject(GameObject gameObject, Vector3 source, Vector3 target, float transitionTime) {
      float timeStart = Time.time;
      while (Time.time < timeStart + transitionTime) {
        gameObject.transform.localPosition = Vector3.Lerp(source, target, (Time.time - timeStart)/ transitionTime);
        yield return null;
      }
      gameObject.transform.localPosition = target;
    }

    private IEnumerator HoverObject(GameObject gameObject, Vector3 median, Vector3 min, Vector3 max, float transitionTime) {
      float timeStart = Time.time;
      while (true) {
        while (Time.time < timeStart + (transitionTime/2)) {
          gameObject.transform.localPosition = Vector3.Lerp(median, max, (Time.time - timeStart)/ (transitionTime/2));
          yield return null;
        }
        gameObject.transform.localPosition = max;
        timeStart = Time.time;
        while (Time.time < timeStart + transitionTime) {
          gameObject.transform.localPosition = Vector3.Lerp(max, min, (Time.time - timeStart) / transitionTime);
          yield return null;
        }
        gameObject.transform.localPosition = min;
        timeStart = Time.time;
        while (Time.time < timeStart + (transitionTime/2)) {
          gameObject.transform.localPosition = Vector3.Lerp(min, median, (Time.time - timeStart)/ (transitionTime/2));
          yield return null;
        }
        gameObject.transform.localPosition = median;
        timeStart = Time.time;
      }
    }

    public void OnIconHoverIn(string characterName) {
      Sprite splashArt = Resources.Load<Sprite>($"SplashArts/{characterName}");
      characterSplash.transform.Find("Background").GetComponent<UnityEngine.UI.Image>().sprite = splashArt;
      switch (characterName) {
        case "MoriCalliope":
          characterSplash.transform.Find("Background").GetComponent<UnityEngine.UI.Image>().color = new Color((float)195/255, (float)67/255, (float) 91/255, 1);
          characterNameObject.GetComponent<TMP_Text>().text = "MORI CALLIOPE";
          characterDescriptionObject.GetComponent<TMP_Text>().text = GetCharacterDescription(characterName);
          break;
        case "NinomaeInanis":
          characterSplash.transform.Find("Background").GetComponent<UnityEngine.UI.Image>().color = new Color((float)126/255, (float)66/255, (float) 195/255, 1);
          characterNameObject.GetComponent<TMP_Text>().text = "NINOMAE INA'NIS";
          characterDescriptionObject.GetComponent<TMP_Text>().text = GetCharacterDescription(characterName);
          break;
      }
      characterSplash.transform.Find("Foreground").gameObject.SetActive(true);
      characterSplash.transform.Find("Foreground").GetComponent<UnityEngine.UI.Image>().sprite = splashArt;
      StopAllCoroutines();
      StartCoroutine(HoverCharacter());
      gameObject.GetComponent<CharacterData>().selectedCharacter = characterName;
    }

    private string GetCharacterDescription(string characterName){
      switch (characterName) {
        case "MoriCalliope":
          return "Mori Calliope is the Grim Reaper's first apprentice. As death's assistant, she specializes in using <color=#FFA>lifesteal</color> and <color=#FFA>post-death effects</color> to gain an advantage in the battlefield.";
        case "NinomaeInanis":
          return "Ninomae Ina'nis is a priestess and worshipper to an ancient tentacle god. As a summoner, she specializes in <color=#FFA>summoning allies</color> and <color=#FFA>controlling enemies</color>.";
        default:
          return "";
      }
    }

    private IEnumerator HoverCharacter() {
      characterSplash.gameObject.transform.localPosition = new Vector3(-1000, -220, 0);
      StartCoroutine(MoveObject(characterSplash.gameObject, characterSplash.gameObject.transform.localPosition, new Vector3(-290, -220, 0), 0.4f));
      yield return new WaitForSeconds(0.4f);
      StartCoroutine(HoverObject(characterSplash.gameObject, characterSplash.gameObject.transform.localPosition, new Vector3(-290, -260, 0), new Vector3(-290, -180, 0), 3f));
      yield return null;
    }

    public void OnIconHoverOut() {
      Sprite splashArt = Resources.Load<Sprite>($"SplashArts/HoshimachiSuisei");
      characterNameObject.GetComponent<TMP_Text>().text = "";
      characterDescriptionObject.GetComponent<TMP_Text>().text = "";
      characterSplash.transform.Find("Background").GetComponent<UnityEngine.UI.Image>().sprite = splashArt;
      characterSplash.transform.Find("Background").GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 1);
      characterSplash.transform.Find("Foreground").gameObject.SetActive(false);
      StopAllCoroutines();
      StartCoroutine(HoverCharacter());
    }

    public void SelectCharacter(String characterName) {
      if (characterName != null) {
        isCharacterSelected = true;
        playButton.transform.Find("Background").GetComponent<Outline>().effectColor = new Color((float)71/255, (float)194/255, 255, 0.5f);
        playButton.transform.Find("Text").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f);
      } else {
        isCharacterSelected = false;
        playButton.transform.Find("Background").GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 0.5f);
        playButton.transform.Find("Text").GetComponent<TMP_Text>().color = new Color((float)149/255, (float)149/255, (float)149/255, 0.5f);
      }
    }
}
