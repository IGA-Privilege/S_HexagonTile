using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedMoveState { Random,Circle }
public class M_FloatingSeed : MonoBehaviour
{
    public GameObject[] balls; // 存储小球对象
    public float sphereRadius = 5f; // 球形范围的半径
    private Vector3 sphereCenter = Vector3.zero; // 球形范围的中心点
    private Vector3[] targetPositions; // 存储每个小球的目标位置
    public float movementSpeed = 2f; // 小球移动速度
    private Vector3[] initialPositions; // 存储每个小球的初始位置
    private SeedMoveState moveState = SeedMoveState.Random;
    private Transform targetCenter;

    private void Start()
    {
        InitializeSeedPosition();
    }

    private void Update()
    {
        switch (moveState)
        {
            case SeedMoveState.Random:
                SeedRandomMovement();
                break;
            case SeedMoveState.Circle:
                break;
        }
    }

    private void InitializeSeedPosition()
    {
        balls = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            balls[i] = transform.GetChild(i).gameObject;

        targetPositions = new Vector3[balls.Length];
        initialPositions = new Vector3[balls.Length];

        // 初始化小球的目标位置、等待时间和初始位置
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.localPosition = GetRandomPointInSphere();
            initialPositions[i] = balls[i].transform.position;
            targetPositions[i] = GetRandomPointInSphere();
        }
    }

    private void SeedRandomMovement()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            Vector3 currentPosition = balls[i].transform.localPosition;
            Vector3 targetPosition = targetPositions[i];
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * movementSpeed);
            balls[i].transform.localPosition = newPosition;

            if (Vector3.Distance(currentPosition, targetPosition) < 0.1f) targetPositions[i] = GetRandomPointInSphere();
        }
    }
  
    private Vector3 GetRandomPointInSphere()  // 在球形范围内获取随机位置
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.Normalize();
        return sphereCenter + randomDirection * sphereRadius;
    }
}
