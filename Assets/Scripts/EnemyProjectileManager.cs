using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
  //  Should probably fetch this dynamically when we have varied characters
  public GameObject projectile;

  [SerializeField] public float projectileDamage = 20f;
  [SerializeField] private float fireRate = 1f;
  [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

  [SerializeField] private Quaternion shootRotation = Quaternion.Euler(0, 180, 0);

  //  Ren's notes: Experimenting with bullet-based powerups here
  //[SerializeField] private int bulletCount = 3;

  private bool isFiring = false;

  void Start(){
    isFiring = true;
    //InvokeRepeating("FireProjectile", 0f, fireRate);
    StartCoroutine(FireProjectile());
    projectile.transform.rotation = shootRotation;
  }

  void Update(){

  }

  //  TODO: Calibrate fireRate and enemy speed so it feels fair
  private IEnumerator FireProjectile(){
    while (isFiring) {
        Instantiate(projectile, transform.position + shootOffset, transform.rotation);

      yield return new WaitForSeconds(fireRate);
    }
  }
}
