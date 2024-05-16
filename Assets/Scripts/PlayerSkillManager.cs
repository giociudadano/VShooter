using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour{
	[Header ("GUI")]
	[SerializeField] private GameObject skillQGUI;
	[SerializeField] private GameObject skillEGUI;

  [Header ("Projectiles")]
  [SerializeField] private GameObject go_SkillQ;
  [SerializeField] private GameObject go_SkillE;

  [Header ("Skill Set Cooldown")]
  [SerializeField] private float skillQBaseCooldown = 15f;
  [SerializeField] private float skillEBaseCooldown = 7f;

  private Vector3 shootOffset = new Vector3(0f, 0f, 1f);
  private bool skillQOnCooldown = false;
  private bool skillEOnCooldown = false;

  [Header ("Objects")]
  [SerializeField] private GameObject player;

  [SerializeField] private float bonusAbilityHasteFlat;

    // Start is called before the first frame update
  void Start() {
    skillQGUI.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}",skillQBaseCooldown)}s Cooldown";
    skillEGUI.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}",skillEBaseCooldown)}s Cooldown";
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
        float skillQCooldown = skillQBaseCooldown * (1 - CalculateCooldownReduction(bonusAbilityHasteFlat));
				skillQGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").gameObject.SetActive(true);
				StartCoroutine(skillQGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").GetComponent<SkillCooldownDisplayManager>().StartCooldown(skillQCooldown));
				skillQGUI.transform.Find("Skill Icon Mask/Skill Cooldown").gameObject.SetActive(true);
				skillQGUI.transform.Find("Skill Icon Mask/Skill Ring").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        yield return new WaitForSeconds(skillQCooldown);
        skillQOnCooldown = false;
				skillQGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").gameObject.SetActive(false);
				skillQGUI.transform.Find("Skill Icon Mask/Skill Cooldown").gameObject.SetActive(false);
				skillQGUI.transform.Find("Skill Icon Mask/Skill Ring").GetComponent<UnityEngine.UI.Image>().color = new Color((float) 42/255, (float) 227/255, 1, 1);
  }

	private IEnumerator CastSkillE() {
        Instantiate(go_SkillE,player.transform.position + shootOffset,player.transform.rotation);
				skillEOnCooldown = true;
        float skillECooldown = skillEBaseCooldown * (1 - CalculateCooldownReduction(bonusAbilityHasteFlat));
				skillEGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").gameObject.SetActive(true);
				StartCoroutine(skillEGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").GetComponent<SkillCooldownDisplayManager>().StartCooldown(skillECooldown));
				skillEGUI.transform.Find("Skill Icon Mask/Skill Cooldown").gameObject.SetActive(true);
				skillEGUI.transform.Find("Skill Icon Mask/Skill Ring").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        yield return new WaitForSeconds(skillECooldown);
        skillEOnCooldown = false;
				skillEGUI.transform.Find("Skill Icon Mask/Skill Cooldown Time").gameObject.SetActive(false);
				skillEGUI.transform.Find("Skill Icon Mask/Skill Cooldown").gameObject.SetActive(false);
				skillEGUI.transform.Find("Skill Icon Mask/Skill Ring").GetComponent<UnityEngine.UI.Image>().color = new Color((float) 42/255, (float) 227/255, 1, 1);
  }

    private float CalculateCooldownReduction(float bonusAbilityHasteFlat) {
        return bonusAbilityHasteFlat / (bonusAbilityHasteFlat + 100);
    }

    public void SetBonusAbilityHaste(float bonusAbilityHasteFlat) {
      this.bonusAbilityHasteFlat = bonusAbilityHasteFlat;
      skillQGUI.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}", skillQBaseCooldown * (1 - CalculateCooldownReduction(bonusAbilityHasteFlat)))}s Cooldown";
      skillEGUI.transform.Find("Skill Tooltip/Cooldown").GetComponent<TMP_Text>().text = $"{String.Format("{0:0.##}", skillEBaseCooldown * (1 - CalculateCooldownReduction(bonusAbilityHasteFlat)))}s Cooldown";
    }
}
