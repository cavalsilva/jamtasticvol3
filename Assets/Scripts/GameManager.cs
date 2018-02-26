using jamtasticvol3.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using jamtasticvol3.DialogSystem;
using System;
using jamtasticvol3.Characters;
using TouchScript.Gestures.TransformGestures;
using UnityEngine.SceneManagement;

namespace jamtasticvol3
{
    public class GameManager : Singleton<GameManager>
    {
        public enum Flags { B31, E32, M52 }
        public enum Features
        {
            Red, Orange, Yellow, Green, Cyan, Blue, Purple,
            Stars, Hearts,
            Feathers, Glitter, Jewels
        }

        public event Delegates.SimpleEvent OnNewMask, OnItemAdded, OnItemRemoved;

        public List<GameMask> gameMasks;
        public GameObject mask;

        List<Flags> _flags;

        int currentMask = -1;

        Dictionary<string, List<Features>> _featuresDict;

        Transform _maskContainer;

        private void Start()
        {
            _maskContainer = GameObject.Find("MaskContainer").transform;

            _featuresDict = new Dictionary<string, List<Features>>();
            _flags = new List<Flags>();

            StartGame();
        }

        void StartGame()
        {
            NewMask();
        }

        public void NewMask()
        {
            if (OnNewMask != null)
                OnNewMask();

            _featuresDict.Clear();
            DialogSystem.DialogSystem.Instance.ClearDialogs();

            currentMask++;

            if (currentMask < gameMasks.Count)
                DialogSystem.DialogSystem.Instance.DelayedCallDialog(gameMasks[currentMask].startDialogID);
            else if (_flags.Contains(Flags.B31) && _flags.Contains(Flags.M52) && CompareBibaManuMasks())
                DialogSystem.DialogSystem.Instance.DelayedCallDialog("F0");
            else
                DialogSystem.DialogSystem.Instance.DelayedCallDialog("F1");
        }

        public void DeliverMask()
        {
            Transform trans = _maskContainer.GetChild(0);
            TransformGesture[] gests = trans.GetComponentsInChildren<TransformGesture>();
            for(int i = 0; i < gests.Length; i++)
            {
                gests[i].enabled = false;
            }

            trans.SetParent(CharacterManager.Instance.GetCharacter(gameMasks[currentMask].characterName).transform);

            Instantiate(mask, _maskContainer);

            float perc = CompareMasks();

            /*List<TargetResult> results = gameMasks[currentMask].results;
            results.OrderBy(x => x.result);

            for (int i = 0; i < results.Count; i++)
            {
                if((int)perc <= results[i].result)
                {
                    DialogSystem.DialogSystem.Instance.CallDialog(results[i].callDialogID);
                    gameMasks[currentMask].finalResult = perc;
                    break;
                }
            }*/

            gameMasks[currentMask].features = GetMaskFeatures();
            gameMasks[currentMask].finalResult = perc;
            DialogSystem.DialogSystem.Instance.CallDialog(gameMasks[currentMask].results[0].callDialogID);
        }

        float CompareMasks()
        {
            List<Features> features = GetMaskFeatures();
            float count = 0;

            for(int i = 0; i < gameMasks[currentMask].features.Count; i++)
            {
                if (features.Contains(gameMasks[currentMask].features[i]))
                    count++;
            }

            return (count / gameMasks[currentMask].features.Count) * 100;
        }

        public List<Features> GetMaskFeatures()
        {
            List<string> keys = _featuresDict.Keys.ToList();
            List<Features> features = new List<Features>();

            for (int i = 0; i < keys.Count; i++)
            {
                for (int j = 0; j < _featuresDict[keys[i]].Count; j++)
                {
                    if (!features.Contains(_featuresDict[keys[i]][j]))
                        features.Add(_featuresDict[keys[i]][j]);
                }
            }

            return features;
        }

        public void AddMaskFeatures(string objName, List<Features> features)
        {
            if(!_featuresDict.ContainsKey(objName))
                _featuresDict.Add(objName, features);

            if (OnItemAdded != null)
                OnItemAdded();
        }

        public void RemoveMaskFeatures(string objName)
        {
            if (_featuresDict.ContainsKey(objName))
                _featuresDict.Remove(objName);

            if (OnItemRemoved != null)
                OnItemRemoved();
        }

        public void AddFlag(string flag)
        {
            _flags.Add((Flags)Enum.Parse(typeof(Flags), flag));
        }

        bool CompareBibaManuMasks()
        {
            List<Features> bibaMask = gameMasks.Find(x => x.characterName == "Biba").features;
            List<Features> manuMask = gameMasks.Find(x => x.characterName == "Manu").features;
            int count = 0;

            for (int i = 0; i < bibaMask.Count; i++)
            {
                if (manuMask.Contains(bibaMask[i]))
                    count++;
            }

            return count >= 2;
        }

        public void GameEnd()
        {
            StartCoroutine(_GameEnd());
        }
        IEnumerator _GameEnd()
        {
            yield return new WaitForSeconds(5f);

            Fade.Instance.FadeIn(() => SceneManager.LoadScene("Credits"));
        }
    }
}

[System.Serializable]
public class GameMask
{
    public string characterName;
    public string startDialogID;
    public List<jamtasticvol3.GameManager.Features> features;
    public List<TargetResult> results;
    public float finalResult;
}

[System.Serializable]
public class TargetResult
{
    public int result;
    public string callDialogID;
}