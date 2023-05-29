using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//Make custom button to reset spwan point index
[CustomEditor(typeof(playerSpwaner))]
public class playerSpwanerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        playerSpwaner myScript = (playerSpwaner)target;
        if (GUILayout.Button("Reset Spwan Point Index"))
        {
            PlayerPrefs.SetInt("spwanPointIndex", 0);
        }

        //Make custom button to reset player prefs
        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}