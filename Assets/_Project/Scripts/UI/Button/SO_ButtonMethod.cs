using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Jahaha.UI
{
    [CreateAssetMenu(fileName = "New UI Method", menuName = "Jahaha/UI/New UI Method")]
    public class SO_ButtonMethod : ScriptableObject
    {
        public enum ButtonType { Sun, Rain, Monsoon, Updraft ,Scan,Setting,PullOut}
        public ButtonType type;
        public int residueNumber;
        public Sprite buttonIcon;
        public Sprite buttonBG;

        public UnityAction GetCertainMethod()
        {
            //Debug.Log("Get Certain Method");
            switch (type)
            {
                case ButtonType.Sun:
                    return Excute_Sun;
                case ButtonType.Rain:
                    return Excute_Rain;
                case ButtonType.Monsoon:
                    return Excute_Monsoon;
                case ButtonType.Updraft:
                    return Excute_Updraft;
                case ButtonType.Scan:
                    return Excute_Scan;
                case ButtonType.Setting:
                    return Excute_Setting;
                case ButtonType.PullOut:
                    return Excute_PullOut;
                default: return null;
            }
        }

        private void Excute_Sun()
        {
            Debug.Log("Sun");
        }

        private void Excute_Rain()
        {
            Debug.Log("Rain");
        }

        private void Excute_Monsoon()
        {
            Debug.Log("Monsoon");
        }

        private void Excute_Updraft()
        {
            Debug.Log("Updraft");
        }

        private void Excute_Setting()
        {
            Debug.Log("Setting");
        }

        private void Excute_Scan()
        {
            Debug.Log("Scan");
        }

        private void Excute_PullOut()
        {
            RectTransform panelRect = M_WidgetContainer.instance.panel_LandPot;
            float outX = - panelRect.rect.width / 2;
            float innerX = panelRect.rect.width / 2;
            if (panelRect.anchoredPosition.x == outX)
                DOTween.To(() => panelRect.anchoredPosition, x => panelRect.anchoredPosition = x, new Vector2(innerX, panelRect.anchoredPosition.y), 1);
            if (panelRect.anchoredPosition.x == innerX)
                DOTween.To(() => panelRect.anchoredPosition, x => panelRect.anchoredPosition = x, new Vector2(outX, panelRect.anchoredPosition.y), 1);
        }
    }
}