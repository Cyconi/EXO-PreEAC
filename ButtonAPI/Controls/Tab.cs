using System;
using xButtonAPI.Misc;
using xButtonAPI.Pages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;

namespace xButtonAPI.Controls
{
	public class Tab : Base_Classes.GenericControl
	{
        public readonly MenuTab menuTab;

        public readonly Image tabIcon;

        public readonly GameObject badgeGameObject;

        public readonly TextMeshProUGUI badgeText;

		public Tab(string menu, string tooltip, Sprite icon = null, Transform parent = null)
        {
            if (parent == null)
            {
                parent = xButtonAPI.menuTabBase.transform.parent;
            }

            gameObject = UnityEngine.Object.Instantiate(xButtonAPI.menuTabBase, parent);
            gameObject.name = menu + "Tab";
            menuTab = gameObject.GetOrAddComponent<MenuTab>();
            menuTab.field_Private_MenuStateController_0 = xButtonAPI.GetMenuStateControllerInstance();
            menuTab.field_Public_String_0 = menu;
            tabIcon = gameObject.transform.Find("Icon").GetOrAddComponent<Image>();
            tabIcon.sprite = icon;
            tabIcon.overrideSprite = icon;
            badgeGameObject = gameObject.transform.GetChild(0).gameObject;
            badgeText = badgeGameObject.GetComponentInChildren<TextMeshProUGUI>();
            menuTab.gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetOrAddComponent<Button>();
            menuTab.gameObject.GetOrAddComponent<Button>().onClick.AddListener((Action)delegate
            {
                menuTab.gameObject.GetOrAddComponent<StyleElement>().field_Private_Selectable_0 = menuTab.gameObject.GetOrAddComponent<Button>();

                var tooltipObj = menuTab.gameObject.GetOrAddComponent<VRC.UI.Elements.Tooltips.UiTooltip>();

                if (!string.IsNullOrEmpty(tooltip))
                {
                    tooltipObj.field_Public_String_0 = tooltip;
                }
                else
                {
                    tooltipObj.enabled = false;
                }
            });
        }

        public Tab(MenuPage menu, string tooltip, Sprite icon = null, Transform parent = null) : this(menu.menuName, tooltip, icon, parent)
        {
        }

		public void SetBadge(bool showing = true, string text = "")
		{
			if (!(badgeGameObject == null) && !(badgeText == null))
			{
				badgeGameObject.SetActive(showing);
				badgeText.text = text;
			}
		}
	}
}
