using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxRangeFloatAttribute : System.Attribute
{
    public float Min { get; private set; }
    public float Max { get; private set; }
    public MinMaxRangeFloatAttribute(float min, float max){ Min = min; Max = max;}
}

[System.Serializable]
public struct RangedFloat { public float minValue; public float maxValue; }
