using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Bird : O_ElementMovable
{
    public float height;

    //protected override void MoveToNextTile(TileRelativePos direction)
    //{
    //    //base.MoveToNextTile()
    //    //isMoveActionEnd = false;
    //    TurnAround(direction);

    //    Transform targetTile = currentTile.GetComponentInParent<O_TileInfoContainer>().neighborTiles[direction].transform;
    //    //Debug.Log(targetTile.GetComponent<O_TileInfoContainer>().thisInfo.tileType + " " + targetTile.name);
    //    if (targetTile.GetComponent<O_TileInfoContainer>().thisInfo.tileType == TileType.Ocean)
    //    {
    //        currentMoveState = MoveState.OnMove;
    //        Vector3 targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
    //        transform.DOMove(targetPos, 1).OnComplete(() => ChangeOnLandingTile());
    //    }
    //    else
    //    {
    //        M_FloatingSeed seed = FindObjectOfType<M_FloatingSeed>();
    //        if (seed.CheckIsSeedAttachedThis(transform))
    //        {
    //            seed.GetComponent<M_SeedAction>().tile_Landing = currentTile;
    //            seed.ExitCircleMovement();
    //        }

    //    }
    //    void ChangeOnLandingTile()
    //    {
    //        currentTile = targetTile;
    //        CheckWhetherThereIsMonsoon();
    //    }
    //}


    protected override void Locating()
    {
        base.Locating();
        Vector3 newPos = new Vector3(currentTile.position.x, height, currentTile.position.z);
        transform.position = newPos;
    }
}
