using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDataGenerator : MonoBehaviour
{
    [SerializeField]
    private int numberOfPlanets = 5;
    [SerializeField]
    private int colorsOnGradient = 5;
    [SerializeField]
    private int numberOfLayers = 2;
    [SerializeField]
    private List<ColorListWrapper> colors;
    [SerializeField]
    private List<Material> materials;

    private static PlanetDataGenerator instance;
    public static PlanetDataGenerator Singleton
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

    public List<Tuple<ColorSettings, ShapeSettings>> GenerateData()
    {
        var res = new List<Tuple<ColorSettings, ShapeSettings>>();
        for (int i = 0; i < numberOfPlanets; i++) 
        {
            ColorSettings colorSettings = new ColorSettings();
            Gradient gradient = new Gradient();
            GradientColorKey[] keys = new GradientColorKey[colorsOnGradient];
            for (int j = 0; j < colorsOnGradient; j++) 
            {
                GradientColorKey gradientColorKey = new GradientColorKey();
                List<int> pickedIndices = new List<int>();
                gradientColorKey.color = PickRandomColor(ref pickedIndices);
                gradientColorKey.time = UnityEngine.Random.Range(0f, 1f);
                if(j == colorsOnGradient - 1)
                {
                    gradientColorKey.time = 1f;
                }
                keys[j] = gradientColorKey;
            }
            gradient.colorKeys = keys;
            colorSettings.gradient = gradient;
            colorSettings.planetMat = materials[i];

            ShapeSettings shapeSettings = new ShapeSettings();
            NoiseLayer[] noiseLayers = new NoiseLayer[numberOfLayers];

            for (int j = 0; j < numberOfLayers; j++)
            {
                NoiseLayer noiseLayer = new NoiseLayer();
                noiseLayer.enabled = true;
                if (j == 0) 
                {
                    noiseLayer.useFirstLayerForMask = true;
                }
                else
                {
                    noiseLayer.useFirstLayerForMask = UnityEngine.Random.Range(0, 10) % 2 == 0;
                }
                NoiseSettings noiseSettings = new NoiseSettings();
                noiseSettings.numLayers = UnityEngine.Random.Range(4, 9);
                noiseSettings.strength = UnityEngine.Random.Range(-1f, 0.35f);
                noiseSettings.baseRoughness = UnityEngine.Random.Range(-1f, 1f);
                noiseSettings.roughness = UnityEngine.Random.Range(1.5f, 3.5f);
                noiseSettings.persistance = UnityEngine.Random.Range(0.3f, 0.65f);
                noiseSettings.minValue = UnityEngine.Random.Range(0.75f, 1.1f);
                noiseSettings.weightMultiplier = UnityEngine.Random.Range(0.7f, 1.5f);
                noiseSettings.center = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));

                noiseLayer.noiseSettings = noiseSettings;
                noiseLayers[j] = noiseLayer;
            }

            shapeSettings.noiseLayers = noiseLayers;
            shapeSettings.radius = UnityEngine.Random.Range(6f, 50f);

            res.Add(new Tuple<ColorSettings, ShapeSettings>(colorSettings, shapeSettings));
        }
        return res;
    }

    private Color PickRandomColor(ref List<int> pickedIndices)
    {
        int listNum = UnityEngine.Random.Range(0, colors.Count);
        int i = UnityEngine.Random.Range(0, colors[listNum].list.Count);
        while(pickedIndices.Contains(i))
        {
            i = UnityEngine.Random.Range(0, colors[listNum].list.Count);
        }
        return colors[listNum].list[i];
    }

    [Serializable]
    class ColorListWrapper
    {
        public List<Color> list;
    }
}
