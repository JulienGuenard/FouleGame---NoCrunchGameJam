using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenu;
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
       if(Instance == null)
       {
            Instance = this;
       }
       else if (Instance != null && Instance != this)
       {
            Destroy(Instance);
       }
    }

    private void Update()
    {
        CheckPause();
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActiveMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void ClearMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

   public void ResumeGame()
    {
        ClearMenu(PauseMenu);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        ActiveMenu(PauseMenu);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void CheckPause()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && PauseMenu != null)
        {
            if(GameIsPaused)
            {
                ResumeGame();

            }
            else
            {
               Pause();  
            }
        }
    }
}
