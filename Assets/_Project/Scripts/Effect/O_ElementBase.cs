using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ElementBase : MonoBehaviour
{
    protected enum ElementState { OnLocating, OnFunction, OnDestroy, OnMove }
    protected ElementState currentState = ElementState.OnLocating;
    protected Transform currentTile;

    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (currentState)
        {
            case ElementState.OnLocating:
                Locating();
                break;
            case ElementState.OnFunction:
                Function();
                break;
        }
    }

    protected virtual void Locating()
    {
        if (currentTile != null)
        {
            if (currentTile.GetComponentInParent<O_TileInfoContainer>().onTileElements.Contains(this))
                currentTile.GetComponentInParent<O_TileInfoContainer>().RemoveElement(this);
        }

        currentTile = M_Tile.Instance.tile_Targeting;
        if(currentTile!=null)
        currentTile.GetComponentInParent<O_TileInfoContainer>().AddElement(this);
    }

    public virtual void Set()
    {
        Locating();
        currentState = ElementState.OnFunction;
    }

    protected virtual void Function()
    {

    }

    protected virtual void DestroySelf()
    {
        currentTile.GetComponentInParent<O_TileInfoContainer>().RemoveElement(this);
    }
}
