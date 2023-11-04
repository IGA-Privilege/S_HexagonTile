using DG.Tweening;
using MoreMountains.FeedbacksForThirdParty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    public float selfRotatingSpeed = 1f;
    [Header("Circle Move")]
    private Transform targetCenter;
    public float radius = 2.0f; // �뾶
    public float rotationSpeed = 30.0f; // ��ת�ٶ�
    private float recordAngle = 0.0f;
    private float pos_DefaultY;

    private void Start()
    {
        InitializeSeedPosition();
        pos_DefaultY = transform.position.y;
        SpawnSeed();
    }

    private void Update()
    {
        switch (moveState)
        {
            case SeedMoveState.Random:
                SeedRandomMovement();
                break;
            case SeedMoveState.Circle:
                SeedCircleMovement();
                break;
        }
        SeedSelfRotating();
    }

    public Transform TargetCenter { get { return targetCenter; } }

    private void SpawnSeed()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.localScale = Vector3.zero;
            balls[i].transform.DOScale(1, 0.4f);
        }
    }

    public void PlantSeed()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.DOScale(0, 0.5f);
            balls[i].transform.DOMoveY(balls[i].transform.position.y - 1f, 0.5f);
            balls[i].transform.Find("Trail").gameObject.SetActive(false);
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

    private void SeedCircleMovement()
    {
        float splitAngle = 360 / balls.Length;

        for (int i = 0; i < balls.Length; i++)
        {
            //float x = targetCenter.position.x + Mathf.Cos(recordAngle * splitAngle * i) * radius;
            //float z = targetCenter.position.z + Mathf.Sin(recordAngle * splitAngle * i) * radius;
            //float y = targetCenter.position.y; // ���Ҫ����y����

            //// �����������λ��
            //Vector3 currentPosition = balls[i].transform.localPosition;
            //Vector3 targetPosition = new Vector3(x, y, z);
            //Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * movementSpeed);
            //balls[i].transform.localPosition = newPosition;

            //// ���ӽǶ��Խ�����һ֡����ת
            //recordAngle += rotationSpeed * Time.deltaTime;
            //// ʹ�Ƕȱ�����0��360��֮��
            //if (recordAngle >= 360.0f) recordAngle = 0.0f;

            //balls[i].transform.RotateAround(targetCenter.position, Vector3.up, 2);

            float angleInDegrees = splitAngle * (i + 1);
            // ���Ƕ�ת��Ϊ����
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            // ʹ��Mathf.Cos��Mathf.Sin����������������x��y����
            float x = Mathf.Cos(angleInRadians);
            float y = Mathf.Sin(angleInRadians);
            // ��������
            Vector2 vector = new Vector2(x, y);

            Vector3 targetLP = new Vector3(vector.x * 3.5f, 0, vector.y * 3.5f);
            Vector3 currentLP = balls[i].transform.localPosition;

            Vector3 newLP = Vector3.Lerp(currentLP, targetLP, Time.deltaTime * movementSpeed);
            balls[i].transform.localPosition = newLP;
            //Vector2 lookDir = mousePos - rb.position; 
            //float angle = Mathf.Atan2(lookDir.y, lookDir, x) * Mathf.Rad2Deg;
        }

        transform.Rotate(Vector3.up, 0.3f);

        Vector3 newParentPos = Vector3.Lerp(transform.position, targetCenter.position, Time.deltaTime * movementSpeed*10);
        transform.position = newParentPos;
    }

    private void SeedSelfRotating()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.Rotate(Vector3.up, selfRotatingSpeed);
        }
    }
  
    public void EnterCircleMovement(Transform _targetCenter)
    {
        Debug.Log(_targetCenter.name);
        targetCenter = _targetCenter;
        recordAngle = 0;
        moveState = SeedMoveState.Circle;
    }

    public void ExitCircleMovement()
    {
        targetCenter = null;
        recordAngle = 0;
        moveState = SeedMoveState.Random;
        transform.DOMoveY(pos_DefaultY, 0.4f);
    }

    public bool CheckIsSeedAttachedThis(Transform _FromElement)
    {
        if (_FromElement == targetCenter) return true;
        else return false;
    }

    private Vector3 GetRandomPointInSphere()  // �����η�Χ�ڻ�ȡ���λ��
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.Normalize();
        return sphereCenter + randomDirection * sphereRadius;
    }
}
