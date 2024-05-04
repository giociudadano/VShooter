using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroySfx : MonoBehaviour
{   
    [SerializeField] private AudioClip sfx;
    private SfxManager sfxManager;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        sfxManager = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        
    }
}
