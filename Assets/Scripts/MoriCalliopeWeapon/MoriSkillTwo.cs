using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriSkillTwo : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1000f;
    [SerializeField] private float forwardSpeed = 50f;
    private GameObject player;
    
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        if(transform.position.z >= 100)
        {
            Destroy(gameObject);
        }
    }
}
