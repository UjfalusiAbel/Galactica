using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainNoiseGenerator
{
    private static TerrainNoiseGenerator instance;
    public static TerrainNoiseGenerator Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new TerrainNoiseGenerator();
            }
            return instance;
        }
    }

    public float[,] GenerateNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[width, height];
        scale = Mathf.Clamp(scale, 0.0001f, 100000);

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) 
        {
            float offsetX = Random.Range(-1000f, 1000f) + offset.x;
            float offsetY = Random.Range(-1000f, 1000f) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 1f;

                for (int i = 0; i < octaves; i++) 
                {
                    float pointX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float pointY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(pointX, pointY) * 2f - 1f;

                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                minNoiseHeight = Mathf.Min(minNoiseHeight, noiseHeight);
                maxNoiseHeight = Mathf.Max(maxNoiseHeight, noiseHeight);

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
