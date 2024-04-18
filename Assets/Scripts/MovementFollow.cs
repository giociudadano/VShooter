using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFollow : MonoBehaviour {
  // Follows towards the target object.
  
  [SerializeField] private float speed = 8f;
  [SerializeField] public GameObject followObject;

  void Start() {
        
  }

  void Update() {
    MoveObject();
  }

  void MoveObject() {
    transform.position = Vector3.MoveTowards(transform.position, followObject.transform.position, speed * Time.deltaTime);
  }
}
