using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeSkillOne : MonoBehaviour
{
    [SerializeField] private float shieldHealth = 1f;
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float duration = 4f;
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
        FollowPlayer();
        SkillOneCheck();
    }

    private void SkillOneBegin()
    {
        time = Time.time;
        player.GetComponent<PlayerController>().speed += movementSpeed;
    }

    private void SkillOneCheck()
    {
        if (Time.time >= time + duration)
        {
            player.GetComponent<PlayerController>().speed -= movementSpeed;

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
        objectRotation.y = 180f;
        transform.rotation = Quaternion.Euler(objectRotation);
   
    }
}
