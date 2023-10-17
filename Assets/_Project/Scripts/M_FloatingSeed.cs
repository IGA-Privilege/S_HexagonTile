using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedMoveState { Random,Circle }
public class M_FloatingSeed : MonoBehaviour
{
    public GameObject[] balls; // �洢С�����
    public float sphereRadius = 5f; // ���η�Χ�İ뾶
    private Vector3 sphereCenter = Vector3.zero; // ���η�Χ�����ĵ�
    private Vector3[] targetPositions; // �洢ÿ��С���Ŀ��λ��
    public float movementSpeed = 2f; // С���ƶ��ٶ�
    private Vector3[] initialPositions; // �洢ÿ��С��ĳ�ʼλ��
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

        // ��ʼ��С���Ŀ��λ�á��ȴ�ʱ��ͳ�ʼλ��
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
  
    private Vector3 GetRandomPointInSphere()  // �����η�Χ�ڻ�ȡ���λ��
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.Normalize();
        return sphereCenter + randomDirection * sphereRadius;
    }
}
