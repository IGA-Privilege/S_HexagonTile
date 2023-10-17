using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class o_BR_Consumable : O_ButtonRegister
{
    public int residueNumber;
    private TMPro.TMP_Text txt_Number;

    protected override void Start()
    {
        base.Start();
        residueNumber = targetMethod.residueNumber;
        txt_Number = transform.Find("Number").GetComponent<TMPro.TMP_Text>();
        SyncNumberText();
    }

    void SyncNumberText()
    {
        txt_Number.text = residueNumber.ToString();
    }
}
