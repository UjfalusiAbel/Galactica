using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{

    private NoiseSettings settings;
    private Noise noise;

    public RigidNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
        noise = new Noise();
    }

    ///<summary>
    ///Térbeli pont véletlenszerű eltolása Perlin-noise segítségével
    ///Durvább eltolást eredményez sima szűrőnél
    ///0-1 közötti értékek négyzetre emelésével osztjuk el az értékeket túlnyomóan 0 felé tolva őket
    ///kiugró hegycsúcsokat hoz létre
    ///</summary>
    ///<param name="point">Gömb egy pontja</param>
    ///<returns>Eltolt pont a gömbben</returns>
    public float EvaluatePoint(Vector3 point)
    {
        float noiseValue = 0f;
        float frequency = settings.baseRoughness;
        float amplitude = 1f;
        float weight = 1f;

        for (int i = 0; i < settings.numLayers; i++)
        {
            
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);
            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistance;
        }

        noiseValue = Mathf.Max(0f, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
