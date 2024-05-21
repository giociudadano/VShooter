using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriSkillOne : MonoBehaviour
{
    [SerializeField] private float areaSize = 15.0f;
    [SerializeField] private float healAmount = 10.0f;
    [SerializeField] private float maxHealing = 60.0f;
    private float counter = 0;
    [SerializeField] private float rotateSpeed = 500f;
    private GameObject player;
    private float rotateDegrees = 350f;

    private SfxManager sfx;
    [SerializeField] AudioClip spinSfx;

    // Start is called before the first frame update

    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        sfx.PlayOneShot(spinSfx);
        AOEHealing(healAmount);
    }

    void Update()
    {
        SkillOne();
        FollowPlayer();
    }

    private void SkillOne()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.rotation.eulerAngles.y) >= rotateDegrees)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        transform.position = player.transform.position;
    }

    private void AOEHealing(float healAmount)
    {
        var colliders = Physics.OverlapSphere(player.transform.position, areaSize);
        foreach (var col in colliders)
        {
            if (col.GetComponent<Collider>().CompareTag("Enemy") || col.GetComponent<Collider>().CompareTag("Boss"))
            {
                counter++;
            };
        };
        healAmount *= counter;
        healAmount = Math.Min(healAmount, maxHealing);
        if (healAmount > 0)
        {
            player.GetComponent<PlayerHealthManager>().Heal(healAmount);
        }
    }
}
