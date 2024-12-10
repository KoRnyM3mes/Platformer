using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int SceneToLoad;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneToLoad); //Loads a scene. Selected by SceneToLoad
        Debug.Log("Load complete!");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
