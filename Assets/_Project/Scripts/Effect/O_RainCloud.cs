using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class O_RainCloud : O_ElementMovable
{
    bool isRained = false;
    public float height;
    public float rainTime=1;
    public ParticleSystem vfx_Rain;
    public float dessolveSpeed;

    void Start()
    {
        
    }

    //protected override void Update()
    //{
    //    base.Update();
    //}

    protected override void Locating()
    {
        base.Locating();
        Vector3 newPos = new Vector3(currentTile.position.x, height, currentTile.position.z);
        transform.position = newPos;
    }

    //public override void Set()
    //{
    //    // 得写确认是否有风，没风的情况下就结束
    //    //Locating();
    //    //currentState = ElementState.OnFunction;
      
    //}

    protected override void Function()
    {
        if (!isRained)
        {
            isRained = true;
            vfx_Rain.Play();
            if (currentTile != null) DestroySelf();
        }
    }


    protected override void DestroySelf()
    {
        base.DestroySelf();
        O_TileInfoContainer tileInfo = currentTile.GetComponentInParent<O_TileInfoContainer>();
        StartCoroutine(DessolveMesh(transform.Find("Cloud").GetComponent<MeshRenderer>(), tileInfo));
    }

    IEnumerator DessolveMesh(MeshRenderer targetMesh,O_TileInfoContainer targetTile)
    {
        TriggerTileEffect(targetTile);
        yield return new WaitForSeconds(rainTime);
        vfx_Rain.Stop();
        while (targetMesh.material.GetFloat("_ControlledTime") < 1)
        {
            float newTime = targetMesh.material.GetFloat("_ControlledTime") + dessolveSpeed;
            targetMesh.material.SetFloat("_ControlledTime", newTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject, 1);
    }

    void TriggerTileEffect(O_TileInfoContainer targetTile)
    {
        TileType tileType = targetTile.thisInfo.tileType;
        if (tileType == TileType.Grassland || tileType == TileType.FlowerLand)
            targetTile.GetComponent<O_TreeTile>().Growing();
        if (tileType == TileType.Mountain)
            targetTile.GetComponent<O_MountainTile>().MountainDessolve();
        //if (tileType == TileType.Snow)
        //    targetTile.GetComponent<O_SnowTile>().SnowDessolve();
    }
}
