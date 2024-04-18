using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
  
  public Transform target;

  void Start() {
        
  }
  
  void Update() {
    Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, target.position.z);
    transform.LookAt(targetPosition);
  }
}
