using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]    GameObject pauseMenu;
    [SerializeField]    GameObject interfaceIG;

                        public static bool gameIsPaused = false;

                        public static MenuManager instance { get; private set; }

    private void Awake()
    {
       if(instance == null) instance = this;
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()                  { Application.Quit(); }
    public void ActiveMenu(GameObject menu) { menu.SetActive(true); }
    public void ClearMenu(GameObject menu)  { menu.SetActive(false); }

   public void ResumeGame()
    {
        ClearMenu(pauseMenu);
        Time.timeScale = 1f;
        gameIsPaused = false;
        ActiveMenu(interfaceIG);
    }
    public void Pause()
    {
        ActiveMenu(pauseMenu);
        Time.timeScale = 0f;
        gameIsPaused = true;
        ClearMenu(interfaceIG);
    }

    public void CheckPause()
    {
        if (!Input.GetKeyUp(KeyCode.Escape))    return;
        if (pauseMenu == null)                  return;

        if(gameIsPaused)    ResumeGame();
        else                Pause();
    }
}
