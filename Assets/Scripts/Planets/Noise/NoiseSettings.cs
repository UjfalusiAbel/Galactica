using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoiseSettings 
{
    [Range(1,8)]
    public int numLayers;
    public float strength = 1f;
    public float baseRoughness = 1f;
    public float roughness = 2f;
    public float persistance = 0.5f;
    public Vector3 center;
    public float minValue;
    public float weightMultiplier = 0.8f;
    public NoiseFilterType filterType;
}
