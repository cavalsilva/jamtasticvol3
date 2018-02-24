using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using jamtasticvol3.Utils;
using DG.Tweening;

namespace jamtasticvol3.DialogSystem
{
    public class DialogEntry : MonoBehaviour
    {
        public event Delegates.IntEvent OnAnswer;

        public Text dialogText;
        public RectTransform answersContainer;
        public GameObject answerPrefab;

        RectTransform _rectTransform;
        CanvasGroup _cv;
        CanvasGroup _answersCV;

        Dialog _dialog;

        public void Init(Dialog dialog)
        {
            _rectTransform = GetComponent<RectTransform>();

            _cv = GetComponent<CanvasGroup>();
            _answersCV = answersContainer.GetComponent<CanvasGroup>();
            _answersCV.alpha = 0f;

            _dialog = dialog;

            StartCoroutine(_AnimatedDialog());
        }

        public void SendAnswer(int callerID, string callDialogID)
        {
            if (OnAnswer != null)
                OnAnswer(callerID);

            _cv.alpha = 0.7f;
            _cv.interactable = false;
            _cv.blocksRaycasts = false;

            DialogSystem.Instance.CallDialog(callDialogID);
        }

        IEnumerator _AnimatedDialog()
        {
            int count = 0;
            bool callNext = true;

            if (_dialog.answers != null && _dialog.answers.Count > 0)
            {
                callNext = false;

                for (int i = 0; i < _dialog.answers.Count; i++)
                {
                    DialogAnswer da = Instantiate(answerPrefab, answersContainer).GetComponent<DialogAnswer>();
                    da.Init(_dialog.answers[i], i);
                }
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

            yield return new WaitForEndOfFrame();
            var scroll = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
            scroll.normalizedPosition = Vector2.zero;

            while (true)
            {
                dialogText.text += _dialog.dialog[count];

                count++;

                if (count >= _dialog.dialog.Length)
                    break;

                yield return new WaitForSeconds(0.015f);

                yield return new WaitForEndOfFrame();
                scroll.normalizedPosition = Vector2.zero;
            }

            if (callNext)
                DialogSystem.Instance.DelayedCallDialog(_dialog.callID);
            else
                _answersCV.DOFade(1f, 1.5f).Play();
        }
    }
}