using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
  
  public GameObject camera;

  void Start() {
    camera = GameObject.Find("Main Camera");
  }
  
  void Update() {
    Vector3 targetPosition = new Vector3(transform.position.x, camera.transform.position.y, camera.transform.position.z);
    transform.LookAt(targetPosition);
  }
}
