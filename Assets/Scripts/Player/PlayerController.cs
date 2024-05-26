using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] public float speed = 12f;
  private Dictionary<string, float> bounds = new Dictionary<string, float> () {
    {"minimumX", -7f}, {"maximumX", 7f},
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
      Vector3 movementVector = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
      transform.Translate(movementVector * speed * Time.deltaTime, Space.World);
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
