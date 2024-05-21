using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaSkillOne : MonoBehaviour
{
    private GameObject player;

    private float time;

    [SerializeField] AudioClip spinSfx;
    [SerializeField] private float duration = 3f;

    [SerializeField] private float bonusHealth = 200f;
    [SerializeField] private float bonusDefense = 30f;
    [SerializeField] private float bonusDamage = 15f;
    [SerializeField] private float bonusDamageSpeed = 5f;
    [SerializeField] private float healthRegen = 20f;


    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SkillOneBegin();
    }

    void Update()
    {
        SkillOneCheck();
        FollowPlayer();
    }

    private void SkillOneBegin()
    {
        Debug.Log("Ara man");
        time = Time.time;
        player.GetComponent<PlayerHealthManager>().SetBonusHealth(bonusHealth);
        player.GetComponent<PlayerHealthManager>().SetBonusDefense(bonusDefense);
        player.GetComponent<PlayerHealthManager>().healthRegen = healthRegen;
        player.GetComponent<PlayerProjectileManager>().projectileDamage += bonusDamage;
        player.GetComponent<PlayerProjectileManager>().SetBonusAttackSpeed(bonusDamageSpeed);
    }

    private void SkillOneCheck()
    {
        if (Time.time >= time + duration)
        {
            player.GetComponent<PlayerHealthManager>().SetBonusHealth(0);
            player.GetComponent<PlayerHealthManager>().SetBonusDefense(0);
            player.GetComponent<PlayerHealthManager>().healthRegen = 0f;
            player.GetComponent<PlayerProjectileManager>().projectileDamage -= bonusDamage;
            player.GetComponent<PlayerProjectileManager>().SetBonusAttackSpeed(0);

            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        Vector3 objectPosition = transform.position;
        objectPosition.x = player.transform.position.x;
        objectPosition.y = player.transform.position.y + 2f;
        objectPosition.z = player.transform.position.z + 2f;
        transform.position = objectPosition;

        Vector3 objectRotation = transform.rotation.eulerAngles;
        objectRotation.x = 90f;
        objectRotation.y = 180f;
        transform.rotation = Quaternion.Euler(objectRotation);
   
    }
}
