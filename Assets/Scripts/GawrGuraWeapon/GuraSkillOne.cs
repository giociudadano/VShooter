using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraSkillOne : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = -500f;
    private GameObject player;
    private float rotateDegreesLimitMin = 150f;

    private float rotateDegreesLimitMax = 200f;

    private SfxManager sfx;
    [SerializeField] AudioClip spinSfx;

    void Start()
    {
        //sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        //sfx.PlayOneShot(spinSfx);
    }

    void Update()
    {
        SkillOne();
        FollowPlayer();
    }

    private void SkillOne()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        Debug.Log(Mathf.Abs(transform.rotation.eulerAngles.y));
        if (Mathf.Abs(transform.rotation.eulerAngles.y) >= rotateDegreesLimitMin
            && Mathf.Abs(transform.rotation.eulerAngles.y) <= rotateDegreesLimitMax)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        transform.position = player.transform.position;
    }
}
