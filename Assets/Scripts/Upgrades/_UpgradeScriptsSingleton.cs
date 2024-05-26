using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UpgradeScriptsSingleton : MonoBehaviour
{
    // Static instance of _UpgradeScriptsSingleton
    public static _UpgradeScriptsSingleton Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this, destroy this
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this instance
        Instance = this;

        // Make sure this object is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
