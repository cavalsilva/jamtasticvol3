using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace jamtasticvol3.Menu
{
    public class ButtonToggleDialog : MonoBehaviour
    {
        Button _btn;
        public Image image;
        bool _dialogOpen = true;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(() =>
            {
                if(_dialogOpen)
                    GameUIManager.Instance.CloseDialog();
                else
                    GameUIManager.Instance.OpenDialog();
            });

            GameUIManager.Instance.OnDialogOpen += () =>
            {
                _dialogOpen = true;

                Color c = image.color;
                c.a = 0.8f;
                image.color = c;
            };

            GameUIManager.Instance.OnDialogClose += () =>
            {
                _dialogOpen = false;

                Color c = image.color;
                c.a = 1f;
                image.color = c;
            };
        }
    }
}