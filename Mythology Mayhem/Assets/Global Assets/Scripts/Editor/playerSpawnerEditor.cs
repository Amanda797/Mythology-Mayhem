using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//Make custom button to reset spwan point index
[CustomEditor(typeof(playerSpawner))]
public class playerSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        playerSpawner myScript = (playerSpawner)target;
        if (GUILayout.Button("Reset Spawn Point Index"))
        {
            PlayerPrefs.SetInt("spawnPointIndex", 0);
        }

        //Make custom button to reset player prefs
        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}