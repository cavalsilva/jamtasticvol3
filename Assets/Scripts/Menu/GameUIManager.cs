using DG.Tweening;
using jamtasticvol3.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jamtasticvol3.Menu
{
    public class GameUIManager : Singleton<GameUIManager>
    {
        public event Delegates.SimpleEvent OnDialogOpen, OnDialogClose;

        public RectTransform dialogContainer;

        public void OpenDialog(Delegates.SimpleEvent callback = null)
        {
            if (OnDialogOpen != null)
                OnDialogOpen();

            dialogContainer.DOAnchorPosX(0f, dialogContainer.anchoredPosition.x != 0f ? 1.5f : 0f).SetEase(Ease.OutBack, 1f).OnComplete(() => { if (callback != null) callback(); }).Play();
        }

        public void CloseDialog()
        {
            if (OnDialogClose != null)
                OnDialogClose();

            dialogContainer.DOAnchorPosX(465f, 0.5f).Play();
        }
    }
}