﻿using System;
using System.Collections;
using MelonLoader;
using xButtonAPI.Controls.Grouping;
using xButtonAPI.Misc;
using xButtonAPI.Pages;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;

namespace xButtonAPI.Controls
{
    public class Label
    {
        public readonly SimpleSingleButton LabelButton;

        public Label(Transform parent, string text, string tooltip, Action onClick = null, bool Bg = false)
        {
            LabelButton = new SimpleSingleButton(parent, text, tooltip, onClick);
            LabelButton.transform.Find("Background").gameObject.active = Bg;
            LabelButton.buttonBackground.color = new Color(0f, 0f, 0f, 0f);

            var Handler = LabelButton.gameObject.AddComponent<ObjectHandler>();

            Handler.OnUpdateEachSecond += (a, b) =>
            {
                LabelButton.text.transform.localPosition = new Vector3(0f, -19f, 0f);
            };

            if (onClick == null)
                LabelButton.gameObject.GetOrAddComponent<Button>().enabled = false;
            
        }

        public Label(MenuPage pge, string text, string tooltip, Action onClick = null, bool Bg = false)
            : this(pge.menuContents, text, tooltip, onClick)
        {
        }

        public Label(ButtonGroup grp, string text, string tooltip, Action onClick = null, bool Bg = false)
            : this(grp.gameObject.transform, text, tooltip, onClick)
        {
        }

        public Label(CollapsibleButtonGroup grp, string text, string tooltip, Action onClick = null, bool Bg = false)
            : this(grp.buttonGroup, text, tooltip, onClick)
        {
        }
    }
}