using jamtasticvol3.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using jamtasticvol3.DialogSystem;

namespace jamtasticvol3
{
    public class GameManager : Singleton<GameManager>
    {
        public enum Features
        {
            Feathers,
            Glitter,
            Bones,
            Piercings
        }

        public List<GameMask> gameMasks;

        int currentMask = -1;

        Dictionary<string, List<Features>> _featuresDict;

        private void Start()
        {
            _featuresDict = new Dictionary<string, List<Features>>();

            Invoke("StartGame", 2f);
        }

        void StartGame()
        {
            NewMask();
        }

        public void NewMask()
        {
            _featuresDict.Clear();
            DialogSystem.DialogSystem.Instance.ClearDialogs();

            currentMask++;

            DialogSystem.DialogSystem.Instance.DelayedCallDialog(gameMasks[currentMask].startDialogID);
        }

        public void DeliverMask()
        {
            int perc = CompareMasks();

            List<TargetResult> results = gameMasks[currentMask].results;
            results.OrderBy(x => x.result);

            for (int i = 0; i < results.Count - 1; i++)
            {
                if(perc <= results[i].result)
                {
                    DialogSystem.DialogSystem.Instance.DelayedCallDialog(results[i].callDialogID);
                    break;
                }
            }
        }

        int CompareMasks()
        {
            List<Features> features = GetMaskFeatures();
            int count = 0;

            for(int i = 0; i < gameMasks[currentMask].features.Count; i++)
            {
                if (features.Contains(gameMasks[currentMask].features[i]))
                    count++;
            }

            return (count / gameMasks[currentMask].features.Count) * 100;
        }

        List<Features> GetMaskFeatures()
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
        }

        public void RemoveMaskFeatures(string objName)
        {
            if (_featuresDict.ContainsKey(objName))
                _featuresDict.Remove(objName);
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
}

[System.Serializable]
public class TargetResult
{
    public float result;
    public string callDialogID;
}