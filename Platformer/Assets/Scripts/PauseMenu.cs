using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public int SceneToLoad = 0;
    public UnityEvent pauseEvent, resumeEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        resumeEvent.Invoke();
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseEvent.Invoke();
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RMainMenu()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    
}
