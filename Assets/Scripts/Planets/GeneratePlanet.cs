using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratePlanet : MonoBehaviour
{
    [Range(5,256)]
    public int resolution = 128;
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [SerializeField,HideInInspector]
    private MeshFilter[] meshFilters;
    private TerrainFace[] terrainFaces;

    private ShapeGenerator shapeGenerator;
    private ColorGenerator colorGenerator;

    public Gradient GetGradient
    {
        get
        {
            return colorSettings.gradient;
        }
    }

    public void Initialize(ColorSettings colorSettings, ShapeSettings shapeSettings)
    {
        this.colorSettings = colorSettings;
        this.shapeSettings = shapeSettings;

        shapeGenerator = new ShapeGenerator();
        colorGenerator = new ColorGenerator();

        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0) 
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) 
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;
                meshObject.transform.position = transform.position;
                meshObject.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMat;

            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i], shapeGenerator);
        }
    }

    public void ConstructPlanet()
    {
        GenerateMesh();
        GenerateColors();
    }

    private void GenerateMesh()
    {
        int i = 0;
        foreach(var terrainFace in terrainFaces)
        {
            terrainFace.ConstructMesh();
            transform.GetChild(i).AddComponent<MeshCollider>().convex = true;
            transform.GetChild(i).tag = "Planet";
            i++;
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    private void GenerateColors()
    {
        colorGenerator.UpdateColors();
    }
}
