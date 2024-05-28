using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
}
