using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;
using System.Numerics;

public enum TileRelativePos { West,SouthWest,SouthEast,East,NorthEast,NorthWest}
public class O_TileInfoContainer : MonoBehaviour
{
    [HideInInspector] public HexTileTransformData thisInfo;
    public MMF_Player mmf_DropDown;
    public Dictionary<TileRelativePos,O_TileInfoContainer> neighborTiles = new Dictionary<TileRelativePos,O_TileInfoContainer>();

    public List<O_ElementBase> onTileElements = new List<O_ElementBase>();
    public List<WindLevelRegister> onTileWinds = new List<WindLevelRegister>();

    void Start()
    {
        TileTransformation();
        DetermineNearbyTile();
        //SpecialTileInitialize();
    }

    private void DetermineNearbyTile() //通过Tag来检测，得注意Collider和Tag的对应物体的层级
    {
        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;
            // 依据角度获取弧度
            float angleInRadians = 90 + i * 60 * Mathf.Deg2Rad;
            // 使用Sin和Cos函数来获取2D向量
            UnityEngine.Vector2 vector = new UnityEngine.Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            //Debug.Log("entered");
            UnityEngine.Vector3 v3HeigtOffset = new UnityEngine.Vector3(0, 1, 0);
            if (Physics.Raycast(transform.Find("Middle").position+ v3HeigtOffset, new UnityEngine.Vector3(vector.x, 0, vector.y)*100, out hit))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Tile" && hit.transform.GetComponentInParent<O_TileInfoContainer>() != this)
                {
                    TileRelativePos newPos = i switch
                    {
                        0 => TileRelativePos.NorthWest,
                        1 => TileRelativePos.West,
                        2 => TileRelativePos.SouthWest,
                        3 => TileRelativePos.SouthEast,
                        4 => TileRelativePos.East,
                        _ => TileRelativePos.NorthEast
                    };
                    neighborTiles.Add(newPos, hit.transform.parent.GetComponent<O_TileInfoContainer>());
                }
            }
        }

        //Debug.Log(neighborTiles.Count);
    }

    private void TileTransformation()
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 0);
        transform.Find("Middle").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 1);
        transform.Find("Bottom").GetComponent<MeshRenderer>().material = M_Tile.Instance.GetMaterial(thisInfo.tileType, 2);
    }

    public void TopTileTransition()
    {
        Transform topTile = transform.Find("Top");
        StartCoroutine(Dessolve(topTile.GetComponent<MeshRenderer>()));
    }

    public void TopTileTransition(string isFinished)
    {
        Transform topTile = transform.Find("Top");
        StartCoroutine(DessolveToStart(topTile.GetComponent<MeshRenderer>()));
    }

    IEnumerator Dessolve(MeshRenderer targetMesh)
    {
        while (targetMesh.material.GetFloat("_ControlledTime") < 1)
        {
            float newTime = targetMesh.material.GetFloat("_ControlledTime") + 0.005f;
            targetMesh.material.SetFloat("_ControlledTime", newTime);
            //targetMesh.materials[1].SetFloat("_ControlledTime", newTime);
            yield return null;
        }
        targetMesh.transform.localPosition = new UnityEngine.Vector3(0, 3, 0);
        targetMesh.material.SetColor("_DefaultColor", M_Game.instance.color_GrassLand);
        targetMesh.material.SetFloat("_ControlledTime", 0);
        mmf_DropDown.PlayFeedbacks();
        thisInfo.tileType = TileType.Grassland;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<M_TileContentGenerator>().GenerateOnSurfaceContent(thisInfo.tileType, transform);
        transform.AddComponent<O_TreeTile>();
        if (GetComponent<O_MountainTile>() != null) GetComponent<O_MountainTile>().enabled = false;
        if (GetComponent<O_SnowTile>() != null) GetComponent<O_SnowTile>().enabled = false;
    }

    IEnumerator DessolveToStart(MeshRenderer targetMesh)
    {
        while (targetMesh.material.GetFloat("_ControlledTime") < 1)
        {
            float newTime = targetMesh.material.GetFloat("_ControlledTime") + 0.005f;
            targetMesh.material.SetFloat("_ControlledTime", newTime);
            //targetMesh.materials[1].SetFloat("_ControlledTime", newTime);
            yield return null;
        }
        targetMesh.transform.localPosition = new UnityEngine.Vector3(0, 3, 0);
        targetMesh.material.SetColor("_DefaultColor", M_Game.instance.color_GrassLand);
        targetMesh.material.SetFloat("_ControlledTime", 0);
        mmf_DropDown.PlayFeedbacks();
        thisInfo.tileType = TileType.Start;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<M_TileContentGenerator>().GenerateOnSurfaceContent(thisInfo.tileType, transform);

        yield return new WaitForSeconds(1f);
        M_Game.instance.GameSucceed();
    }

    public Transform TryGetElements(ButtonType targetElements)
    {
        foreach (var elements in onTileElements)
            switch (targetElements)
            {
                case ButtonType.Sun:
                    if (elements is O_Sun) return elements.transform;
                    break;
                case ButtonType.Rain:
                    if (elements is O_RainCloud) return elements.transform;
                    break;
                case ButtonType.Monsoon:
                    if (elements is O_Monsoon) return elements.transform;
                    break;
                case ButtonType.Boat:
                    if (elements is O_Boat) return elements.transform;
                    break;
                case ButtonType.Bird:
                    if (elements is O_Bird) return elements.transform;
                    break;
            }

        return null;
    }

    public void AddElement(O_ElementBase targetElement)
    {
        if (!onTileElements.Contains(targetElement))
            onTileElements.Add(targetElement);
    }

    public void RemoveElement(O_ElementBase targetElement)
    {
        onTileElements.Remove(targetElement);
    }

    public void TryAddNewWindLevel(WindLevelRegister targetLevelData)
    {
        //Debug.Log(name);
        bool isChanged = false;
        if (onTileWinds.Count == 0)
        {
            //Debug.Log("enaaaasdasads");
            onTileWinds.Add(targetLevelData);
            SendMessageToMovableElements();
            isChanged = true;
        }
        else
        {
            if (CheckIsSourceExits(targetLevelData))
            {
                foreach (var windData in onTileWinds)
                {
                    if (windData.source == targetLevelData.source)
                    {
                        if (windData.windLevel != targetLevelData.windLevel)
                        {
                            windData.windLevel = targetLevelData.windLevel;
                            isChanged = true;
                        }
                        if (windData.forwardDirection != targetLevelData.forwardDirection)
                        {
                            windData.forwardDirection = targetLevelData.forwardDirection;
                            isChanged = true;
                        }
                    }
                }
            }
            else
            {
                onTileWinds.Add(targetLevelData);
                isChanged = true;
            }
        }

        if (isChanged) SendMessageToMovableElements();

        bool CheckIsSourceExits(WindLevelRegister targetLevelData)
        {
            foreach (var windData in onTileWinds)
                if (windData.source == targetLevelData.source) return true;

            return false;
        }
    }

    public void SendMessageToMovableElements()
    {
        foreach (var ele in onTileElements)
        {
            if(ele is O_ElementMovable)
            {
                //Debug.Log(ele.name);
                ele.GetComponent<O_ElementMovable>().Recheck();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        float angleInRadians = 90 + i * 60 * Mathf.Deg2Rad;
    //        // 使用Sin和Cos函数来获取2D向量
    //        UnityEngine.Vector2 vector = new UnityEngine.Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    //        Gizmos.DrawRay(transform.Find("Middle").position, new UnityEngine.Vector3(vector.x, 0, vector.y) * 10);
    //    }
    //}
}

[System.Serializable]
public class WindLevelRegister
{
    public int windLevel;
    public TileRelativePos forwardDirection;
    public O_Monsoon source;
}
