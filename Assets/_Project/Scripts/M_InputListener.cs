using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_InputListener : MonoBehaviour
{
    public Transform arCamera;
    public bool isMobile;
    public RaycastHit hit;
    public bool isScreenClickPermited = false;

    void Update()
    {
        if (isMobile) MobileCenterRaycastHit();
        else PCRaycastHit();
    }

    private void MobileCenterRaycastHit()
    {
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Tile") M_Tile.Instance.UpdateTargetingTile(hit);
        }
        else M_Tile.Instance.UpdateTargetingTileToNull();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Tile"&& isScreenClickPermited)
                {
                    hit.transform.parent.GetComponent<O_TileInteraction>().OnClicked();
                }
            }
        }
    }

    private void PCRaycastHit()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        if (Physics.Raycast(Camera.main.transform.position, arCamera.transform.forward, out hit))
        //if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.tag == "Tile") M_Tile.Instance.UpdateTargetingTile(hit);
        }
        else M_Tile.Instance.UpdateTargetingTileToNull();

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, arCamera.transform.forward, out hit))
            //if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "Tile" && isScreenClickPermited)
                {
                    hit.transform.parent.GetComponent<O_TileInteraction>().OnClicked();
                }
            }
        }
    }
}
