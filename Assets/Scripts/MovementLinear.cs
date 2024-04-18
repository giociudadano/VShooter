using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLinear : MonoBehaviour {
  // Manages objects moving up and down the screen.

  [SerializeField] private float speed = 8f;
  [SerializeField] private float viewLimitDown = -5f;

  [SerializeField] private float viewLimitUp = 50f;

  void Start() {
      
  }

  void Update() {
    MoveObject();
    DeleteObject();
  }

  void MoveObject() {
    transform.Translate(Vector3.forward * Time.deltaTime * speed);
  }

  void DeleteObject() {
    if (transform.position.z < viewLimitDown || transform.position.z > viewLimitUp){
        Destroy(gameObject);
    }
  }
}
