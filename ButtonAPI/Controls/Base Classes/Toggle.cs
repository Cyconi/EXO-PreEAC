﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using xButtonAPI.WingAPI.RawUI;
using xButtonAPI.WingAPI;

namespace xButtonAPI.Controls.Base_Classes
{
    public class Toggle : ControlWithText
    {
        public UnityEngine.UI.Toggle toggle => gameObject?.GetComponentInChildren<UnityEngine.UI.Toggle>(true);

        [Obsolete]
        public UnityEngine.UI.Toggle buttontoggle => toggle;

        public UiToggleTooltip tooltip => gameObject?.GetComponentInChildren<UiToggleTooltip>(true);

        public Image OnImage => transform?.Find("Icon_On")?.GetComponentInChildren<Image>(true);
        public Image OffImage => transform?.Find("Icon_Off")?.GetComponentInChildren<Image>(true);

        public bool ToggleState => toggle?.isOn ?? false;

        public bool NextState = false;
        public Action<bool> TglAction;
        public bool NextIsInvoke = false;

        public void SetAction(Action<bool> newAction)
        {
            toggle.onValueChanged = new UnityEngine.UI.Toggle.ToggleEvent();
            toggle.onValueChanged.AddListener((Action<bool>)delegate (bool val)
            {
                newAction?.Invoke(val);
            });
        }

        public void SetInteractable(bool val)
        {
            toggle.interactable = val;
        }

        public bool AllowUserInvoke = true;

        public void SetToggleState(bool newState, bool invoke = false)
        {
            NextState = newState;
            NextIsInvoke = invoke;

            if (gameObject.active)
            {
                AllowUserInvoke = false;

                toggle.isOn = newState;

                AllowUserInvoke = true;

                if (tooltip != null)
                    tooltip.field_Private_Boolean_1 = !newState;

                if (invoke)
                    toggle.onValueChanged.Invoke(newState);
            }
        }

        public bool ToolTipOne = false;
        public void SetTooltip(string newOffTooltip, string newOnTooltip)
        {
            if (!ToolTipOne)
            {
                tooltip.field_Public_String_0 = newOnTooltip;
                tooltip.field_Public_String_1 = newOffTooltip;
            }
            else
            {
                tooltip.field_Public_String_0 = newOffTooltip;
                tooltip.field_Public_String_1 = newOnTooltip;
            }
        }

        public void SetOnIcon(Sprite newIcon)
        {
            if (newIcon == null)
            {
                OnImage.gameObject.SetActive(false);
                return;
            }

            OnImage.sprite = newIcon;
            OnImage.overrideSprite = newIcon;
            OnImage.gameObject.SetActive(true);
        }

        public void SetOffIcon(Sprite newIcon)
        {
            if (newIcon == null)
            {
                OffImage.gameObject.SetActive(false);
                return;
            }

            OffImage.sprite = newIcon;
            OffImage.overrideSprite = newIcon;
            OffImage.gameObject.SetActive(true);
        }
    }
}
