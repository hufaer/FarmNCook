using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void ReturnToPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
