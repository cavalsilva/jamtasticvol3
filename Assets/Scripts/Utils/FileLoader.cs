using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace jamtasticvol3.Utils
{
    public enum FileType { Dialogs }

    public static class FileLoader
    {
        static bool _started = false;

        static string _dataFolder = "Data";
        static Dictionary<FileType, string> _files = new Dictionary<FileType, string>()
        {
            { FileType.Dialogs, "dialogs.json" }
        };

        static void Init()
        {
            if (_started)
                return;

            _started = true;

#if UNITY_IOS || UNITY_ANDROID
        dataFolder = Application.persistentDataPath + "/" + dataFolder;
#endif

            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
                File.SetAttributes(_dataFolder, FileAttributes.Normal);
            }
        }

        public static string LoadFile(FileType type)
        {
            Init();

            string path = _dataFolder + "/" + _files[FileType.Dialogs];
            string t = "";

            if (File.Exists(path))
            {
                t = File.ReadAllText(path);
            }
            else
                Debug.LogError("File at " + path + " not found!");

            if(t == "")
                Debug.LogError("File at " + path + " is empty!");

            return t;
        }
    }
}