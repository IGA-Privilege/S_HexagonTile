using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public enum TileRelativePos { West,SouthWest,SouthEast,East,NorthEast,NorthWest}
public class O_TileInfoContainer : MonoBehaviour
{
    [HideInInspector] public HexTileTransformData thisInfo;
    public MMF_Player mmf_DropDown;
    public Dictionary<TileRelativePos,O_TileInfoContainer> neighborTiles = new Dictionary<TileRelativePos,O_TileInfoContainer>();

    void Start()
    {
        TileTransformation();
        DetermineNearbyTile();
    }

    public void DetermineNearbyTile() //通过Tag来检测，得注意Collider和Tag的对应物体的层级
    {
        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;
            // 依据角度获取弧度
            float angleInRadians = 90 + i * 60 * Mathf.Deg2Rad;
            // 使用Sin和Cos函数来获取2D向量
            Vector2 vector = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            if (Physics.Raycast(transform.Find("Middle").position, new Vector3(vector.x, 0, vector.y), out hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    TileRelativePos newPos = i switch
                    {
                        0 => TileRelativePos.NorthWest,
                        1 => TileRelativePos.West,
                        2 => TileRelativePos.SouthWest,
                        3 => TileRelativePos.SouthEast,
                        4 => TileRelativePos.East,
                        _=> TileRelativePos.NorthEast
                    };
                    neighborTiles.Add(newPos, hit.transform.parent.GetComponent<O_TileInfoContainer>());
                }
            }
        }
    }

    private void TileTransformation()
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 0);
        transform.Find("Middle").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 1);
        transform.Find("Bottom").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 2);
    }
}
