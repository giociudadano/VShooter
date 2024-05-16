using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class SkillCooldownDisplayManager : MonoBehaviour {
    
    private float currentCooldown;

    void Start() {
        
    }

    void Update() {
        
    }

    public IEnumerator StartCooldown(float baseCooldown) {
      currentCooldown = baseCooldown;
      while (currentCooldown > 0f) {
        gameObject.GetComponent<TMP_Text>().text = currentCooldown.ToString("0.0");
        yield return new WaitForSeconds(0.1f);
        currentCooldown -= 0.1f;
      }
      yield break;
    }
}
