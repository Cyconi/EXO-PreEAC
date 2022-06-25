using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ConsoleLogger;
using UnityEngine.UI;

namespace xButtonAPI.Controls
{
    public class HalfButton : Base_Classes.Button
    {
        private static Vector3 FloorBase = new Vector3(0f, 0f, 0f);

        public static GameObject SingleHalfButton(GameObject menu, string text, string tooltip, Action action, Vector3 Posz, Sprite Icon = null, HalfType Type = HalfType.Normal, bool IsGroup = false)
        {
            var instanciated = GameObject.Instantiate(xButtonAPI.singleButtonBase, menu.transform).gameObject;
            instanciated.name = $"Button_{text}";
            var buttoni = instanciated.GetComponent<UnityEngine.UI.Button>();
            buttoni.onClick.RemoveAllListeners();
            buttoni.onClick.AddListener(new Action(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception r)
                {
                    CLog.E($"ERROR With Btn {text} : : {r}");
                }
                CLog.L($"Pressed on [{text}]");
            }));
            instanciated.transform.Find("Text_H4").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
            instanciated.transform.Find("Text_H4").gameObject.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 22;
            if (!string.IsNullOrEmpty(tooltip))
                instanciated.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>().ToList().ForEach(s => s.field_Public_String_0 = tooltip);
            else
                instanciated.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>().ToList().ForEach(s => s.enabled = false);
            GameObject Img = instanciated.transform.Find("Icon").gameObject;
            if (Icon != null)
                Img.GetComponent<Image>().sprite = Icon;
            else
                Img.SetActive(false);

            instanciated.transform.rotation = new Quaternion(0, 0, 0, 0);
            instanciated.transform.localPosition = Posz;

