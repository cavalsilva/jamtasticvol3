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

        Dictionary<string, List<Features>> _featuresDict;

        private void Start()
        {
            _featuresDict = new Dictionary<string, List<Features>>();

            Invoke("StartGame", 2f);
        }

        void StartGame()
        {
            DialogSystem.DialogSystem.Instance.CallDialog(0.ToString());
        }

        public void NewMask()
        {
            _featuresDict.Clear();
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
        }

        public void RemoveMaskFeatures(string objName)
        {
            if (_featuresDict.ContainsKey(objName))
                _featuresDict.Remove(objName);
        }
    }
}