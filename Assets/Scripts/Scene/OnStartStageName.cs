using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OnStartStageName : MonoBehaviour
{
    private TextMeshProUGUI stageText;

    // Start is called before the first frame update
    void Start()
    {
        stageText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        stageText.text = "Stage " + (SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Update is called once per frame
    void Update()
    {
        // If you need to update the text dynamically, do it here
    }
}
