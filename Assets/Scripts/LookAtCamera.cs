using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
  
  public GameObject mainCamera;

  void Start() {
    mainCamera = GameObject.Find("Main Camera");
  }
  
  void Update() {
    Vector3 targetPosition = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    transform.LookAt(targetPosition);
  }
}
