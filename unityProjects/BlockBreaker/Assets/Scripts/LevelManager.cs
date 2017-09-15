using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelManager : MonoBehaviour {
    public void loadLevel(string name)
    {
        Debug.Log("Level load requested: " + name);
        SceneManager.LoadScene(name);
    }
    public void quitRequest()
    {
        Debug.Log("Game exit requested.");
        Application.Quit();
    }
}
