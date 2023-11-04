using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum ButtonType { Sun, Rain, Monsoon, Updraft, Scan, Setting, PullOut, Confirm, Cancel, Boat, Bird, Start, Return, Level }
namespace Jahaha.UI
{
    [CreateAssetMenu(fileName = "New UI Method", menuName = "Jahaha/UI/New UI Method")]
    public class SO_ButtonMethod : ScriptableObject
    {

        public ButtonType type;
        public int residueNumber;
        public Sprite buttonIcon;
        public Sprite buttonBG;
        public static O_ElementBase currentElement;

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
                case ButtonType.Confirm:
                    return Excute_Confirm;
                case ButtonType.Cancel:
                    return Excute_Cancel;
                case ButtonType.Boat:
                    return Excute_Boat;
                case ButtonType.Bird:
                    return Excute_Bird;
                case ButtonType.Start:
                    return Excute_Start;
                case ButtonType.Return:
                    return Excute_Return;
                case ButtonType.Level:
                    return Excute_Level;
                default: return null;
            }
        }

        private void Excute_Sun()
        {
            currentElement = Instantiate(M_Game.instance.pre_Sun).GetComponent<O_ElementBase>();
        }

        private void Excute_Boat()
        {
            currentElement = Instantiate(M_Game.instance.pre_Boat).GetComponent<O_ElementBase>();
        }

        private void Excute_Bird()
        {
            currentElement = Instantiate(M_Game.instance.pre_Bird).GetComponent<O_ElementBase>();
        }

        private void Excute_Rain()
        {
            currentElement = Instantiate(M_Game.instance.pre_Cloud).GetComponent<O_ElementBase>();
        }

        private void Excute_Monsoon()
        {
            currentElement = Instantiate(M_Game.instance.pre_Monsoon).GetComponent<O_ElementBase>();
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
            O_Scanner.Instance.TriggerScanner();
        }

        private void Excute_Confirm()
        {
            Debug.Log("Clicked");
            if (currentElement != null)
            {
                Debug.Log("Confirm");
                currentElement.Set();
                currentElement = null;
            }
            else
            {
                M_Tile.Instance.tile_Targeting.parent.GetComponent<O_TileInteraction>().OnClicked();
            }
        }

        private void Excute_Start()
        {
            M_Global.Instance.EnterSwitchScene(1);
            //SceneManager.LoadScene(1);
        }

        private void Excute_Return()
        {
            M_Global.Instance.EnterSwitchScene(0);
            //SceneManager.LoadScene(0);
        }

        private void Excute_Level()
        {
            M_Global.Instance.EnterSwitchScene(residueNumber);
            //SceneManager.LoadScene(residueNumber);
        }

        private void Excute_Cancel()
        {
            if (currentElement != null)
            {
                Destroy(currentElement.gameObject);
                currentElement = null;
            }
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