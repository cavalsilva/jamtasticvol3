using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleBGM : MonoBehaviour
{
    public Sprite bgmOn, bgmOff;

    Button _btn;
    public Image image;
    bool _isOn = true;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() =>
        {
            _isOn = !_isOn;

            MusicController.Instance.ToggleMusic(_isOn);
            image.sprite = _isOn ? bgmOff : bgmOn;
        });
    }
}
