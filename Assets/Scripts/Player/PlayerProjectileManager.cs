using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
  //  Should probably fetch this dynamically when we have varied characters
  public GameObject projectile;
  [SerializeField] public float projectileDamage = 20f;
  [SerializeField] private float attackSpeed = 1.5f;
  [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);
  [SerializeField] private float shootingStartDelay = 1.5f;

  private SfxManager sfx;
  private bool isFiring = false;

  void Start(){
    isFiring = true;
    sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    //InvokeRepeating("FireProjectile", 0f, fireRate);
    StartCoroutine(FireProjectile());
  
  }

  private IEnumerator FireProjectile(){
    yield return new WaitForSeconds(shootingStartDelay);
    while (isFiring) {
        Instantiate(projectile, transform.position + shootOffset, transform.rotation);
        sfx.PlayShootingSfx();
        yield return new WaitForSeconds(1f/attackSpeed);
    }
  }

  public void SetBonusAttackSpeed(float bonusAttackSpeed){
    attackSpeed = 1.5f * (1f + bonusAttackSpeed);
  }
}
