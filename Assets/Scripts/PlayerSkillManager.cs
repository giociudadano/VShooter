using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private GameObject skillOne;
    [SerializeField] private float skillOneCooldown = 15f;
    private bool skillOneFire = false;
    [SerializeField] private GameObject skillTwo;
    [SerializeField] private float skillTwoCooldown = 25f;
    private bool skillTwoFire = false;
    [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);

    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckSkill();
    }

    private void CheckSkill()
    {
        if (Input.GetButtonDown("Fire1") && !skillOneFire)
        {
            SkillOne();
        }
        if (Input.GetButtonDown("Fire2") && !skillTwoFire)
        {
            SkillTwo();
        }
    }

    private void SkillOne()
    {
        skillOneFire = true;
        Instantiate(skillOne, transform.position + shootOffset, skillOne.transform.rotation);
        Invoke("skillOneDelayActivation", skillOneCooldown);
    }

    private void SkillTwo()
    {
        skillTwoFire = true;
        Instantiate(skillTwo, transform.position + shootOffset, skillTwo.transform.rotation);
        Invoke("skillTwoDelayActivation", skillTwoCooldown);
    }

    private void skillOneDelayActivation()
    {
        skillOneFire = false;
    }

    private void skillTwoDelayActivation()
    {
        skillTwoFire = false;
    }
}
