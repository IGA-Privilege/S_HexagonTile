using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Seed : Singleton<M_Seed>
{
    public bool isAttaching = false;
    public bool isMoveing = false;
    [HideInInspector] public O_Tile currentLandingTile;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveTo(Vector3 targetPos)
    {

    }
}
