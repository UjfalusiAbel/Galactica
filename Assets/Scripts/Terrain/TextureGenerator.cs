using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator 
{
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

    public Texture2D GenerateTexture(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
