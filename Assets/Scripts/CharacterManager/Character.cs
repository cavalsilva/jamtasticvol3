using jamtasticvol3.DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace jamtasticvol3.Characters
{
    public class Character : MonoBehaviour
    {
        public Sprite normal, happy, sad, angry, special;

        Image _image;

        Dictionary<Dialog.Mood, Sprite> _moods;

        void Start()
        {
            _image = GetComponent<Image>();

            _moods = new Dictionary<Dialog.Mood, Sprite>()
            {
                { Dialog.Mood.Normal, normal },
                { Dialog.Mood.Happy, happy },
                { Dialog.Mood.Sad, sad },
                { Dialog.Mood.Angry, angry },
                { Dialog.Mood.Special, special }
            };

            CharacterManager.Instance.AddCharacter(name, this);

            gameObject.SetActive(false);
        }

        public void SetMood(Dialog.Mood mood)
        {
            _image.sprite = _moods[mood];
        }
    }
}