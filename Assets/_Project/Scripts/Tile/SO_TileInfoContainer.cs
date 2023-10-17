using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTile",menuName ="SO/New Tile")]
public class SO_TileInfoContainer : ScriptableObject
{
    public TileType tileType;
    public string tileName;
    public GameObject tileObject;
    public OnTileObject[] onTileObjects;
}

[System.Serializable]
public class OnTileObject
{
    public GameObject targetObj;
    [MinMaxRangeInt(0, 10)]
    public RangedInt toGenRange;
    [MinMaxRangeFloat(0, 3)]
    public RangedFloat toScaleRange;
    public InstantiateType instantiateType;
}

public enum InstantiateType { DropDown, ScaleUp}
