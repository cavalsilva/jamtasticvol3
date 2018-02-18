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

        public string ID;
        public DialogType type = DialogType.Default;
        public string dialog;
        public List<Answer> answers;
    }

    [Serializable]
    public class Answer
    {
        public string answer;
        public string callID;
    }
}