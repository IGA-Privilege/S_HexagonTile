using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    public GameObject pre_Level;
    public SO_Level[] levels;

    public float radius = 5;
    public int currentLevel = 1;
    public float rotateTime = 0.4f;
    private float splitAngle;

    public SpriteRenderer bg;
    public TMP_Text text1;
    public TMP_Text text2;

    private void Start()
    {
        InitializeLevels();
        RotateToTargetLevel();
    }

    private void InitializeLevels()
    {
        splitAngle = 360.0f / levels.Length;
        for (int i = 0; i < levels.Length; i++)
        {
            float angle = i * splitAngle;
            float radians = angle * Mathf.Deg2Rad;
            Vector3 spawnPosition = new Vector3(Mathf.Cos(radians) * radius, 0, Mathf.Sin(radians) * radius);

            Transform newLevel = Instantiate(pre_Level, spawnPosition, Quaternion.identity).transform;
            // 调整旋转使其正面朝外（与圆心反向）
            Vector3 lookAtPosition = -spawnPosition;
            newLevel.LookAt(lookAtPosition);
            //同步Level信息
            newLevel.Find("Image").GetComponent<SpriteRenderer>().sprite = levels[i].img_Tile;
            newLevel.Find("Name").GetComponent<TMP_Text>().text = levels[i].levelName;
            newLevel.Find("Index").GetComponent<TMP_Text>().text = levels[i].levelIndex.ToString();

            newLevel.SetParent(transform);
        }
        transform.position = new Vector3(0, 0, radius);
    }

    public void OnLevelReduce()
    {
        currentLevel--;
        if (currentLevel < 1) currentLevel = 12;
        RotateToTargetLevel();
    }

    public void OnLevelIncrease()
    {
        currentLevel++;
        if (currentLevel > 12) currentLevel = 1;
        RotateToTargetLevel();
    }

    public void LoadLevel()
    {
        M_Global.Instance.EnterSwitchScene(currentLevel+2);
    }

    public void ReturnToMain()
    {
        M_Global.Instance.EnterSwitchScene(0);
    }

    private void RotateToTargetLevel()
    {
        transform.DORotate(new Vector3(0, 90 + currentLevel * splitAngle, 0), rotateTime);

        DOTween.To(() => text1.color, x => text1.color = x, levels[currentLevel].bgColor, rotateTime);
        DOTween.To(() => text2.color, x => text2.color = x, levels[currentLevel].bgColor, rotateTime);
        DOTween.To(() => bg.color, x => bg.color = x, levels[currentLevel].bgColor, rotateTime);
    }
}
