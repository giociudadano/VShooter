using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  // Manages player input and movement.

  [SerializeField] private float speed = 12f;
  private Dictionary<string, float> bounds = new Dictionary<string, float> () {
    {"minimumX", -8.5f}, {"maximumX", 8.5f},
    {"minimumZ", -1f},     {"maximumZ", 2.5f}
  };

  void Start() {
        
  }

  void Update() {
    MovePlayer();
    CheckBounds();
  }

  void MovePlayer() {
      // Checks for player input and pushes the player in the player input direction.
      Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      transform.Translate(movementVector * speed * Time.deltaTime);
    }

    void CheckBounds() {
      // Checks if the player is out of bounds and pushes the player towards the bounds box.
      if (transform.position.x < bounds["minimumX"] || transform.position.z < bounds["minimumZ"]){
        transform.position = new Vector3(Mathf.Max(bounds["minimumX"], transform.position.x), transform.position.y, Mathf.Max(transform.position.z, bounds["minimumZ"]));
      }
      if (transform.position.x > bounds["maximumX"] || transform.position.z > bounds["maximumZ"]){
        transform.position = new Vector3(Mathf.Min(bounds["maximumX"], transform.position.x), transform.position.y, Mathf.Min(transform.position.z, bounds["maximumZ"]));
      }
    }
}
