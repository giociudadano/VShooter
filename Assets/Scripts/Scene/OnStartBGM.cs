using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartBGM : MonoBehaviour
{

    private MusicPlayer musicPlayer;

    [SerializeField] private int trackIndex;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
        musicPlayer.PlayTrack(trackIndex);
    }

}
