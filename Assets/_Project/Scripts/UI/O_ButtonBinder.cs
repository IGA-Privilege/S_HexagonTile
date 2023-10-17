using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class O_ButtonBinder : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public enum ButtonType { Start, Exit, Level1, Level2, Level3, Level4, Tutorial, Pause, Continue }
    public Sprite sprite_Deselected;
    public Sprite sprite_Hovering;
    public ButtonType type;

    public void TriggerButtonEffect()
    {

    }

    public void TriggerOnHovering()
    {
        transform.DOScale(1.15f, 0.3f);
        transform.GetComponent<Image>().sprite = sprite_Hovering;
    }

    public void TriggerOnDeselected()
    {
        transform.DOScale(1f, 0.3f);
        transform.GetComponent<Image>().sprite = sprite_Deselected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerButtonEffect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TriggerOnHovering();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TriggerOnDeselected();
    }
}
