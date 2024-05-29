using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealerManager : MonoBehaviour
{
    [SerializeField] private float healAmount = 5f;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossHealthManager>().Heal(healAmount);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealthManager>().Heal(healAmount);
        }
    }
}
