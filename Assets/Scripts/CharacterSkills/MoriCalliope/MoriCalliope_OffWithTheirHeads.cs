using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_OffWithTheirHeads : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1000f;
    [SerializeField] private float forwardSpeed = 50f;
    private GameObject player;

    private SfxManager sfx;
    [SerializeField] AudioClip launchSfx;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        sfx.PlayOneShot(launchSfx);
    }

    void Update()
    {
        SkillTwo();
        Forward();
    }

    private void SkillTwo()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void Forward()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        if (transform.position.z >= 70)
        {
            forwardSpeed *= -1;
        }
        if (transform.position.z <= player.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
