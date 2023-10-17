using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Camera : MonoBehaviour
{
    public bool isArrowKeyControl;
    public float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isArrowKeyControl) CameraMovement();
    }

    void CameraMovement()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(xAxis, yAxis);

        if(direction!=Vector2.zero)
        {
            transform.position += Time.deltaTime * moveSpeed * new Vector3(direction.x, 0, direction.y).normalized;
        }
    }
}
