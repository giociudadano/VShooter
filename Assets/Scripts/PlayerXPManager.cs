using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerXPManager : MonoBehaviour {
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int level = 1;

    [SerializeField] private GameObject XPBar;

    [SerializeField] private float currentXP = 0;

    [SerializeField] private float maximumXP;

    [SerializeField] private float[] growthFactor = new float[2] {2.5f, 37.5f};

    void Start() {
       levelText.text = "LV " + level.ToString();
       maximumXP = (growthFactor[0] * (level+1f)) + (growthFactor[1] * (level+1f));
    }

    void Update() {
       
    }

    public void GainXP(float value) {
      currentXP += value;
      if (currentXP >= maximumXP) {
        LevelUp();
      }
      XPBar.transform.localScale = new Vector3(currentXP/maximumXP, 1f, 1f); 
    }

    public void LevelUp() {
      currentXP -= maximumXP;
      level += 1;
      levelText.text = "LV " + level.ToString();
      maximumXP = (growthFactor[0] * (level+1f)) + (growthFactor[1] * (level+1f));
    }
}
