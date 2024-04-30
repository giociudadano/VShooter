using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour {

	[SerializeField] private float xOffset = 0.75f;
	
	[SerializeField] private float textDuration = 1.25f;

	float newPositionX;

  void Start() {
    newPositionX = transform.position.x + Random.Range(-xOffset, xOffset);
  }
  void Update() {
		transform.position = Vector3.Lerp(transform.position, new Vector3(newPositionX, 2.75f, transform.position.z), 0.5f);
    Destroy(gameObject, textDuration);
  }
}
