using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLinear : MonoBehaviour {
  // Manages objects moving in the x-z plane.

  [SerializeField] private float speed = 8f;
  [SerializeField] private float viewLimitDown = -5f;
  [SerializeField] private float viewLimitUp = 50f;

  void Start() {
      StartCoroutine(MoveObject());
  }

  private IEnumerator MoveObject() {
    while (true) {
      transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
      
      if (transform.position.z < viewLimitDown || transform.position.z > viewLimitUp) {
        Destroy(gameObject);
      }

      yield return null;
    }
  }
}
