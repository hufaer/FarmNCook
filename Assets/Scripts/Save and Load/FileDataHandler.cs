using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirectoryName = "";
    private string dataFileName = "";
    private bool useEncryption = true;
    private readonly string encryptionCode = "crocodile";

    public FileDataHandler(string dataDirectoryName, string dataFileName, bool useEncryption)
    {
        this.dataDirectoryName = dataDirectoryName;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Read()
    {
        string path = Path.Combine(dataDirectoryName, dataFileName);
        GameData loaded = null;

        if (File.Exists(path))
        {
            try
            {
                string loadString = "";
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        loadString = sr.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    loadString = Encrypt(loadString);
                }

                loaded = JsonUtility.FromJson<GameData>(loadString);
            } catch (Exception e)
            {
                Debug.LogError("Exception reading:" + e);
            }
        }
        return loaded;
    }

    public void Write(GameData gameData)
    {
        string path = Path.Combine(dataDirectoryName, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string json = JsonUtility.ToJson(gameData, true);

            if (useEncryption)
            {
                json = Encrypt(json);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(json);
                }
            }
        } catch (Exception e)
        {
            Debug.LogError("Error writing:" + e);
        }
    }

    private string Encrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; ++i)
        {
            result += (char)(data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }
        return result;
    }
}
