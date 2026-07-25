using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class pause : MonoBehaviour
{
    public GameObject pauseUI;
    public string mainMenuSceneName = "MainmenuUI";
    private bool p = false;

    private void Start()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (p)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        p = true;
    }

    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        p = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoGoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}