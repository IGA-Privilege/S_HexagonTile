using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jahaha.MethodList;

public class O_Bar_Dotted : MonoBehaviour
{
    private int value_Max;
    private int value_Current;
    public Action ValueReachZero;
    public Action<int> ValueChange;

    void Start()
    {
        value_Max = transform.childCount;
        value_Current = value_Max;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow)) OnValueDecrease();
        if (Input.GetKeyDown(KeyCode.UpArrow)) OnValueIncrease();
    }

    public void OnValueDecrease()
    {
        value_Current--;
        if (value_Current < 0) value_Current = 0;
        else ML_Scale.Pop(1, 0.8f, 1.1f, 0, transform.GetChild(value_Current), 0.5f);
    }

    public void OnValueIncrease()
    {
        value_Current++;
        if (value_Current > value_Max) value_Current = value_Max;
        else ML_Scale.Pop(0, 1.2f, 0.9f, 1, transform.GetChild(value_Current - 1), 0.5f);
    }

    public bool isEnergyAffluent()
    {
        if (value_Current > 0) return true;
        else return false;
    }

    private void OnValidate()
    {

    }
}