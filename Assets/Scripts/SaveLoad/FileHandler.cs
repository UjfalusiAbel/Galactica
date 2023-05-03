using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHandler 
{
    private readonly string[] slots = { "First", "Second", "Third" };
    private readonly string saveFileName = "SaveData.dat";
    private readonly string key = "secretkey123";
    private bool hasSaveData = false;
    private int slotNumber = 0;
    private SaveData data;
    private static FileHandler instance;
    public static FileHandler Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new FileHandler();
            }
            return instance;
        }
    }

    public bool HasSaveData
    {
        get
        {
            return hasSaveData;
        }
        set
        {
            hasSaveData = value;
        }
    }

    public int SetSlotNumber
    {
        set
        {
            slotNumber = value;
        }
    }

    public SaveData GetSaveData
    {
        get
        {
            return data;
        }
    }

    public string GetSlotAndFile(int num)
    {
        if (num >= slots.Length) 
        {
            throw new ArgumentOutOfRangeException();
        }
        return Path.Combine(slots[num], saveFileName);
    }

    public string WriteToJson<T>(T objectModel)
    {
        string result;
        var json = JsonUtility.ToJson(objectModel);
        var encoded = Encryptor.Singleton.EncryptOrDecrypt(json, key);
        string finalPath = Path.Combine(Application.persistentDataPath, slots[slotNumber], saveFileName);

        try
        {
            var dir = Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, slots[slotNumber]));
            File.Create(finalPath).Dispose();
            File.WriteAllText(finalPath, encoded);
            result = "Sikeres mentés!";
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            result = "Sikertelen mentés!";
        }
        return result;
    }

    private T LoadFromJson<T>(int slotNumber)
    {
        T data;
        string finalPath = Path.Combine(Application.persistentDataPath, slots[slotNumber], saveFileName);
        try
        {
            var text = File.ReadAllText(finalPath);
            var decrypt = Encryptor.Singleton.EncryptOrDecrypt(text, key);
            data = JsonUtility.FromJson<T>(decrypt);
            return data;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public void LoadSaveData(int slotNumber)
    {
        data = LoadFromJson<SaveData>(slotNumber);
        hasSaveData = true;
    }
}
