using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    [SerializeField] private GameObject header;
    [SerializeField] private GameObject characterSplash;

		private bool sceneStart;

    void Start() {
      header.gameObject.transform.localPosition = new Vector3(1200, 210, 0);
      characterSplash.gameObject.transform.localPosition = new Vector3(-700, -160, 0);
    }

    void Update() {
			header.gameObject.transform.localPosition = Vector3.Lerp(header.gameObject.transform.localPosition, new Vector3(72, 210, 0), 0.15f);
      characterSplash.gameObject.transform.localPosition = Vector3.Lerp(characterSplash.gameObject.transform.localPosition, new Vector3(-180, -160, 0), 0.15f);
    }
}
