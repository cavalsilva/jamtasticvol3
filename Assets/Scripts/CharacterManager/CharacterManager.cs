using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using jamtasticvol3.DialogSystem;
using jamtasticvol3.Utils;
using System.Linq;

namespace jamtasticvol3.Characters
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        Dictionary<string, Character> _characters;

        public void ShowCharacter(string charName)
        {
            List<string> keys = _characters.Keys.ToList();

            for(int i = 0; i < keys.Count; i++)
            {
                _characters[keys[i]].gameObject.SetActive(charName.Equals(_characters[keys[i]].name) ? true : false);
            }
        }

        public Character GetCharacter(string charName)
        {
            return _characters[charName];
        }

        public void AddCharacter(string charName, Character script)
        {
            if (_characters == null)
                _characters = new Dictionary<string, Character>();

            _characters.Add(charName, script);
        }

        public void SetCharacterMood(string charName, Dialog.Mood mood)
        {
            _characters[charName].SetMood(mood);
        }
    }
}