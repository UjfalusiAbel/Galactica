using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator
{
    private static MeshGenerator instance;
    public static MeshGenerator Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new MeshGenerator();
            }
            return instance;
        }
    }

    public Mesh GenerateMesh(float[,] heightMap, float heightMultiplier)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topleftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y] * heightMultiplier, topleftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height); 
                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        
        Mesh mesh = meshData.GenerateMesh();
        return mesh;
    }
}
