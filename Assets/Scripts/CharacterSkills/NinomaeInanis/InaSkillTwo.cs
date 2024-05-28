using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaSkillTwo : MonoBehaviour
{
    [SerializeField] private GameObject tako;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(tako, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
}
