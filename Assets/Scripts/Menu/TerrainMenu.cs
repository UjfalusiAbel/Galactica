using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainMenu : MonoBehaviour
{
    public void ReturnToSpace()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
