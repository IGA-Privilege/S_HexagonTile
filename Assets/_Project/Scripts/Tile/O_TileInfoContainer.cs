using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class O_TileInfoContainer : MonoBehaviour
{
    [HideInInspector] public HexTileTransformData thisInfo;
    public MMF_Player mmf_DropDown;

    void Start()
    {
        TileTransformation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TileTransformation()
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 0);
        transform.Find("Middle").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 1);
        transform.Find("Bottom").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 2);
    }
}
