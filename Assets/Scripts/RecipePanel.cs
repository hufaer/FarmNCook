using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipePanel : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject shadowPanel;

    public bool isUnlocked = false;
    public int id;

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Destroy(shadowPanel);
        }
    }

    public void LoadData(GameData gameData)
    {
        isUnlocked = gameData.recipesUnlocked[id];
        if (isUnlocked)
        {
            Destroy(shadowPanel);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.recipesUnlocked[id] = isUnlocked;
    }
}
