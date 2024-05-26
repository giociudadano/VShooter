using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damage = 40f;

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().Hurt(damage);
        }
        if(collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossHealthManager>().Hurt(damage);
        }
        if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
