using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    private NoiseSettings settings;
    private Noise noise;

    public SimpleNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
        noise = new Noise();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public float EvaluatePoint(Vector3 point)
    {
        float noiseValue = 0f;
        float frequency = settings.baseRoughness;
        float amplitude = 1f;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.center);
            noiseValue += (v + 1f) * 0.5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistance;
        }

        noiseValue = Mathf.Max(0f, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
