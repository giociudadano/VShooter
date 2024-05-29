using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaSkillTwo : MonoBehaviour
{
    [SerializeField] private GameObject tako;
    [SerializeField] private AudioClip summonSfx;
    private GameObject player;
    private SfxManager sfxManager;
    
    // Start is called before the first frame update
    void Start()
    {   
        sfxManager = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        sfxManager.PlayOneShot(summonSfx);
        Instantiate(tako, player.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
}
