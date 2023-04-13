using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject recipePanel;
    [SerializeField] GameObject settingsPanel;

    private void Update()
    {
        if (recipePanel.activeInHierarchy)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused && pausePanel.gameObject.activeInHierarchy)
            {
                Resume();
            } else
            {
                Pause();
            }
        }    
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenRecipeMenu()
    {
        pausePanel.SetActive(false);
        recipePanel.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
