using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KiaraSkillOne : MonoBehaviour
{
    [SerializeField] private float shieldHealth = 1f;
    [SerializeField] private float explodeSize = 200000f;
    [SerializeField] private float explodeDamage = 999999f;
    [SerializeField] private float explodeDamageBoss = 100f;
    [SerializeField] private float duration = 100f;
    [SerializeField] private float yOffset = 1.5f;
    [SerializeField] private float zOffset = 1.5f;
    private GameObject player;
    private float time;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SkillOneBegin();
    }

    // Update is called once per frame
    void Update()
    {
        ExplodeShield();
        FollowPlayer();
        SkillOneDurationManager();
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            shieldHealth -= other.gameObject.GetComponent<BasicProjectile>().projectileDamage;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            shieldHealth -= other.gameObject.GetComponent<EnemyManager>().collisionDamage;
        }
    }

    private void SkillOneBegin()
    {
        time = Time.time;
    }

    private void SkillOneDurationManager()
    {
        if (Time.time >= time + duration)
        {
            Destroy(gameObject);
        }
    }

    private void ExplodeShield()
    {
        if (shieldHealth <= 0f)
        {
            var objects = Physics.OverlapSphere(player.transform.position, explodeSize);
            foreach (var obj in objects)
            {
                if (obj.GetComponent<Collider>().CompareTag("Enemy"))
                {
                    obj.GetComponent<EnemyHealthManager>().Hurt(explodeDamage);
                };
                if (obj.GetComponent<Collider>().CompareTag("Boss"))
                {
                    obj.GetComponent<BossHealthManager>().Hurt(explodeDamageBoss);
                }
                if (obj.GetComponent<Collider>().CompareTag("EnemyProjectile"))
                {
                    Destroy(obj.gameObject);
                }
            };
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        Vector3 objectPosition = transform.position;
        objectPosition.x = player.transform.position.x;
        objectPosition.y = player.transform.position.y + yOffset;
        objectPosition.z = player.transform.position.z + zOffset;
        transform.position = objectPosition;

        Vector3 objectRotation = transform.rotation.eulerAngles;
        objectRotation.x = -90f;
        transform.rotation = Quaternion.Euler(objectRotation);

    }
}
