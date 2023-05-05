using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject planetLanding;
    private Rigidbody ship;
    private TerrainType[] terrainTypes;
    private bool disabled = false;

    private void Start()
    {
        if(DataStorage.Singleton.isStored)
        {
            ship.transform.position = DataStorage.Singleton.shipPosition;
            ship.transform.rotation = DataStorage.Singleton.shipRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Planet" && !disabled)
        {
            ship = GetComponent<Rigidbody>();
            ship.velocity = Vector3.zero;
            ship.angularVelocity = Vector3.zero;
            Spaceship.Singleton.SetInputIsEnabled = false;
            Gradient gradient = collision.collider.transform.parent.GetComponent<GeneratePlanet>().GetGradient;
            terrainTypes = new TerrainType[gradient.colorKeys.Length];
            int i = 0;
            foreach(var colorKey in gradient.colorKeys)
            {
                terrainTypes[i] = new TerrainType() { color = colorKey.color, height = colorKey.time };
            }
            planetLanding.SetActive(true);
        }
    }

    public void LoadTerrain()
    {
        DataStorage.Singleton.isStored = true;
        DataStorage.Singleton.shipPosition = ship.transform.position;
        DataStorage.Singleton.shipRotation = ship.transform.rotation;
        TextureGenerator.Singleton.TerrainTypes = terrainTypes;
        SceneManager.LoadScene("Terrain");
    }

    public void Return()
    {
        Spaceship.Singleton.SetInputIsEnabled = true;
        planetLanding.SetActive(false);
        disabled = true;
        Invoke(nameof(ResetDisabled), 5f);
    }

    private void ResetDisabled()
    {
        disabled = false;
    }
}
