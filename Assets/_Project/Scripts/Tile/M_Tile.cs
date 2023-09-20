using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class M_Tile : Singleton<M_Tile>
{
    public Transform currentTile;
    public List<Transform> currentTilesNeighbors = new List<Transform>();
    private PLR_HexMapTransform hexMap;
    public Dictionary<TileType, Material[]> tileMaterialBinder = new Dictionary<TileType, Material[]>();

    void Awake()
    {
        InitializeHexMap();
    }

    private void Start()
    {
        DetermineNearbyTile(currentTile);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("dadsaasd");
            StartCoroutine(MapGen());
        }
    }

    public void DetermineNearbyTile(Transform _currentTile)
    {
        currentTilesNeighbors.Clear();
        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;

            // 依据角度获取弧度
            float angleInRadians = 90 + i * 60 * Mathf.Deg2Rad;
            // 使用Sin和Cos函数来获取2D向量
            Vector2 vector = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            if (Physics.Raycast(_currentTile.position, new Vector3(vector.x, 0, vector.y), out hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    currentTilesNeighbors.Add(hit.transform.parent);
                }
            }
        }
    }

    public void InitializeHexMap()
    {
        hexMap = FindObjectOfType<PLR_HexMapTransform>();

        foreach (HexTileTransfromBinder colorObj in hexMap.colorData.hexTiles)
        {
            GameObject sampleTile = Instantiate(colorObj.tile_Target);
            Material[] materials = new Material[3];
            for (int i = 0; i < 3; i++)
                materials[i] = sampleTile.transform.GetChild(i).GetComponent<MeshRenderer>().material;
            tileMaterialBinder.Add(colorObj.tileType, materials);
            Destroy(sampleTile );
        }
    }



    IEnumerator MapGen()
    {
        foreach (var item in hexMap.hexTilesData)
        {
            O_TileInfoContainer info = item.tileTrans.GetComponent<O_TileInfoContainer>();
            Debug.Log("ENtered");
            if(info.thisInfo.tileType!= TileType.Ocean)
            {
                item.tileTrans.GetComponent<O_TileInfoContainer>().mmf_DropDown.PlayFeedbacks();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public Material GetMaterial(TileType tileType,int targetIndex) 
    {
        return tileMaterialBinder[tileType][targetIndex];
    }
}
