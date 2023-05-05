using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage 
{
    private static DataStorage instance;
    public static DataStorage Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new DataStorage();
            }
            return instance;
        }
    }

    public bool isStored = false;
    public Vector3 shipPosition;
    public Quaternion shipRotation;
}
