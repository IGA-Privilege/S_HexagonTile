using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[ExecuteInEditMode]
public class PLR_HexMap : MonoBehaviour
{
    public SO_HexMap colorData;
    public float distanceOffset;
    public List<HexTileData> hexTilesData = new List<HexTileData>();
    private Transform newLevelParent;

    #region Generate 3D Level
    [ContextMenu("Generate 3D Hex Map")]
    public void Generate3DLevel()
    {
        hexTilesData.Clear();
        newLevelParent = new GameObject("Hex Level Container").transform;
        for (int x = 0; x < colorData.map.width; x++)
            for (int y = 0; y < colorData.map.height; y++)
            {
                GenerateBrick(x, y, colorData.map);
            }
    }

    private void GenerateBrick(int x, int y, Texture2D targetMap)
    {
        Color pixelColor = targetMap.GetPixel(x, y);
        if (pixelColor.a == 0) return;
        else foreach (HexTileBinder colorObj in colorData.hexTiles)
            {
                if (colorObj.color.Equals(pixelColor))
                {
                    Vector3 pos = new Vector3(x * distance_Hori, 0, y * distance_Vert);
                    GameObject newTile = Instantiate(colorObj.colorObj, pos, Quaternion.identity);
                    HexTileData newTileData = new HexTileData
                    {
                        transform = newTile.transform,
                        index = hexTilesData.Count,
                        originPos = pos,
                    };
                    hexTilesData.Add(newTileData);
                    newTile.transform.SetParent(newLevelParent);
                }
                else continue;
            }
    }
    #endregion

    private void OnValidate()
    {
        CalculateHexValue();
        ResetTilesPosition();
    }

    public float radius_Outer = 1;
    private float radius_Inner;
    private float distance_Vert;
    private float distance_Hori;

    void CalculateHexValue()
    {
        radius_Inner = radius_Outer / Mathf.Sqrt(3);
        distance_Hori = radius_Inner * 2;
        distance_Vert = radius_Outer * 2;
    }

    private void ResetTilesPosition()
    {
        foreach (HexTileData tile in hexTilesData)
        {
            tile.transform.localPosition = distanceOffset/10 * tile.originPos;
        }
    }
}

[System.Serializable]
public class HexTileData
{
    public Transform transform;
    public int index;
    public Vector3 originPos;
}
