using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator 
{
    public ShapeSettings settings;
    public MinMax elevationMinMax;
    
    private INoiseFilter[] filters;

    public void UpdateSettings(ShapeSettings settings)
    {
        this.settings = settings;
        filters = new INoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < filters.Length; i++) 
        {
            filters[i] = NoiseFilterFactory.CreateFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0f;
        float elevation = 0f;

        if(filters.Length > 0)
        {
            firstLayerValue = filters[0].EvaluatePoint(pointOnUnitSphere);
            if (settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < filters.Length; i++) 
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerForMask) ? firstLayerValue : 1f;
                elevation += filters[i].EvaluatePoint(pointOnUnitSphere) * mask;
            }
        }
        elevation = settings.radius * (1f + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
