using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager  : MonoBehaviour
{
    //  Static instance of StageManager
    public static StageManager Instance { get; private set; }

    private void Awake()
    {
        //  If an instance of StageManager already exists, destroy the new one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //  Set the instance to this instance of StageManager
        Instance = this;

        // Make sure this object is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    // Method to load the next stage
    private void LoadNextStage()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range? Should we modulo to loop?
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("There are no more stages to load!!!");
        }
    }

    //  Route to load next stage
    public void CompleteCurrentStage()
    {
        Invoke("LoadNextStage", 3);
    }
}
