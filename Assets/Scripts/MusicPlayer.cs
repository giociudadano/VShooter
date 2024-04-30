using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{   
    [SerializeField] AudioClip[] tracklist;
    [SerializeField] private GameObject cameraReference;
    [SerializeField] private AudioSource music;
    private static MusicPlayer _instance;

    public static MusicPlayer Instance
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
        if (!music.isPlaying) {
            transform.position = cameraReference.transform.position;
            PlayRandomTrack();
        }
    }

    public void Play()
    {
        music.Play();
    }

    public void SetTrack(AudioClip track)
    {
        music.clip = track;
    }

    public void PlayRandomTrack()
    {
        music.Stop();
        AudioClip track = tracklist[Random.Range(0, tracklist.Length)];
        if (track != null) {
            music.clip = track;
            music.Play();
        }
    }

}
