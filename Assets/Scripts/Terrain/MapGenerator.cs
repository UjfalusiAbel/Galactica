using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField, Range(1, 200)]
    private int width;
    [SerializeField, Range(1, 200)]
    private int height;
    [SerializeField]
    private float scale;
    [SerializeField, Range(1, 8)]
    private int octaves;
    [SerializeField]
    private float persistance;
    [SerializeField, Range(1f, 10f)]
    private float lacunarity;
    [SerializeField]
    private float heightMultiplier;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private TerrainType[] terrainTypes;
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshRenderer meshRenderer;

    private void Start()
    {
        terrainTypes = TextureGenerator.Singleton.TerrainTypes;
        GenerateMap();
    }
    public void GenerateMap()
    {
        float[,] noiseMap = TerrainNoiseGenerator.Singleton.GenerateNoiseMap(width, height, scale, octaves, persistance, lacunarity, offset);
        Color[] colormap = new Color[width * height];

        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrainTypes.Length; i++) 
                {
                    if(currentHeight <= terrainTypes[i].height)
                    {
                        colormap[y * width + x] = terrainTypes[i].color;
                        break;
                    }
                }
            }
        }

        Texture2D texture = TextureGenerator.Singleton.GenerateTexture(colormap, width, height);
        Mesh mesh = MeshGenerator.Singleton.GenerateMesh(noiseMap, heightMultiplier);
        DrawMesh(mesh, texture);
    }

    public void DrawMesh(Mesh mesh, Texture2D texture)
    {
        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
        MeshCollider collider;
        if(!meshFilter.TryGetComponent<MeshCollider>(out collider))
        {
            collider = meshFilter.gameObject.AddComponent<MeshCollider>();
        }
        collider.sharedMesh = mesh;
    }
}
