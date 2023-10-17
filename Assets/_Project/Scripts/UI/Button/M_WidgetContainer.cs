using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WidgetContainer : MonoBehaviour
{
    public static M_WidgetContainer instance;

    public RectTransform panel_LandPot;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
