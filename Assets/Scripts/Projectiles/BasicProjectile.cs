using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour {

    [SerializeField] public float projectileDamage = 20f;

    void Start() {
        
    }
    void Update() {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            Destroy(gameObject);
        }
    }
}
