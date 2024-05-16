using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    private SfxManager sfx;

    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().Hurt(damage);
            sfx.PlayImpactSfx();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            Destroy(collision.gameObject);
            sfx.PlayImpactSfx();
            Destroy(gameObject);
        }
    }
}
