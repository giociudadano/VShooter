using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
  public GameObject projectile;

  [SerializeField] private float fireRate = 0.35f;
  [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

  void Start(){
    InvokeRepeating("FireProjectile", 0f, fireRate);
  }

  void Update(){

  }

  void FireProjectile(){
    Instantiate(projectile, transform.position + shootOffset, transform.rotation);
  }
}
