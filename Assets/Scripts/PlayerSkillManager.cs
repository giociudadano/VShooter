using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [Header ("Skill Set")]
    [SerializeField] private GameObject skillOneObject;
    [SerializeField] private GameObject skillTwoObject;
    private bool fire1Pressed = false;
    private bool fire2Pressed = false;
    private float skillOneCooldown = 7f;
    private float skillOneLastTime;
    private float skillTwoCooldown = 15f;
    private float skillTwoLastTime;
    private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

    [Header ("Objects")]

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !fire1Pressed)
        {  
            DoSkillOne();
            skillOneLastTime = Time.time;
        }
        if(Input.GetButtonDown("Fire2") && !fire2Pressed)
        {
            DoSkillTwo();
            skillTwoLastTime = Time.time;
        }
        if(Time.time > skillOneCooldown + skillOneLastTime)
        {
            fire1Pressed = false;
        }
        if(Time.time > skillTwoCooldown + skillTwoLastTime)
        {
            fire2Pressed = false;
        }
    }

    private void DoSkillOne()
    {
        fire1Pressed = true;
        Debug.Log("Skill One");
        Instantiate(skillOneObject,player.transform.position + shootOffset,player.transform.rotation);
    }

    private void DoSkillTwo()
    {
        fire2Pressed = true;
        Debug.Log("Skill Two");
        Instantiate(skillTwoObject,player.transform.position + shootOffset,player.transform.rotation);
    }
}
