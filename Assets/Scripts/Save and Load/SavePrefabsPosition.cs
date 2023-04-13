using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefabsPosition : MonoBehaviour, IDataPersistence
{
    public List<GameObject> objects = new List<GameObject>();
    public List<Vector3> deleted = new List<Vector3>();

    public void LoadData(GameData gameData)
    {
        Debug.Log(gameData.deletedObjects.Count);
        foreach (Vector3 obj in gameData.deletedObjects)
        {
            foreach (GameObject objScene in objects)
            {
                if (obj == objScene.transform.position)
                {
                    Debug.Log("URAAAAAAAAAAAAAAAAaa");
                    Destroy(objScene);
                }
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        foreach (Vector3 position in deleted)
        {
            gameData.deletedObjects.Add(position);
        }
    }
}
