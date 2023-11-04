using Jahaha.MethodList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class O_Bar_Regular : MonoBehaviour
{
    public enum BarShinkDirection { Leftwards, Rightwards, Downwards, Upwards }
    public BarShinkDirection shinkDirection;
    private RectMask2D rectMask;
    private float value_Max;
    private float hp_Max = 100;
    private float hp_Current;
    private float hp_PerUnit;

    void Start()
    {
        rectMask = GetComponent<RectMask2D>();
        value_Max = GetComponent<RectTransform>().rect.width;
        hp_Current = hp_Max;
        hp_PerUnit = hp_Max / 5;
        InitializeComponentAndValue();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    OnValueChange(120, 110);
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    OnValueChange(120, 23);
        //}
    }

    private void InitializeComponentAndValue()
    {
        switch (shinkDirection)
        {
            case BarShinkDirection.Leftwards:
                value_Max = GetComponent<RectTransform>().rect.width;
                break;
            case BarShinkDirection.Rightwards:
                value_Max = GetComponent<RectTransform>().rect.width;
                break;
            case BarShinkDirection.Downwards:
                value_Max = GetComponent<RectTransform>().rect.height;
                break;
            case BarShinkDirection.Upwards:
                value_Max = GetComponent<RectTransform>().rect.height;
                break;
        }
    }

    public void OnValueDecrease()
    {
        hp_Current -= hp_PerUnit;
        OnValueChange(hp_Max, hp_Current);
        if (hp_Current <= 0) M_Game.instance.GameFailed();
    }

    private void OnValueChange(float maxValue, float currentValue)
    {
        Vector4 targetVector4 = Vector4.zero;
        float targetValue =value_Max- currentValue/ maxValue * value_Max;
        switch (shinkDirection)
        {
            case BarShinkDirection.Leftwards: targetVector4 = new Vector4(0, 0, targetValue, 0); break;
            case BarShinkDirection.Rightwards: targetVector4 = new Vector4(targetValue, 0, 0, 0); break;
            case BarShinkDirection.Downwards: targetVector4 = new Vector4(0, 0,  0, targetValue); break;
            case BarShinkDirection.Upwards: targetVector4 = new Vector4(0, targetValue, 0, 0); break;
        }
        DOTween.To(() => rectMask.padding, x => rectMask.padding = x, targetVector4, 1);
    }

}
