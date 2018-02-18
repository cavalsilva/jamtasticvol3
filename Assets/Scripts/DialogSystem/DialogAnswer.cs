using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace jamtasticvol3.DialogSystem
{
    public class DialogAnswer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Text answerText;
        public Color normalColor, hoverColor, selectedColor;

        Button _bt;
        Answer _answer;
        int _id;
        bool _answered = false;

        DialogEntry dialogEntry;

        public void Init(Answer answer, int id)
        {
            _answer = answer;
            _id = id;

            dialogEntry = transform.parent.parent.GetComponent<DialogEntry>();
            dialogEntry.OnAnswer += OnAnswer;

            answerText.text = (_id+1) + ") " + _answer.answer;

            _bt = GetComponent<Button>();
            _bt.onClick.AddListener(() => 
            {
                dialogEntry.SendAnswer(_id, answer.callID);
            });
        }

        void OnAnswer(int id)
        {
            _answered = true;

            answerText.color = id == _id ? selectedColor : normalColor;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            answerText.color = hoverColor;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (_answered)
                return;

            answerText.color = normalColor;
        }
    }
}