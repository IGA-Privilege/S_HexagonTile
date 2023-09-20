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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    Debug.Log(hit.transform.parent.gameObject.name);
                }
            }
        }
    }

    private void PCRaycastHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "Tile")
                {
                    Debug.Log(hit.transform.parent.gameObject.name);
                    hit.transform.GetComponent<O_Tile>().OnClicked();
                }
            }
        }
    }
}
