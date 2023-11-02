using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_MountainTile : MonoBehaviour
{
    private float dessolveSpeed = 0.003f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MountainDessolve()
    {
        StartCoroutine(Dessolve(transform.Find("Container").GetChild(0).GetComponent<MeshRenderer>()));
    }

    IEnumerator Dessolve(MeshRenderer targetMesh)
    {
        Debug.Log("enada");
        while (targetMesh.material.GetFloat("_ControlledTime") < 1)
        {
            float newTime = targetMesh.material.GetFloat("_ControlledTime") + dessolveSpeed;
            //targetMesh.material.SetFloat("_ControlledTime", newTime);
            targetMesh.materials[0].SetFloat("_ControlledTime", newTime);
            targetMesh.materials[1].SetFloat("_ControlledTime", newTime);
            targetMesh.materials[2].SetFloat("_ControlledTime", newTime);
            yield return null;
        }
        GetComponent<O_TileInfoContainer>().TopTileTransition();
        yield return new WaitForSeconds(0.2f);
        Destroy(targetMesh.gameObject, 1);
    }
}
