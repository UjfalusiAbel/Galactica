using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace 
{
    private Mesh mesh;
    private int resolution;
    private Vector3 localY;
    private Vector3 localX;
    private Vector3 localZ;
    private ShapeGenerator shapeGenerator;

    public TerrainFace(Mesh mesh, int resolution, Vector3 localY, ShapeGenerator shapeGenerator)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localY = localY;
        this.shapeGenerator = shapeGenerator;
        localX = new Vector3(localY.y, localY.z, localY.x);
        localZ = Vector3.Cross(localY, localX);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        for (int i = 0; i < resolution; i++) 
        {
            for (int j = 0; j < resolution; j++)
            {
                int k = i + j * resolution;
                Vector2 percentage = new Vector3(i, j) / (resolution - 1);
                Vector3 pointOnUnitCube = localY + (percentage.x - 0.5f) * 2f * localX + (percentage.y - 0.5f) * 2f * localZ;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[k] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                if (i != resolution - 1 && j != resolution - 1) 
                {
                    triangles[triIndex++] = k;
                    triangles[triIndex++] = k + resolution + 1;
                    triangles[triIndex++] = k + resolution;

                    triangles[triIndex++] = k;
                    triangles[triIndex++] = k + 1;
                    triangles[triIndex++] = k + resolution + 1;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
