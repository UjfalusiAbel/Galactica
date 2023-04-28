using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory 
{
    public static INoiseFilter CreateFilter(NoiseSettings settings)
    {
        switch(settings.filterType)
        {
            case NoiseFilterType.Simple:
                return new SimpleNoiseFilter(settings);
            case NoiseFilterType.Rigid:
                return new RigidNoiseFilter(settings);
            default:
                return new SimpleNoiseFilter(settings);
        }
    }
}
