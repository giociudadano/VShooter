using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriSkillOne : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 500f;
    private GameObject player;
    private float rotateDegrees = 350f;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        SkillOne();
        FollowPlayer();
    }

    private void SkillOne()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.rotation.eulerAngles.y)>= rotateDegrees)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        transform.position = player.transform.position;
    }
}
