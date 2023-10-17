using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_InputListener : MonoBehaviour
{
    public Transform arCamera;
    public bool isMobile;
    public RaycastHit hit;

    void Update()
    {
        if (isMobile) MobileCenterRaycastHit();
        else PCRaycastHit();
    }

    private void MobileCenterRaycastHit()
    {
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Tile") M_Tile.Instance.UpdateTargetingTile(hit.transform.parent);
        }
        else M_Tile.Instance.UpdateTargetingTile(null);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    Debug.Log(hit.transform.parent.GetComponent<O_TileInfoContainer>().thisInfo.tileType);
                    hit.transform.parent.GetComponent<O_TileInteraction>().OnClicked();
                }
            }
        }
    }

    private void PCRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.tag == "Tile") M_Tile.Instance.UpdateTargetingTile(hit.transform.parent);
        }
        else M_Tile.Instance.UpdateTargetingTile(null);

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "Tile")
                {
                    Debug.Log(hit.transform.parent.GetComponent<O_TileInfoContainer>().thisInfo.tileType);
                    hit.transform.parent.GetComponent<O_TileInteraction>().OnClicked();
                }
            }
        }
    }
}
