using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class playerSpawner : MythologyMayhem
{
    public Dimension type;

    int spawnPointIndex;
    int playerIndex;
    int sceneIndex;
    public List<Transform> spawnPoints = new List<Transform>();
    public playerSelectable PlayerPrefabs2D;
    public playerSelectable PlayerPrefabs3D;

    public GameObject owlPrefab;

    public LocalGameManager localGameManager;
    public PlayerAttach playerAttach;
    public bool playerLoadComplete;

    public int overrideIndex;
    public bool overrideBool;
    public int overrideCharacterIndex;

    // Start is called before the first frame update
    void Awake()
    {       
        //PlayerPrefs.SetInt("spwanPointIndex", 0);
        /*if(PlayerPrefs.HasKey("spwanPointIndex"))
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
        */

        if (overrideBool) 
        {
            spawnPointIndex = overrideIndex;            
        }

        GameObject obj = null;

        if (localGameManager == null)
        {
            if (type == Dimension.TwoD)
            {
                obj = Instantiate(PlayerPrefabs2D.playerPrefabs[playerIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                obj.transform.parent = this.transform;
                obj.SetActive(true);
            }
            else
            {
                obj = Instantiate(PlayerPrefabs3D.playerPrefabs[playerIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                obj.transform.parent = this.transform;
                obj.SetActive(true);
            }
        }
        else
        {

            if (localGameManager.sceneType == Dimension.TwoD)
            {
                obj = Instantiate(PlayerPrefabs2D.playerPrefabs[playerIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
            else
            {
                obj = Instantiate(PlayerPrefabs3D.playerPrefabs[playerIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

                EnemyAI3D[] tempEnemies = FindObjectsOfType<EnemyAI3D>();

                foreach (EnemyAI3D enemy in tempEnemies)
                {
                    enemy.player = obj.GetComponent<Collider>();
                }
            }
        }

        if (obj != null)
        {
            if (localGameManager != null)
            {
                playerAttach = obj.GetComponent<PlayerAttach>();
                if (playerAttach != null)
                {
                    playerAttach.localGameManager = localGameManager;
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
            GameObject owl = Instantiate(owlPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            owl.transform.parent = this.transform;
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
