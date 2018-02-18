using jamtasticvol3.DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogSystem))]
public class DialogSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        if(GUILayout.Button("Call Dialog"))
        {
            DialogSystem.Instance.TestDialog();
        }
    }
}