using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class O_Scanner : Singleton<O_Scanner>
{
    private bool isScanning = false;
    private RectTransform rect_Frame;
    private RectTransform rect_Line;

    public Vector2 frame_Stretch;
    public Vector2 frame_Shrink;
    public Vector2 line_Stretch;
    public Vector2 line_Shrink;
    public float transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        rect_Frame = GetComponent<RectTransform>();
        rect_Line = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerScanner()
    {
        if(isScanning == false)
        {
            DOTween.To(() => rect_Frame.sizeDelta, x => rect_Frame.sizeDelta = x, frame_Stretch, transitionTime);
            DOTween.To(() => rect_Line.sizeDelta, x => rect_Line.sizeDelta = x, line_Stretch, transitionTime);
            isScanning = true;
        }
        else
        {
            DOTween.To(() => rect_Frame.sizeDelta, x => rect_Frame.sizeDelta = x, frame_Shrink, transitionTime);
            DOTween.To(() => rect_Line.sizeDelta, x => rect_Line.sizeDelta = x, line_Shrink, transitionTime);
            isScanning = false;
        }
    }
}
