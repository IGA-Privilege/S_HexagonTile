using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SeedAction : Singleton<M_SeedAction>
{
    public Transform tile_Landing;
    public bool isAttaching = false;
    public bool isMoveing = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveTo(Vector3 targetPos)
    {

    }

    public void TryRegularMove(Transform targetTile)
    {
        Vector3 targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
        M_Tile.Instance.isMoveAllowed = false;
        transform.DOMove(targetPos, 1).OnComplete(()=>M_Tile.Instance.isMoveAllowed=true);
        tile_Landing = targetTile;
        FindObjectOfType<O_Bar_Dotted>().OnValueDecrease();
    }
}
