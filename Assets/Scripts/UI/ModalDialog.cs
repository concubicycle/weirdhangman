
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ModalDialog : NonPersistentSingleton<ModalDialog>
    {
        public Text DialogText;
        public Image DialogImage;
        public GameObject dialog;

        
        [SerializeField]
        private Button _yes;

        [SerializeField]
        private Button _no;

        [SerializeField]
        private Button _ok;

        [SerializeField]
        private Button _cancel;

        public string CancelButtonText
        {
            get { return _cancel.GetComponent<GUIText>().text; }
            set { _cancel.GetComponent<GUIText>().text = value; }
        }

        public void Show(string text,
            UnityAction yesAction,
            UnityAction noAction,
            UnityAction okAction,
            UnityAction cancelAction)
        {
            if (yesAction != null)
            {
                _yes.onClick.RemoveAllListeners();
                _yes.onClick.AddListener(() => { 
                    yesAction();
                    Hide();
                });
            }
            else
                _yes.gameObject.SetActive(false);

            if (noAction != null)
            {
                _no.onClick.RemoveAllListeners();
                _no.onClick.AddListener(() => {
                    noAction();
                    Hide();
                });
            }
            else
                _no.gameObject.SetActive(false);

            if (okAction != null)
            {
                _ok.onClick.RemoveAllListeners();
                _ok.onClick.AddListener(() => {
                    okAction();
                    Hide();
                });
            }
            else
                _ok.gameObject.SetActive(false);

            if (cancelAction != null)
            {
                _cancel.onClick.RemoveAllListeners();
                _cancel.onClick.AddListener(() => {
                    cancelAction();
                    Hide();
                });
            }
            else
                _cancel.gameObject.SetActive(false);


            DialogText.text = text;
            dialog.gameObject.SetActive(true);
        }

        private static void Hide()
        {
            Instance.dialog.gameObject.SetActive(false);
        }        
    }
}
