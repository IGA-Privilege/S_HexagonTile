using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxRangeIntAttribute : System.Attribute
{
    public int Min { get; private set; }
    public int Max { get; private set; }
    public MinMaxRangeIntAttribute(int min, int max){ Min = min; Max = max;}
}

[System.Serializable]
public struct RangedInt { public int minValue; public int maxValue; }
