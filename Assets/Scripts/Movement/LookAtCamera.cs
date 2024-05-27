using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
  
  public GameObject mainCamera;

  //  This code is to be used across several stages so we have to query it dynamically
  //  Ren's notes: I think it's better to use Tags instead of Names, since Tags are more performant
  void Start() {
    //mainCamera = GameObject.Find("Main Camera");
    mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
  }
  
  void Update() {
    Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    transform.LookAt(targetPosition);
  }
}
