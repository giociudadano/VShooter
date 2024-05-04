using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraReference;
    [SerializeField] private AudioClip shootingSfx;
    [SerializeField] private AudioClip impactSfx;
    [SerializeField] private AudioClip killSfx;
    [SerializeField] private AudioSource oneShot;

    private static SfxManager _instance;
    public static SfxManager Instance
    {
        get { return _instance; }
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //  If an instance already exists, destroy this one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //  Otherwise, set this instance as the singleton instance
        _instance = this;

        //  Critical line to ensure that the music doesn't reset (this requires that the audio source isn't a child of a gameobject)
        DontDestroyOnLoad(gameObject);
    }

    //  Start is called before the first frame update
    void Start()
    {   
        transform.position = cameraReference.transform.position;
    }

    public void PlayShootingSfx()
    {
        oneShot.PlayOneShot(shootingSfx);
    }

    public void PlayImpactSfx()
    {
        oneShot.PlayOneShot(impactSfx);
    }

    public void PlayKillSfx()
    {
        oneShot.PlayOneShot(killSfx);
    }

    public void PlayOneShot(AudioClip sfx)
    {
        oneShot.PlayOneShot(sfx);
    }


}
