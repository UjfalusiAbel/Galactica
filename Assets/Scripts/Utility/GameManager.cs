using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int seed;
    private GameManager instance;
    public GameManager Singleton
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GeneratePlanet[] planetGenerator = FindObjectsOfType<GeneratePlanet>();
        if(FileHandler.Singleton.HasSaveData)
        {
            seed = FileHandler.Singleton.GetSaveData.planetData.seed;
        }
        else
        {
            seed = UnityEngine.Random.Range(0, 2000000000);
        }
        UnityEngine.Random.InitState(seed);
        List<Tuple<ColorSettings, ShapeSettings>> planetData = PlanetDataGenerator.Singleton.GenerateData();
        for (int i = 0; i < planetGenerator.Length; i++) 
        {
            planetGenerator[i].Initialize(planetData[i].Item1, planetData[i].Item2);
            planetGenerator[i].ConstructPlanet();
        }
    }

    private void SaveGame()
    {
        SaveData saveData = new SaveData();
        PlanetData planetData = new PlanetData();
        PlayerData playerData = new PlayerData();

        planetData.seed = seed;

        saveData.planetData = planetData;
        saveData.playerData = playerData;

        FileHandler.Singleton.WriteToJson<SaveData>(saveData);
    }

    public void ExitGame()
    {
        SaveGame();
        SceneManager.LoadScene("Menu");
    }
}
