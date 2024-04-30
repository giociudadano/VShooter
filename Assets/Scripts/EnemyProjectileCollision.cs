using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().Hurt(damage);
        }
        if (collision.gameObject.CompareTag("AllyProjectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
