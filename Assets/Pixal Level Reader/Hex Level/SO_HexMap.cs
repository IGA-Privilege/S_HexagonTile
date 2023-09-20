using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Level Color Data", menuName = "Texture Level/Hex Map Data")]
public class SO_HexMap : ScriptableObject
{
    public Texture2D palette;
    public Texture2D map;
    public List<HexTileBinder> hexTiles;

    public void ColorUpdate()
    {
        hexTiles = new List<HexTileBinder>();
        for (int x = 0; x < palette.width; x++)
            hexTiles.Add(new HexTileBinder
            {
                color = palette.GetPixel(x, 0),
                colorObj = null
            });
    }

    private void OnValidate()
    {
        if (palette != null && hexTiles.Count == 0) ColorUpdate();
        else if(palette == null) hexTiles.Clear();
    }
}

[System.Serializable]
public class HexTileBinder
{
    public Color color;
    public GameObject colorObj;
}