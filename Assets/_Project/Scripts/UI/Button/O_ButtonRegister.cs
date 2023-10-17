using Jahaha.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class O_ButtonRegister : MonoBehaviour
{
    public SO_ButtonMethod targetMethod;

    protected virtual void Start()
    {
        RemoveButtonListen();
        AddButtonListen(targetMethod.GetCertainMethod());
        SyncIconImage();
    }

    protected void AddButtonListen(UnityAction action) //Button¼àÌýÊÂ¼þ
    {
        Button tmpButton = transform.GetComponent<Button>();
        if (tmpButton != null)
        {
            tmpButton.onClick.AddListener(action);
        }
    }

    protected void RemoveButtonListen()
    {
        Button tmpButton = transform.GetComponent<Button>();
        if (tmpButton != null)
        {
            tmpButton.onClick.RemoveAllListeners();
        }
    }

    private void SyncIconImage()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = targetMethod.buttonIcon;
    }
}
