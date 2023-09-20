using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Level Color Data", menuName = "Texture Level/Hex Map Transformation Data")]
public class SO_HexMapTransform : ScriptableObject
{
    public Texture2D palette;
    public Texture2D map;
    public GameObject tile_Base;
    public List<HexTileTransfromBinder> hexTiles;

    private void OnValidate()
    {
        if (palette != null && hexTiles.Count == 0) ColorUpdate();
        else if(palette == null) hexTiles.Clear();
    }

    public void ColorUpdate()
    {
        hexTiles = new List<HexTileTransfromBinder>();
        for (int x = 0; x < palette.width; x++)
            hexTiles.Add(new HexTileTransfromBinder
            {
                color = palette.GetPixel(x, 0),
                tile_Target = null,
                tileType = TileType.Start
            });
    }
}

[System.Serializable]
public class HexTileTransfromBinder
{
    public Color color;
    public GameObject tile_Target;
    public TileType tileType;
}

public enum TileType { Start, Destination, Grassland, Mountain, Ocean }