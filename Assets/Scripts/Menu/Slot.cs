using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private int number;
    [SerializeField]
    private string freeText;
    [SerializeField]
    private string occupiedText;
    [SerializeField]
    private TMP_Text text;
    private bool occupied;
    public bool GetOccupied
    {
        get
        {
            return occupied;
        }
    }

    public int GetNumber
    {
        get
        {
            return number;
        }
    }

    void Start()
    {
        if(CheckSave())
        {
            text.text = occupiedText;
            occupied = true;
        }
        else
        {
            text.text = freeText;
            occupied = false;
        }
    }

    public bool CheckSave()
    {
        var path = Path.Combine(Application.persistentDataPath, FileHandler.Singleton.GetSlotAndFile(number));
        var res = File.Exists(path);
        return res;
    }
}
