using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathNextStage : MonoBehaviour
{   

    private StageManager stageManager;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        stageManager.CompleteCurrentStage();
    }
}
