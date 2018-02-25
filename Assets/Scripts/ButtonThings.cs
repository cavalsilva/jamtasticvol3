using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonThings : MonoBehaviour
{
    Transform _transform;
    Button _btn;

    private void Start()
    {
        _transform = GetComponent<Transform>();

        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() =>
        {
            SFXController.Instance.PlaySoundClick();

            _transform.DOScale(1.05f, 0.25f).SetEase(Ease.OutElastic).OnComplete(() =>
            {
                _transform.DOScale(1f, 0f).Play();
            }).Play();
        });
    }
}