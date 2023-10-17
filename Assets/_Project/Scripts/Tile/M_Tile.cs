using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class M_Tile : Singleton<M_Tile>
{
    [HideInInspector] public Transform tile_Targeting;
    public List<Transform> currentTilesNeighbors = new List<Transform>();
    private PLR_HexMapTransform hexMap;
    public Dictionary<TileType, Material[]> tileMaterialBinder = new Dictionary<TileType, Material[]>();
    private M_TileContentGenerator tileContentGenerator;

    [HideInInspector] public bool isMoveAllowed = true;

    void Awake()
    {
        InitializeHexMap();
    }

    private void Start()
    {
        tileContentGenerator = FindObjectOfType<M_TileContentGenerator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(MapGen());
    }

    public void InitializeHexMap()
    {
        hexMap = FindObjectOfType<PLR_HexMapTransform>();

        foreach (HexTileTransfromBinder colorObj in hexMap.colorData.hexTiles)
        {
            GameObject sampleTile = Instantiate(colorObj.tileInfoContainer.tileObject);
            Material[] materials = new Material[3];
            for (int i = 0; i < 3; i++)
                materials[i] = sampleTile.transform.GetChild(i).GetComponent<MeshRenderer>().material;
            tileMaterialBinder.Add(colorObj.tileInfoContainer.tileType, materials);
            Destroy(sampleTile );
        }
    }

    IEnumerator MapGen()
    {
        foreach (var item in hexMap.hexTilesData)
        {
            O_TileInfoContainer info = item.tileTrans.GetComponent<O_TileInfoContainer>();
            //item.tileTrans.Find("Name").GetComponent<TMPro.TMP_Text>().text = info.thisInfo.tileName;
            item.tileTrans.Find("Name").GetComponent<TMPro.TMP_Text>().text = "";
            //Debug.Log("ENtered");
            if (info.thisInfo.tileType != TileType.Ocean)
            {
                item.tileTrans.GetComponent<O_TileInfoContainer>().mmf_DropDown.PlayFeedbacks();
                yield return new WaitForSeconds(0.1f);
            }
        }

        // ÈÃÉ½·åµØ¿éÍ¹Æð
        //foreach (var item in hexMap.hexTilesData)
        //{
        //    O_TileInfoContainer info = item.tileTrans.GetComponent<O_TileInfoContainer>();
        //    if (info.thisInfo.tileType == TileType.Mountain)
        //    {
        //        item.tileTrans.DOMoveY(5, 1);
        //        yield return new WaitForSeconds(0.2f);
        //    }
        //}

        yield return new WaitForSeconds(1);

        foreach (var item in hexMap.hexTilesData)
        {
            O_TileInfoContainer info = item.tileTrans.GetComponent<O_TileInfoContainer>();
            tileContentGenerator.GenerateOnSurfaceContent(info.thisInfo.tileType, item.tileTrans);
        }

        //foreach (var item in hexMap.hexTilesData)
        //{
        //    item.tileTrans.GetComponent<O_TileInfoContainer>().DetermineNearbyTile();
        //}
    }

    public Material GetMaterial(TileType tileType,int targetIndex) 
    {
        return tileMaterialBinder[tileType][targetIndex];
    }

    public void UpdateTargetingTile(Transform tileTrans)
    {
        if(tile_Targeting != tileTrans)
        {
            tile_Targeting = tileTrans;
            O_TileHighLighter.Instance.UpdateTargetingTile(tileTrans);
        }
    }
}
