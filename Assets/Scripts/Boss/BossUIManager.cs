using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BossUIManager : MonoBehaviour
{
    public GameObject BossUI;
    [SerializeField] private String bossName;
    [SerializeField] private Sprite bossIcon;
    // Start is called before the first frame update
    void Start()
    {
        BossUI.SetActive(true);
        BossUI.transform.Find("Boss Name Text").gameObject.GetComponent<TMP_Text>().text = bossName;
        BossUI.transform.Find("Boss Image").gameObject.GetComponent<UnityEngine.UI.Image>().sprite = bossIcon;
    }
}