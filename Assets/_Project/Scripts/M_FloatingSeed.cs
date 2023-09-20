using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FloatingSeed : MonoBehaviour
{

    //public GameObject[] balls; // 存储小球对象
    //public float sphereRadius = 5f; // 球形范围的半径
    //private Vector3 sphereCenter = Vector3.zero; // 球形范围的中心点

    //private Vector3[] targetPositions; // 存储每个小球的目标位置
    //public float movementSpeed = 2f; // 小球移动速度

    //private void Start()
    //{
    //    balls = new GameObject[transform.childCount];
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        balls[i] = transform.GetChild(i).gameObject;
    //    }
    //    targetPositions = new Vector3[balls.Length];

    //    // 初始化小球的目标位置
    //    for (int i = 0; i < balls.Length; i++)
    //    {
    //        targetPositions[i] = GetRandomPointInSphere();
    //    }
    //}

    //private void Update()
    //{
    //    // 更新每个小球的位置
    //    for (int i = 0; i < balls.Length; i++)
    //    {
    //        Vector3 currentPosition = balls[i].transform.localPosition;
    //        Vector3 targetPosition = targetPositions[i];

    //        // 移动小球向目标位置
    //        balls[i].transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime);

    //        // 检查是否到达目标位置
    //        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
    //        {
    //            // 当小球到达目标位置后，重新设定新的目标位置
    //            targetPositions[i] = GetRandomPointInSphere();
    //        }
    //    }
    //}

    //// 在球形范围内获取随机位置
    //private Vector3 GetRandomPointInSphere()
    //{
    //    Vector3 randomDirection = Random.insideUnitSphere;
    //    randomDirection.Normalize();
    //    return sphereCenter + randomDirection * sphereRadius;
    //}

    public GameObject[] balls; // 存储小球对象
    public float sphereRadius = 5f; // 球形范围的半径
    private Vector3 sphereCenter = Vector3.zero; // 球形范围的中心点

    private Vector3[] targetPositions; // 存储每个小球的目标位置
    public float movementSpeed = 2f; // 小球移动速度
    public float minWaitTime = 1f; // 最小等待时间
    public float maxWaitTime = 3f; // 最大等待时间

    private float[] waitTimes; // 存储每个小球的等待时间
    private Vector3[] initialPositions; // 存储每个小球的初始位置

    private void Start()
    {
        balls = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            balls[i] = transform.GetChild(i).gameObject;
        }
        targetPositions = new Vector3[balls.Length];

        targetPositions = new Vector3[balls.Length];
        waitTimes = new float[balls.Length];
        initialPositions = new Vector3[balls.Length];

        // 初始化小球的目标位置、等待时间和初始位置
        for (int i = 0; i < balls.Length; i++)
        {
            targetPositions[i] = GetRandomPointInSphere();
            waitTimes[i] = Random.Range(minWaitTime, maxWaitTime);
            initialPositions[i] = balls[i].transform.position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            waitTimes[i] -= Time.deltaTime;

            if (waitTimes[i] <= 0f)
            {
                Vector3 currentPosition = balls[i].transform.localPosition;
                Vector3 targetPosition = targetPositions[i];

                // 使用Lerp进行平滑插值
                float t = Mathf.PingPong(Time.time * movementSpeed, 1f);
                Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, t);

                // 添加一些随机浮动
                newPosition += Random.insideUnitSphere * 0.1f;

                // 移动小球
                balls[i].transform.localPosition = newPosition;

                // 如果小球接近目标位置，重新设定新的目标位置和等待时间
                if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
                {
                    targetPositions[i] = GetRandomPointInSphere();
                    waitTimes[i] = Random.Range(minWaitTime, maxWaitTime);
                }
            }
        }
    }

    // 在球形范围内获取随机位置
    private Vector3 GetRandomPointInSphere()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.Normalize();
        return sphereCenter + randomDirection * sphereRadius;
    }
}
