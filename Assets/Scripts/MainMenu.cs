using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        DataPersistenceManager.isNewGame = true;
        SceneManager.LoadScene("WorldScene");
    }

    public void LoadGame()
    {
        DataPersistenceManager.isNewGame = false;
        SceneManager.LoadScene("WorldScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
