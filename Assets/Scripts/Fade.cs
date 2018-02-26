using DG.Tweening;
using jamtasticvol3.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : Singleton<Fade>
{
    CanvasGroup _cg;

    void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void FadeIn(Delegates.SimpleEvent callback = null)
    {
        _cg.DOFade(1f, 1f).OnComplete(() =>
        {
            if (callback != null)
                callback();

            _cg.DOFade(0f, 1f).Play();
        }).Play();
    }
}