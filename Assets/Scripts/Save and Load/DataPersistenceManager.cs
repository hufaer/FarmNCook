using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Audio;

public class DataPersistenceManager : MonoBehaviour
{
    public static bool isNewGame = false;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer effectsMixer;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("DataPersistenceManager in more than one in the scene");
        }

        instance = this;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath + " " + fileName);
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        dataPersistenceObjects = FindDataPersistenceObjects();
        if (!isNewGame)
        {
            Debug.Log("Load");
            LoadGame();
        } else
        {
            NewGame();
        }
    }

    private List<IDataPersistence> FindDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> result = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        return new List<IDataPersistence>(result);
    }

    public void NewGame()
    {
        gameData = new GameData();
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("EffectsVolume");
    }

    public void LoadGame()
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        effectsMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("EffectsVolume")) * 20);


        gameData = dataHandler.Read();

        if (gameData == null)
        {
            Debug.Log("No data");
            NewGame();
        }

        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        Debug.Log("Save");
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }

        dataHandler.Write(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
