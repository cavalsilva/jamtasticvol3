using jamtasticvol3.Characters;
using jamtasticvol3.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace jamtasticvol3.DialogSystem
{
    public class DialogSystem : Singleton<DialogSystem>
    {
        public RectTransform dialogsContainer;
        public GameObject containerPrefab;

        List<Dialog> _dialogs;

        void Start()
        {
            string json = FileLoader.LoadFile(FileType.Dialogs);
            json = "{\"Items\":" + json + "}";

            _dialogs = JsonHelper.FromJson<Dialog>(json).ToList();
        }

        public void CallDialog(string id)
        {
            Dialog newDialog = _dialogs.Find(x => x.ID == id);

            DialogEntry de = Instantiate(containerPrefab, dialogsContainer).GetComponent<DialogEntry>();
            de.Init(newDialog);

            if (newDialog.type == Dialog.DialogType.Start)
                GameManager.Instance.NewMask();

            if (!string.IsNullOrEmpty(newDialog.character))
            {
                CharacterManager.Instance.ShowCharacter(newDialog.character);

                if (!string.IsNullOrEmpty(newDialog.mood))
                    CharacterManager.Instance.SetCharacterMood(newDialog.character, (Dialog.Mood)Enum.Parse(typeof(Dialog.Mood), newDialog.mood));
            }
        }

        public void DelayedCallDialog(string id)
        {
            StartCoroutine(_DelayedCallDialog(id));
        }
        IEnumerator _DelayedCallDialog(string id)
        {
            yield return new WaitForSeconds(3f);

            CallDialog(id);
        }


        public void TestDialog()
        {
            var t = _dialogs.FindAll(x => x.type == Dialog.DialogType.Start);

            CallDialog(t.Find(x => x.ID == UnityEngine.Random.Range(0, t.Count).ToString()).ID);
        }
    }

    [Serializable]
    public class Dialog
    {
        public enum DialogType { Start = 0, Default = 1}
        public enum Mood { Normal, Happy, Sad, Angry }

        public string ID;
        public string character;
        public string mood;
        public DialogType type = DialogType.Default;
        public string dialog;
        public List<Answer> answers;
        public string callID;
    }

    [Serializable]
    public class Answer
    {
        public string answer;
        public string callID;
    }
}