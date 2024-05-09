using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementLinear : MonoBehaviour {
  // Manages objects moving up and down the screen.

  [SerializeField] private float speed = 8f;
  [SerializeField] private float viewLimitDown = -5f;
  [SerializeField] private float viewLimitUp = 50f;

  void Start() {
      StartCoroutine(MoveObject());
  }

  void Update() {
    
  }

  //  We don't use Vecto3.Lerp() here since we don't need fancy swerving/acceration/retargetting
  private IEnumerator MoveObject() {
    while(true) {
      transform.Translate(Vector3.forward * Time.deltaTime * speed);
      if (transform.position.z < viewLimitDown || transform.position.z > viewLimitUp) {
        Destroy(gameObject);
      }
      yield return new WaitForEndOfFrame();
    }
  }

  // void DeleteObject() {
  //   if (transform.position.z < viewLimitDown || transform.position.z > viewLimitUp){
  //       Destroy(gameObject);
  //   }
  // }
}
