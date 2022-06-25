using MelonLoader.Preferences;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.PopUps
{
    public static class PopupManagerExtensions
    {
        public delegate void ShowAlertDelegate(VRCUiPopupManager popupManager, string title, string body, float timeout);
        public delegate void ShowStandardPopupV2Fn(VRCUiPopupManager popupManager, string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> additionalSetup = null);
        public delegate void ShowStandardPopupV21Fn(VRCUiPopupManager popupManager, string title, string body, string buttonText, Il2CppSystem.Action onClick, Il2CppSystem.Action<VRCUiPopup> additionalSetup = null);

        public static ShowStandardPopupV21Fn _showStandardPopupV21Fn;

        public static ShowAlertDelegate _showAlertDelegate;

        public static ShowAlertDelegate ShowAlertFn
        {
            get
            {
                if (_showAlertDelegate != null)
                    return _showAlertDelegate;

                var showAlertFn = typeof(VRCUiPopupManager).GetMethods().Single(m =>
                {
                    if (m.ReturnType != typeof(void))
                        return false;

                    if (m.GetParameters().Length != 3)
                        return false;

                    return XrefScanner.XrefScan(m).Any(x => x.Type == XrefType.Global && x.ReadAsObject()?.ToString() ==
                        "UserInterface/MenuContent/Popups/AlertPopup");
                });

                _showAlertDelegate = (ShowAlertDelegate)Delegate.CreateDelegate(typeof(ShowAlertDelegate), showAlertFn);

                return _showAlertDelegate;
            }
        }

        public static ShowStandardPopupV21Fn ShowUiStandardPopupV21
        {
            get
            {
                if (_showStandardPopupV21Fn != null)
                {
                    return _showStandardPopupV21Fn;
                }
                var methodInfo = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(it =>
                {
                    if (it.GetParameters().Length == 5 && !it.Name.Contains("PDM"))
                    {
                        return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
                        {
                            if (jt.Type != XrefType.Global) return false;
                            var @object = jt.ReadAsObject();
                            return @object?.ToString() == "UserInterface/MenuContent/Popups/StandardPopupV2";
                        });
                    }
                    return false;
                });
                _showStandardPopupV21Fn = (ShowStandardPopupV21Fn)Delegate.CreateDelegate(typeof(ShowStandardPopupV21Fn), methodInfo);
                return _showStandardPopupV21Fn;
            }
        }

        public static void HideCurrentPopup(this VRCUiPopupManager vrcUiPopupManager)
        {
            VRCUiManagerEx.Instance.HideScreen("POPUP");
        }

        public static void ShowAlert(this VRCUiPopupManager popupManager, string title, string body, float timeout = 0f)
        {
            ShowAlertFn(popupManager, title, body, timeout);
        }

        public delegate void ShowInputPopupWithCancelDelegate(VRCUiPopupManager popupManager, string title,
            string preFilledText,
            InputField.InputType inputType, bool useNumericKeypad, string submitButtonText,
            Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> submitButtonAction,
            Il2CppSystem.Action cancelButtonAction, string placeholderText = "Enter text....", bool hidePopupOnSubmit = true,
            Il2CppSystem.Action<VRCUiPopup> additionalSetup = null, bool param_11 = false, int param_12 = 0);

        public static ShowInputPopupWithCancelDelegate _showInputPopupWithCancelDelegate;

        public static ShowInputPopupWithCancelDelegate ShowInputPopupWithCancelFn
        {
            get
            {
                if (_showInputPopupWithCancelDelegate != null)
                    return _showInputPopupWithCancelDelegate;

                var method = typeof(VRCUiPopupManager).GetMethods().Single(m =>
                {
                    if (!m.Name.StartsWith(
                            "Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_") ||
                        m.Name.Contains("PDM"))
                        return false;

                    return XrefScanner.XrefScan(m).Any(x => x.Type == XrefType.Global && x.ReadAsObject()?.ToString() ==
                        "UserInterface/MenuContent/Popups/InputKeypadPopup");
                });

                _showInputPopupWithCancelDelegate = (ShowInputPopupWithCancelDelegate)Delegate.CreateDelegate(typeof(ShowInputPopupWithCancelDelegate), method);

                return _showInputPopupWithCancelDelegate;
            }
        }

        public static void ShowInputPopupWithCancel(this VRCUiPopupManager popupManager, string title, string preFilledText,
            InputField.InputType inputType, bool useNumericKeypad, string submitButtonText,
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> submitButtonAction,
            Action cancelButtonAction, string placeholderText = "Enter text....", bool hidePopupOnSubmit = true,
            Action<VRCUiPopup> additionalSetup = null)
        {
            ShowInputPopupWithCancelFn(popupManager,
                    title,
                    preFilledText,
                    inputType, useNumericKeypad, submitButtonText, submitButtonAction, cancelButtonAction, placeholderText, hidePopupOnSubmit, additionalSetup);
        }

        public static void ShowStandardPopupV2(this VRCUiPopupManager popupManager, string title, string body, string leftButtonText,
            Action leftButtonAction, string rightButtonText, Action rightButtonAction,
            Action<VRCUiPopup> additonalSetup)
        {
            popupManager.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_0(title, body, leftButtonText, leftButtonAction, rightButtonText, rightButtonAction, additonalSetup);
        }

        public static void ShowStandardPopupV2(this VRCUiPopupManager popupManager, string title, string body, string buttonText,
            Action onClick, Action<VRCUiPopup> onCreated = null)
        {
            ShowUiStandardPopupV21(popupManager, title, body, buttonText, onClick, onCreated);
        }

        public static void ShowInputPopup(string title, string initialText, InputField.InputType inputType, bool isNumeric,
            string confirmButtonText, Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> onComplete, Action onCancel = null,
                string placeholderText = "Enter text...", bool closeAfterInput = true, Action<VRCUiPopup> onPopupShown = null,
                    bool showLimitLabel = false, int textLengthLimit = 0)
                        {
                            ShowUiInputPopup(title, initialText, inputType, isNumeric, confirmButtonText, onComplete, onCancel, placeholderText, closeAfterInput, onPopupShown, showLimitLabel, textLengthLimit);
                        }

        internal delegate void ShowUiInputPopupAction(string title, string initialText, InputField.InputType inputType,
                bool isNumeric, string confirmButtonText, Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> onComplete,
                    Il2CppSystem.Action onCancel, string placeholderText = "Enter text...", bool closeAfterInput = true,
                        Il2CppSystem.Action<VRCUiPopup> onPopupShown = null, bool bUnknown = false, int charLimit = 0);

        private static ShowUiInputPopupAction ourShowUiInputPopupAction;

        internal static ShowUiInputPopupAction ShowUiInputPopup
        {
            get
            {
                if (ourShowUiInputPopupAction != null) return ourShowUiInputPopupAction;

                var candidates = typeof(VRCUiPopupManager)
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(it =>
                        it.Name.StartsWith("Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_")
                        && !it.Name.EndsWith("_PDM"))
                    .ToList();

                var targetMethod = candidates.SingleOrDefault(it => XrefScanner.XrefScan(it).Any(jt =>
                    jt.Type == XrefType.Global &&
                    jt.ReadAsObject()?.ToString() == "UserInterface/MenuContent/Popups/InputPopup"));

                if (targetMethod == null)
                    targetMethod = typeof(VRCUiPopupManager).GetMethod(nameof(VRCUiPopupManager.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0),
                    BindingFlags.Instance | BindingFlags.Public);

                ourShowUiInputPopupAction = (ShowUiInputPopupAction)Delegate.CreateDelegate(typeof(ShowUiInputPopupAction), VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0, targetMethod);

                return ourShowUiInputPopupAction;
            }
        }
    }
    public class VRCUiManagerEx
    {
        public static VRCUiManager _uiManagerInstance;

        public static VRCUiManager Instance
        {
            get
            {
                if (_uiManagerInstance == null)
                {
                    _uiManagerInstance = (VRCUiManager)typeof(VRCUiManager).GetMethods().First(x => x.ReturnType == typeof(VRCUiManager)).Invoke(null, new object[0]);
                }

                return _uiManagerInstance;
            }
        }

        public static bool IsOpen => Instance.field_Private_Boolean_0;
    }
}
