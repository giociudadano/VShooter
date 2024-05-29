using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InaSkillOne : MonoBehaviour
{
    [SerializeField] AudioClip summonSfx;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float yOffset = 1.5f;
    [SerializeField] private float zOffset = 1.5f;

    private SfxManager sfx;
    private GameObject player;
    private GameObject[] summonedTakos;

    // Start is called before the first frame update
    void Start()
    {   
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        sfx.PlayOneShot(summonSfx);
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DestroyBook());
        BuffTakos();
    }

    //  Generic method to destroy the turret after a duration amount of time
    private IEnumerator DestroyBook()
    {   
        float uptime = 0f;
        while (uptime <= duration) {
            uptime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        };
        Destroy(gameObject);
    }

    private void BuffTakos()
    {
        summonedTakos = GameObject.FindGameObjectsWithTag("InaTakoTurret");
        foreach (GameObject summonedTako in summonedTakos) {
            summonedTako.GetComponent<TakoTurret>().Frenzy();
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 objectPosition = transform.position;
        objectPosition.x = player.transform.position.x;
        objectPosition.y = player.transform.position.y + yOffset;
        objectPosition.z = player.transform.position.z + zOffset;
        transform.position = objectPosition;

        Vector3 objectRotation = transform.rotation.eulerAngles;
        objectRotation.x = 90f;
        objectRotation.y = 180f;
        transform.rotation = Quaternion.Euler(objectRotation);
    }
}
