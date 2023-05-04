using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class playerSpwaner : MonoBehaviour
{
    int spwanPointIndex;
    int playerIndex;
    public List<Transform> spwanPoints = new List<Transform>();
    public playerSelectable PlayerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("spwanPointIndex", 0);
        if(PlayerPrefs.HasKey("spwanPointIndex"))
        {
            spwanPointIndex = PlayerPrefs.GetInt("spwanPointIndex");
        }
        else
        {
            PlayerPrefs.SetInt("spwanPointIndex", 0);
            spwanPointIndex = PlayerPrefs.GetInt("spwanPointIndex");
        }

        if(PlayerPrefs.HasKey("playerIndex"))
        {
            playerIndex = PlayerPrefs.GetInt("playerIndex");
        }
        else
        {
            PlayerPrefs.SetInt("playerIndex", 0);
            playerIndex = PlayerPrefs.GetInt("playerIndex");
        }

        if(spwanPointIndex >= spwanPoints.Count)
        {
            spwanPointIndex = 0;
            PlayerPrefs.SetInt("spwanPointIndex", spwanPointIndex);
        }

        Instantiate(PlayerPrefabs.playerPrefabs[playerIndex],spwanPoints[spwanPointIndex].position,spwanPoints[spwanPointIndex].rotation);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//Make custom button to reset spwan point index
[CustomEditor(typeof(playerSpwaner))]
public class playerSpwanerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        playerSpwaner myScript = (playerSpwaner)target;
        if(GUILayout.Button("Reset Spwan Point Index"))
        {
            PlayerPrefs.SetInt("spwanPointIndex", 0);
        }

        //Make custom button to reset player prefs
        if(GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}


