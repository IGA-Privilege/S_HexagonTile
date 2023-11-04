using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class O_ElementMovable : O_ElementBase
{
    public enum MoveState { Checking, OnMove, MoveEnd }
    protected MoveState currentMoveState = MoveState.Checking;
    private O_Monsoon initialSource = null;

    public override void Set()
    {
        currentState = ElementState.OnMove;
        CheckWhetherThereIsMonsoon();
        CheckWhetherThereIsSeed();
    }

    protected void CheckWhetherThereIsMonsoon()
    {
        if(currentTile.GetComponentInParent<O_TileInfoContainer>().onTileWinds.Count > 0)
        {
            if (GetStrongestWind() != null)
            {
                WindLevelRegister tempDate = GetStrongestWind();
                if (tempDate.windLevel > 0) MoveToNextTile(GetStrongestWind().forwardDirection);
                else MoveEndAction();
            }
            else MoveEndAction();
        }
        else MoveEndAction();

        WindLevelRegister GetStrongestWind()
        {
            WindLevelRegister tempWindData = new WindLevelRegister
            {
                windLevel = 0,
                forwardDirection = TileRelativePos.West,
                source = null,
            };

            if (initialSource == null)
            {
                tempWindData = currentTile.GetComponentInParent<O_TileInfoContainer>().onTileWinds[0];
                initialSource = tempWindData.source;
            }
            else
            {
                foreach (WindLevelRegister wind in currentTile.GetComponentInParent<O_TileInfoContainer>().onTileWinds)
                {
                    if (wind.source == initialSource) 
                    {
                        tempWindData = wind;
                        initialSource = tempWindData.source;
                    }
                }
            }

            foreach (WindLevelRegister wind in currentTile.GetComponentInParent<O_TileInfoContainer>().onTileWinds)
            {
                if (tempWindData.windLevel < wind.windLevel)
                {
                    tempWindData = wind;
                    initialSource = tempWindData.source;
                }
            }

            if (tempWindData.source == null) return null;
            else return tempWindData;
        }
    }

    protected void CheckWhetherThereIsSeed()
    {
        M_FloatingSeed seed = FindObjectOfType<M_FloatingSeed>();
        //Debug.Log(currentTile.parent.name + "    " + seed.GetComponent<M_SeedAction>().tile_Landing);
        if (seed.GetComponent<M_SeedAction>().tile_Landing == currentTile.parent)
        {
            if (this is O_RainCloud) return;
            seed.EnterCircleMovement(transform);
        }
    }

    public void Recheck()
    {
        CheckWhetherThereIsMonsoon();
    }

    protected virtual void MoveToNextTile(TileRelativePos direction)
    {
        //isMoveActionEnd = false;
        currentMoveState = MoveState.OnMove;
        Transform targetTile = currentTile.GetComponentInParent<O_TileInfoContainer>().neighborTiles[direction].transform;
        Vector3 targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
        TurnAround(direction);
        transform.DOMove(targetPos, 1).OnComplete(() => ChangeOnLandingTile());

        void ChangeOnLandingTile()
        {
            currentTile = targetTile;
            CheckWhetherThereIsMonsoon();
            EnterSnow();
        }

    }

    protected void TurnAround(TileRelativePos direction)
    {
        int angle = direction switch
        {
            TileRelativePos.SouthWest => 30,
            TileRelativePos.West => 90,
            TileRelativePos.NorthWest => 150,
            TileRelativePos.NorthEast => 210,
            TileRelativePos.East => 270,
            _ => 330
        };

        transform.DORotate(new Vector3(0, angle, 0), 0.3f);
    }

    private void MoveEndAction()
    {
        //Locating();
        currentState = ElementState.OnFunction;
        M_FloatingSeed seed = FindObjectOfType<M_FloatingSeed>();
        if (seed != null)
            if (seed.CheckIsSeedAttachedThis(transform))
            {
                seed.GetComponent<M_SeedAction>().tile_Landing = currentTile;
                EnterSnow();
                seed.ExitCircleMovement();
            }
    }

    void EnterSnow()
    {
        M_FloatingSeed seed = FindObjectOfType<M_FloatingSeed>();

        Debug.Log(seed.TargetCenter);
        Debug.Log(currentTile.GetComponentInParent<O_TileInfoContainer>().thisInfo.tileType == TileType.Snow);

        if (seed.TargetCenter == transform
            && currentTile.GetComponentInParent<O_TileInfoContainer>().thisInfo.tileType == TileType.Snow)
            FindObjectOfType<O_Bar_Regular>().OnValueDecrease();
    }
}
