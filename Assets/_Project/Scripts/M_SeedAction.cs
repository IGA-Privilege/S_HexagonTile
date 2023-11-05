using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class M_SeedAction : Singleton<M_SeedAction>
{
    public Transform tile_Landing;
    public bool isAttaching = false;
    public bool isMoveing = false;
    private O_Bar_Dotted bar_Energy;
    private O_Bar_Regular bar_Health;
    private M_FloatingSeed seed;

    void Start()
    {
        bar_Energy = FindObjectOfType<O_Bar_Dotted>();
        bar_Health = FindObjectOfType<O_Bar_Regular>();
        seed = GetComponent<M_FloatingSeed>();
    }

    void Update()
    {
        
    }

    public void TryRegularMove(Transform targetTile)
    {
        O_TileInfoContainer tileInfo = targetTile.GetComponent<O_TileInfoContainer>();
        TileType type = tileInfo.thisInfo.tileType;

        bool isMovable = IsTileMovable();
        bool isEnergyAffluent = bar_Energy.isEnergyAffluent() ? true : false;

        if(isMovable && isEnergyAffluent)
        {
            ExcuteSpecialMoveAction();
            ExcuteCommonMoveAction(targetTile);
        }

        bool IsTileMovable()
        {
            if (type == TileType.Mountain) return false;
            else if (type == TileType.Ocean)
            {
                foreach (var item in tileInfo.onTileElements)
                    if (item is O_Boat) return true;
            }
            else return true;

            return false;
        }

        void ExcuteCommonMoveAction(Transform targetTile)
        {
            Vector3 targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
            M_Tile.Instance.isMoveAllowed = false;
            transform.DOMove(targetPos, 1).OnComplete(() => M_Tile.Instance.isMoveAllowed = true);
            tile_Landing = targetTile;
            if (tileInfo.thisInfo.tileType == TileType.Destination) StartCoroutine(Arrived());
            bar_Energy.OnValueDecrease();
            if (tileInfo.thisInfo.tileType == TileType.Snow) bar_Health.OnValueDecrease();
        }

        void ExcuteSpecialMoveAction()
        {
            foreach (O_ElementBase elementBase in tileInfo.onTileElements)
            {
                if(elementBase is O_Bird)
                {
                    seed.EnterCircleMovement(elementBase.transform);
                }
                else if(elementBase is O_Boat)
                {
                    seed.EnterCircleMovement(elementBase.transform);
                }
            }
        }
    }

    public void TryFreeMove(Transform targetTile)
    {
        O_TileInfoContainer tileInfo = targetTile.GetComponent<O_TileInfoContainer>();
        TileType type = tileInfo.thisInfo.tileType;

        bool isMovable = IsTileMovable();

        if (isMovable)
        {
            ExcuteCommonMoveAction(targetTile);
        }

        bool IsTileMovable()
        {
            if (type == TileType.Mountain) return false;
            else if (type == TileType.Ocean)
            {
                foreach (var item in tileInfo.onTileElements)
                    if (item is O_Boat) return true;
            }
            else return true;

            return false;
        }

        void ExcuteCommonMoveAction(Transform targetTile)
        {
            Vector3 targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
            M_Tile.Instance.isMoveAllowed = false;
            transform.DOMove(targetPos, 1).OnComplete(() =>End());
            tile_Landing = targetTile;
            if (tileInfo.thisInfo.tileType == TileType.Destination) StartCoroutine(Arrived());
            if (tileInfo.thisInfo.tileType == TileType.Snow) bar_Health.OnValueDecrease();
        }

        void End()
        {
            GetComponent<O_WindDetector>().currentState = O_WindDetector.SeedState.Floating;
            M_Tile.Instance.isMoveAllowed = true;
        }
    }

    public void TryTriggerArrive()
    {
        if (tile_Landing.GetComponentInParent<O_TileInfoContainer>().thisInfo.tileType == TileType.Destination) StartCoroutine(Arrived());
    }


    IEnumerator Arrived()
    {
        yield return new WaitForSeconds(1);
        GetComponent<M_FloatingSeed>().PlantSeed();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<O_DestinationTile>().SandLandDessolve();
    }
}
