using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class O_DestinationTile : MonoBehaviour
{
    public void SandLandDessolve()
    {
        StartCoroutine(Dessolve(transform.Find("Container")));
    }

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
        GetComponent<O_TileInfoContainer>().TopTileTransition("Finished");
        yield return new WaitForSeconds(0.2f);
    }
}
