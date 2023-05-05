using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator 
{
    private TerrainType[] terrainTypes;

    private static TextureGenerator instance;
    public static TextureGenerator Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new TextureGenerator();
            }
            return instance;
        }
    }

    public TerrainType[] TerrainTypes
    {
        get
        {
            return terrainTypes;
        }
        set
        {
            terrainTypes = value;
        }
    }

    public Texture2D GenerateTexture(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
