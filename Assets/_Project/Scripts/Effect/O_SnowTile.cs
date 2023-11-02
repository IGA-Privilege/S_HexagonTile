using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_SnowTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SnowDessolve()
    {
        //StartCoroutine(Dessolve(transform.Find("Container").GetChild(0).GetComponent<MeshRenderer>()));
        StartCoroutine(Dessolve(transform.Find("Container")));
    }

    //IEnumerator Dessolve(MeshRenderer targetMesh)
    //{
    //    while (targetMesh.material.GetFloat("_ControlledTime") < 1)
    //    {
    //        float newTime = targetMesh.material.GetFloat("_ControlledTime") + 0.001f;
    //        targetMesh.material.SetFloat("_ControlledTime", newTime);
    //        //targetMesh.materials[1].SetFloat("_ControlledTime", newTime);
    //        yield return null;
    //    }
    //    GetComponent<O_TileInfoContainer>().TopTileTransition();
    //    yield return new WaitForSeconds(0.2f);
    //    Destroy(targetMesh.gameObject, 1);
    //}

    IEnumerator Dessolve(Transform container)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).DOScale(Vector3.zero, 0.7f);
            yield return new WaitForSeconds(0.05f);
            Destroy(container.GetChild(i).gameObject, 1);
        }
        yield return new WaitForSeconds(0.3f);
        GetComponent<O_TileInfoContainer>().TopTileTransition();
        yield return new WaitForSeconds(0.2f);

    }
}
