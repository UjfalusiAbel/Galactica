using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilter
{
    public float EvaluatePoint(Vector3 point);
}
