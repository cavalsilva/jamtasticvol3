using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace jamtasticvol3.Menu
{
    public class ButtonToggleDialog : MonoBehaviour
    {
        Button _btn;
        Image _image;
        bool _dialogOpen = true;

        public Sprite closeImg, openImg;

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
                _image.sprite = closeImg;
            };

            GameUIManager.Instance.OnDialogClose += () =>
            {
                _dialogOpen = false;
                _image.sprite = openImg;
            };

            _image = GetComponent<Image>();
        }
    }
}