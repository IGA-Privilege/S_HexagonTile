using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_TileHighLighter : Singleton<O_TileHighLighter>
{
    public float radius;
    public float yOffset;
    private LineRenderer lr;
    

    void Start()
    {
        InitializeLineFrame();
    }

    void InitializeLineFrame()
    {
        float c = radius;
        float b = radius / 2;
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));
     
        Vector3[] nodes = new Vector3[7];
        nodes[0] = new Vector3(0, 0, c);
        nodes[1] = new Vector3(a, 0, b);
        nodes[2] = new Vector3(a, 0, -b);
        nodes[3] = new Vector3(0, 0, -c);
        nodes[4] = new Vector3(-a, 0, -b);
        nodes[5] = new Vector3(-a, 0, b);
        nodes[6] = new Vector3(0, 0, c);

        lr = transform.Find("Hex Frame").GetComponent<LineRenderer>();
        lr.positionCount = nodes.Length;
        lr.SetPositions(nodes);
    }

    public void UpdateTargetingTile(Transform tileTrans)
    {
        if (tileTrans == null) lr.enabled = false;
        else
        {
            transform.position = tileTrans.position + new Vector3(0, yOffset, 0);
            lr.enabled = true;
        }
    }
}
