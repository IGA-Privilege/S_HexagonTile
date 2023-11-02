using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[ExecuteInEditMode]
public class PLR_HexMapTransform : MonoBehaviour
{
    public SO_HexMapTransform colorData;
    public float distanceOffset;
    public List<HexTileTransformData> hexTilesData = new List<HexTileTransformData>();
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
        else foreach (HexTileTransfromBinder colorObj in colorData.hexTiles)
            {
                if (colorObj.color.Equals(pixelColor))
                {
                    Vector3 pos = new Vector3(x * distance_Hori, 0, y * distance_Vert);
                    GameObject newTile = Instantiate(colorData.tile_Base, pos, Quaternion.identity);
                    HexTileTransformData newTileData = new HexTileTransformData
                    {
                        tileTrans = newTile.transform,
                        index = hexTilesData.Count,
                        originPos = pos,
                        tile_Target = colorObj.tileInfoContainer.tileObject,
                        tileType = colorObj.tileInfoContainer.tileType,
                        tileName = colorObj.tileInfoContainer.tileName
                    };
                    Debug.Log(newTileData.tileName);
                    hexTilesData.Add(newTileData);
                    newTile.GetComponent<O_TileInfoContainer>().thisInfo = newTileData;
                    newTile.transform.SetParent(newLevelParent);

                    switch (newTileData.tileType)
                    {
                        case TileType.Start:
                            newTile.AddComponent<O_StartTile>();
                            newTile.name = "Start";
                            break;
                        case TileType.Destination:
                            newTile.AddComponent<O_DestinationTile>();
                            newTile.name = "Destination";
                            break;
                        case TileType.Grassland:
                            newTile.AddComponent<O_TreeTile>();
                            newTile.name = "GrassLand";
                            break;
                        case TileType.Mountain:
                            newTile.AddComponent<O_MountainTile>();
                            newTile.name = "Montain";
                            break;
                        case TileType.Ocean:
                            newTile.AddComponent<O_OceanTile>();
                            newTile.name = "Ocean";
                            break;
                        case TileType.FlowerLand:
                            newTile.AddComponent<O_TreeTile>();
                            newTile.name = "FlowerLand";
                            break;
                        case TileType.Snow:
                            newTile.AddComponent<O_SnowTile>();
                            newTile.name = "Snow";
                            break;
                    }
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
        foreach (HexTileTransformData tile in hexTilesData)
        {
            tile.tileTrans.localPosition = distanceOffset/10 * tile.originPos;
        }
    }
}

[System.Serializable]
public class HexTileTransformData
{
    public Transform tileTrans;
    public int index;
    public GameObject tile_Target;
    public TileType tileType;
    public Vector3 originPos;
    public string tileName;
}
