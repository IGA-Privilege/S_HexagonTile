using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FloatingSeed : MonoBehaviour
{

    //public GameObject[] balls; // �洢С�����
    //public float sphereRadius = 5f; // ���η�Χ�İ뾶
    //private Vector3 sphereCenter = Vector3.zero; // ���η�Χ�����ĵ�

    //private Vector3[] targetPositions; // �洢ÿ��С���Ŀ��λ��
    //public float movementSpeed = 2f; // С���ƶ��ٶ�

    //private void Start()
    //{
    //    balls = new GameObject[transform.childCount];
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        balls[i] = transform.GetChild(i).gameObject;
    //    }
    //    targetPositions = new Vector3[balls.Length];

    //    // ��ʼ��С���Ŀ��λ��
    //    for (int i = 0; i < balls.Length; i++)
    //    {
    //        targetPositions[i] = GetRandomPointInSphere();
    //    }
    //}

    //private void Update()
    //{
    //    // ����ÿ��С���λ��
    //    for (int i = 0; i < balls.Length; i++)
    //    {
    //        Vector3 currentPosition = balls[i].transform.localPosition;
    //        Vector3 targetPosition = targetPositions[i];

    //        // �ƶ�С����Ŀ��λ��
    //        balls[i].transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime);

    //        // ����Ƿ񵽴�Ŀ��λ��
    //        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
    //        {
    //            // ��С�򵽴�Ŀ��λ�ú������趨�µ�Ŀ��λ��
    //            targetPositions[i] = GetRandomPointInSphere();
    //        }
    //    }
    //}

    //// �����η�Χ�ڻ�ȡ���λ��
    //private Vector3 GetRandomPointInSphere()
    //{
    //    Vector3 randomDirection = Random.insideUnitSphere;
    //    randomDirection.Normalize();
    //    return sphereCenter + randomDirection * sphereRadius;
    //}

    public GameObject[] balls; // �洢С�����
    public float sphereRadius = 5f; // ���η�Χ�İ뾶
    private Vector3 sphereCenter = Vector3.zero; // ���η�Χ�����ĵ�

    private Vector3[] targetPositions; // �洢ÿ��С���Ŀ��λ��
    public float movementSpeed = 2f; // С���ƶ��ٶ�
    public float minWaitTime = 1f; // ��С�ȴ�ʱ��
    public float maxWaitTime = 3f; // ���ȴ�ʱ��

    private float[] waitTimes; // �洢ÿ��С��ĵȴ�ʱ��
    private Vector3[] initialPositions; // �洢ÿ��С��ĳ�ʼλ��

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

        // ��ʼ��С���Ŀ��λ�á��ȴ�ʱ��ͳ�ʼλ��
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

                // ʹ��Lerp����ƽ����ֵ
                float t = Mathf.PingPong(Time.time * movementSpeed, 1f);
                Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, t);

                // ���һЩ�������
                newPosition += Random.insideUnitSphere * 0.1f;

                // �ƶ�С��
                balls[i].transform.localPosition = newPosition;

                // ���С��ӽ�Ŀ��λ�ã������趨�µ�Ŀ��λ�ú͵ȴ�ʱ��
                if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
                {
                    targetPositions[i] = GetRandomPointInSphere();
                    waitTimes[i] = Random.Range(minWaitTime, maxWaitTime);
                }
            }
        }
    }

    // �����η�Χ�ڻ�ȡ���λ��
    private Vector3 GetRandomPointInSphere()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.Normalize();
        return sphereCenter + randomDirection * sphereRadius;
    }
}