            if (IsGroup)
                instanciated.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -115);
            else
                instanciated.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -80);

            switch (Type)
            {
                case HalfType.Top:
                    Img.transform.localPosition = new Vector3(0f, 0f, 0f);
                    instanciated.transform.Find("Text_H4").transform.localPosition = new Vector3(0, 22, 0);
                    instanciated.transform.Find("Background").transform.localPosition = new Vector3(0, 53, 0);
                    break;
                case HalfType.Normal:
                    Img.transform.localPosition = new Vector3(0f, 0f, 0f);
                    instanciated.transform.Find("Text_H4").transform.localPosition = new Vector3(0, -22, 0);
                    break;
                case HalfType.Bottom:
                    Img.transform.localPosition = new Vector3(0f, 0f, 0f);
                    instanciated.transform.Find("Text_H4").transform.localPosition = new Vector3(0, -69.9f, 0);
                    instanciated.transform.Find("Background").transform.localPosition = new Vector3(0, -53, 0);
                    break;
            }

            return instanciated;
        }

        public static GameObject SingleHalfToggle(GameObject menu, string text, string Ontooltip, string Offtooltip, Action<bool> BoolStateChange, Vector3 TogglePoz, Sprite OnImageSprite = null, Sprite OffImageSprite = null, float FontSize  = 24f, bool BaseState = false)
        {
            var ToggleObj = GameObject.Instantiate(xButtonAPI.toggleButtonBase, menu.transform).gameObject;
            ToggleObj.name = $"Toggle_{text}";
            ToggleObj.transform.rotation = new Quaternion(0, 0, 0, 0);
            ToggleObj.transform.localPosition = TogglePoz;
            ToggleObj.GetComponent<UIInvisibleGraphic>().enabled = false;

            var OnImage = ToggleObj.transform.Find("Icon_On").gameObject;
            OnImage.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
            OnImage.transform.localPosition = new Vector3(-52.22f, 30.18f, 0f);
            if (OnImageSprite != null)
                OnImage.GetComponent<Image>().sprite = OnImageSprite;
            else
                OnImage.GetComponent<Image>().sprite = xButtonAPI.onIconSprite;
            OnImage.SetActive(BaseState);

            var OffImage = ToggleObj.transform.Find("Icon_Off").gameObject;
            OffImage.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
            OffImage.transform.localPosition = new Vector3(-52.22f, 30.18f, 0f);
            if (OffImageSprite != null)
                OffImage.GetComponent<Image>().sprite = OffImageSprite;
            OffImage.SetActive(!BaseState);


            var buttoni = ToggleObj.GetComponent<UnityEngine.UI.Toggle>();
            buttoni.onValueChanged.RemoveAllListeners();
            buttoni.isOn = BaseState;
            buttoni.onValueChanged.AddListener(new Action<bool>(val => {
                try {
                    BoolStateChange.Invoke(val);
                    OffImage.active = !val;
                    OnImage.active = val;
                }
                catch (Exception r) {
                    CLog.E($"ERROR With Toggle {text} : : {r}");
                }
                CLog.L($"Pressed on [{text}]");
            }));

            if (!string.IsNullOrEmpty(Ontooltip)) {
                ToggleObj.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>().field_Public_String_1 = Ontooltip;
                ToggleObj.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>().field_Public_String_0 = Offtooltip;
            }
            else
                ToggleObj.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>().enabled = false;

            if (string.IsNullOrEmpty(Offtooltip))
                ToggleObj.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>().field_Public_String_0 = Ontooltip; // meh, why not

            ToggleObj.transform.Find("Text_H4").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
            ToggleObj.transform.Find("Text_H4").gameObject.GetComponent<TMPro.TextMeshProUGUI>().fontSize = FontSize;
            ToggleObj.transform.Find("Text_H4").transform.localPosition = new Vector3(34.42f, -22, 0);
            ToggleObj.transform.Find("Text_H4").GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 50f);
            ToggleObj.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -80);

            return ToggleObj;
        }

        public static GameObject DuoHalfToggle(GameObject menu, string text, string Ontooltip, string OffTooltip, Action<bool> BoolStateChange, string text2, string Ontooltip2, string OffTooltip2, Action<bool> BoolStateChange2, Vector3 TogglePoz, Sprite OnImageSprite = null, Sprite OffImageSprite = null, float FirstFontSize = 24f, float SecondFontSize = 24f, bool FirstState = false, bool SecondState = false)
        {
            GameObject Base = new GameObject();
            Base.name = $"DuoToggles";
            Base.transform.parent = menu.transform;
            Base.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Base.transform.localScale = new Vector3(1f, 1f, 1f);
            Base.transform.localPosition = TogglePoz;
            Base.AddComponent<LayoutElement>();
            GameObject Sub = new GameObject(); // this has a reason! ;3
            Sub.name = $"Toggles_[WorldClient]";
            Sub.transform.parent = Base.transform;
            Sub.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Sub.transform.localScale = new Vector3(1f, 1f, 1f);
            Sub.transform.localPosition = new Vector3(0f, -3f, 0f);

            HalfButton.SingleHalfToggle(Sub.gameObject, text, Ontooltip, OffTooltip, BoolStateChange, new Vector3(0f, 50f, 0f), OnImageSprite, OffImageSprite, FirstFontSize, FirstState);
            HalfButton.SingleHalfToggle(Sub.gameObject, text2, Ontooltip2, OffTooltip2, BoolStateChange2, new Vector3(0f, -51f, 0f), OnImageSprite, OffImageSprite, SecondFontSize, SecondState);
            return Base;
        }

        public static GameObject GroupOfHalfButtons(GameObject menu, string FirstName, string FirstTooltip, Action action, Vector3 Posz, string SecondName = null, string SecondTooltip = null, Action Secondaction = null, string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
        {
            GameObject Base = new GameObject();
            Base.name = $"GroupOfHalfButtons";
            Base.transform.parent = menu.transform;
            Base.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Base.transform.localScale = new Vector3(1f, 1f, 1f);
            Base.transform.localPosition = Posz;
            Base.AddComponent<LayoutElement>();
            GameObject Sub = new GameObject(); // this has a reason! ;3
            Sub.name = $"Buttons_[WorldClient]";
            Sub.transform.parent = Base.transform;
            Sub.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Sub.transform.localScale = new Vector3(1f, 1f, 1f);
            Sub.transform.localPosition = new Vector3(0f, -3f, 0f);


            SingleHalfButton(Sub, FirstName, FirstTooltip, action, new Vector3(0f, 10.7f, 0), null, HalfType.Top, true);


            if (Secondaction != null)
                SingleHalfButton(Sub, SecondName, SecondTooltip, Secondaction, new Vector3(0f, -1.36f, 0), null, HalfType.Normal, true);


            if (!string.IsNullOrEmpty(thirdName))
                SingleHalfButton(Sub, thirdName, thirdTooltip, thirdaction, new Vector3(0f, -13.88f, 0), null, HalfType.Bottom, true);


            return Base;
        }
        // ^^^ i redid this like, 3 times, be happy i put a lot of time into my code and wasn't lazy ;3 (this was worse ._.) ^^^
        // Ex of the above code 
            //HalfButton.GroupOfHalfButtons(MainSelItems.gameObject, "", "", delegate
            //{
            //
            //}, new Vector3(0f, 0f, 0f), "", "", delegate
            //{
            //
            //}, "", "", delegate
            //{
            //
            //});

        public enum HalfType
        {
            Top,
            Normal,
            Bottom
        }
    }
}
