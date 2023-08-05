using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class playerSpwaner : MonoBehaviour
{
    public ScenePlayerObject.PlayerType type;

    int spwanPointIndex;
    int playerIndex;
    int sceneIndex;
    public List<Transform> spwanPoints = new List<Transform>();
    public playerSelectable PlayerPrefabs2D;
    public playerSelectable PlayerPrefabs3D;

    public GameObject owlPrefab;

    public LocalGameManager localGameManager;
    public PlayerAttach playerAttach;
    public bool playerLoadComplete;

    public int overrideIndex;
    public bool overrideBool;

    // Start is called before the first frame update
    void Awake()
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

        // Save Scene Status
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex);

        if (overrideBool) 
        {
            spwanPointIndex = overrideIndex;
            for (int i = 0; i < spwanPoints.Count; i++) 
            { 
                
            }
        }
        GameObject obj = null;
        if (type == ScenePlayerObject.PlayerType.TwoD)
        {
            obj = Instantiate(PlayerPrefabs2D.playerPrefabs[playerIndex], spwanPoints[spwanPointIndex].position, spwanPoints[spwanPointIndex].rotation);
        }
        else 
        {
            obj = Instantiate(PlayerPrefabs3D.playerPrefabs[playerIndex], spwanPoints[spwanPointIndex].position, spwanPoints[spwanPointIndex].rotation);
        }

        if (obj != null)
        {
            if (localGameManager != null)
            {
                playerAttach = obj.GetComponent<PlayerAttach>();
                if (playerAttach != null)
                {
                    playerAttach.inScene = localGameManager.inScene;
                    playerAttach.type = localGameManager.sceneType;
                    obj.name = (localGameManager.sceneType.ToString() + " Character " + localGameManager.inScene);
                    obj.SetActive(false);
                }
            }
        }

        if (PlayerPrefs.GetInt("owl") == 1)
        {
            Debug.Log("Owl is active");
            Instantiate(owlPrefab, spwanPoints[spwanPointIndex].position, spwanPoints[spwanPointIndex].rotation).SetActive(true);
        } 

    }

    // Update is called once per frame
    void Update()
    {
        if (localGameManager != null)
        {
            if (!playerLoadComplete)
            {
                AddPlayerLocal();
            }
        }
    }

    void AddPlayerLocal()
    {

        if (localGameManager.player == null)
        {
            localGameManager.AddPlayerLocalAndGlobal(playerAttach);
        }
        else 
        {
            playerLoadComplete = true;
        }

    }
}


/*
//Make custom button to reset spwan point index
[CustomEditor (typeof(playerSpwaner))]
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
*/

