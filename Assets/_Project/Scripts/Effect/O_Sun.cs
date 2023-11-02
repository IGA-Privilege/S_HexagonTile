using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Sun : O_ElementBase
{
    bool isTriggered = false;
    public float height;
    public ParticleSystem vfx_SunLight;
    public float dessolveSpeed;

    void Start()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Locating()
    {
        base.Locating();
        Vector3 newPos = new Vector3(currentTile.position.x, height, currentTile.position.z);
        transform.position = newPos;
    }

    public override void Set()
    {
        base.Set();
        vfx_SunLight.Play();
    }

    protected override void Function()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            if (currentTile != null)
            {
                foreach (var item in currentTile.GetComponentInParent<O_TileInfoContainer>().neighborTiles)
                    if (item.Value.thisInfo.tileType == TileType.Snow)
                        item.Value.transform.GetComponent<O_SnowTile>().SnowDessolve();

                if (currentTile.GetComponentInParent<O_TileInfoContainer>().thisInfo.tileType == TileType.Snow)
                    currentTile.GetComponentInParent<O_SnowTile>().SnowDessolve();
            }
        }
    }

    //IEnumerator DessolveMesh(MeshRenderer targetMesh, O_TileInfoContainer targetTile)
    //{
    //    while (targetMesh.material.GetFloat("_ControlledTime") < 1)
    //    {
    //        float newTime = targetMesh.material.GetFloat("_ControlledTime") + dessolveSpeed;
    //        targetMesh.material.SetFloat("_ControlledTime", newTime);
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(0.2f);
    //    Destroy(gameObject, 1);
    //}

    //void TriggerTileEffect(O_TileInfoContainer targetTile)
    //{
    //    TileType tileType = targetTile.thisInfo.tileType;
    //    if (tileType == TileType.Grassland || tileType == TileType.FlowerLand)
    //        targetTile.GetComponent<O_TreeTile>().Growing();
    //    if (tileType == TileType.Mountain)
    //        targetTile.GetComponent<O_MountainTile>().MountainDessolve();
  
    //}
}
