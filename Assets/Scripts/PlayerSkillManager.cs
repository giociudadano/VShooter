using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [Header ("Skill Set")]
    [SerializeField] private GameObject go_SkillQ;
    [SerializeField] private GameObject go_SkillE;

    [Header ("Skill Set Cooldown")]
    [SerializeField] private float skillQCooldown = 7f;
    [SerializeField] private float skillECooldown = 15f;

    private Vector3 shootOffset = new Vector3(0f, 0f, 1f);
    private float skillQTimestamp;
    private float skillETimestamp;
    private bool skillQOnCooldown = false;
    private bool skillEOnCooldown = false;

    [Header ("Objects")]
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update(){
      if(Input.GetButtonDown("Fire1") && !skillQOnCooldown){  
        StartCoroutine(CastSkillQ());
      }
      if(Input.GetButtonDown("Fire2") && !skillEOnCooldown){
        StartCoroutine(CastSkillE());
      }
    }

    private IEnumerator CastSkillQ() {
        Instantiate(go_SkillQ,player.transform.position + shootOffset,player.transform.rotation);
				skillQOnCooldown = true;
        yield return new WaitForSeconds(skillQCooldown);
        skillQOnCooldown = false;
    }

		private IEnumerator CastSkillE() {
        Instantiate(go_SkillE,player.transform.position + shootOffset,player.transform.rotation);
				skillEOnCooldown = true;
        yield return new WaitForSeconds(skillQCooldown);
        skillEOnCooldown = false;
    }
}
