using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    private int triangleIndex = 0;

    public MeshData(int width, int height)
    {
        vertices = new Vector3[width * height];
        uvs = new Vector2[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex++] = a;
        triangles[triangleIndex++] = b;
        triangles[triangleIndex++] = c;
    }

    public Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
