using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextManager : MonoBehaviour {

	[SerializeField] private float xOffset = 0.75f;

  [SerializeField] private float yOffset = 0.75f;
	
	[SerializeField] private float textDuration = 1.25f;

	float newPositionX, newPositionY;

  void Start() {
    newPositionX = transform.position.x + Random.Range(-xOffset, xOffset);
    newPositionY = transform.position.y + yOffset;
  }
  void Update() {
		transform.position = Vector3.Lerp(transform.position, new Vector3(newPositionX, newPositionY, transform.position.z), 0.5f);
    Destroy(gameObject, textDuration);
  }
}
