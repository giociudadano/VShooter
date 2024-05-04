using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
  //  Should probably fetch this dynamically when we have varied characters
  public GameObject projectile;
  [SerializeField] public float projectileDamage = 20f;
  [SerializeField] private float fireRate = 0.15f;
  [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

  [SerializeField] private float shootingStartDelay = 1.5f;

  //  Ren's notes: Experimenting with bullet-based powerups here
  //[SerializeField] private int bulletCount = 3;

  private SfxManager sfx;

  private bool isFiring = false;

  void Start(){
    isFiring = true;
    sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    //InvokeRepeating("FireProjectile", 0f, fireRate);
    StartCoroutine(FireProjectile());
  
  }

  void Update(){

  }

  //  TODO: Calibrate fireRate and enemy speed so it feels fair
  private IEnumerator FireProjectile(){
    yield return new WaitForSeconds(shootingStartDelay);
    while (isFiring) {
        Instantiate(projectile, transform.position + shootOffset, transform.rotation);
        sfx.PlayShootingSfx();
        yield return new WaitForSeconds(fireRate);
    }
  }
}
