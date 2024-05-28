using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

  //private static List<GameObject> persistentObjects = new List<GameObject>();
    void Start() {
        
    }
    void Update() {
        
    }

    //  Hackish method to destroy all DontDestroyOnLoadObjects
    private void DestroyPersistentObjects() {
      var sacrifice = new GameObject("Sacrificial Lamb");
      DontDestroyOnLoad(sacrifice);

      foreach(var root in sacrifice.scene.GetRootGameObjects()) {
        Destroy(root);
      }
    }

    public void ReloadScene()
    {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
      Time.timeScale = 1;
      DestroyPersistentObjects();
      SceneManager.LoadScene("CharacterSelect");
    }
}
