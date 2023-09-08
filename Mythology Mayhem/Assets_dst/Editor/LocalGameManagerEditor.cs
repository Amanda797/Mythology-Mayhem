using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(LocalGameManager))]
public class LocalGameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalGameManager myScript = (LocalGameManager)target;

        if (myScript.inScene == MythologyMayhem.Level.None)
        {
            EditorGUILayout.HelpBox("You need to assign 'inScene' to equal current Level", MessageType.Error);
        }
        else
        {

            if (myScript.activePlayerSpawner == null)
            {
                EditorGUILayout.HelpBox("You need to add a Spawner System to this Level", MessageType.Error);

                if (GUILayout.Button("Add Spawner"))
                {
                    myScript.AddSpawner();
                }
            }

            if (myScript.activePlayerSpawner != null)
            {
                if (myScript.activePlayerSpawner.spwanPoints.Count == 0)
                {
                    EditorGUILayout.HelpBox("You need to add at least one Spawn Point to this Level", MessageType.Error);
                }
                else
                {
                    EditorGUILayout.HelpBox("You can add more than one Spawn Point if needed", MessageType.Info);
                }

                if (GUILayout.Button("Add Spawn Point"))
                {
                    myScript.AddSpawnPoint();
                }
            }

        }

        if (GUI.changed) 
        {
            EditorUtility.SetDirty(myScript);
            EditorSceneManager.MarkSceneDirty(myScript.gameObject.scene);
        }
    }
}
