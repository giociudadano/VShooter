using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public GameObject skillOne;
    public GameObject skillTwo;
    private bool fire1On = true;
    private bool fire2On = true;
    private float skillOneCooldown = 7f;
    private float skillOneLastTime;
    private float skillTwoCooldown = 15f;
    private float skillTwoLastTime;
    [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && fire1On)
        {  
            DoSkillOne();
            skillOneLastTime = Time.time;
        }
        if(Input.GetButtonDown("Fire2") && fire2On)
        {
            DoSkillTwo();
            skillTwoLastTime = Time.time;
        }
        if(Time.time > skillOneCooldown + skillOneLastTime)
        {
            fire1On = true;
        }
        if(Time.time > skillTwoCooldown + skillTwoLastTime)
        {
            fire2On = true;
        }
    }

    private void DoSkillOne()
    {
        fire1On = false;
        Debug.Log("Skill One");
        Instantiate(skillOne,skillOne.transform.position + shootOffset,skillOne.transform.rotation);
    }

    private void DoSkillTwo()
    {
        fire2On = false;
        Debug.Log("Skill Two");
        Instantiate(skillTwo,skillTwo.transform.position + shootOffset,skillTwo.transform.rotation);
    }
}
