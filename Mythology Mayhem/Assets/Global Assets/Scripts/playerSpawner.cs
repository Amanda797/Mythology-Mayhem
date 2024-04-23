using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-10)]
public class playerSpawner : MythologyMayhem
{
    public Dimension type;

    int spawnPointIndex;
    int sceneIndex;
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject playerContainer2D;
    public GameObject playerContainer3D;
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
        if (overrideBool) 
        {
            spawnPointIndex = overrideIndex;
        } else
        {
            if(PlayerPrefs.GetInt("spawnPointIndex") >= spawnPoints.Count)
            {
                spawnPointIndex = 0;
            } else
            {
                spawnPointIndex = PlayerPrefs.GetInt("spawnPointIndex");
            }
        }

        GameObject spawnPlayerContainer = null;

        GameObject obj = null;
        if (localGameManager.sceneType == Dimension.TwoD)
        {
            spawnPlayerContainer = Instantiate(playerContainer2D, this.gameObject.transform);
            print(spawnPlayerContainer);

            obj = Instantiate(PlayerPrefabs2D.playerPrefabs[(int)GameManager.instance.gameData.selectedCharacter], spawnPlayerContainer.transform);

            obj.transform.position = spawnPoints[spawnPointIndex].position;
            obj.transform.rotation = spawnPoints[spawnPointIndex].rotation;

            obj.SetActive(true);
        }
        else
        {
            spawnPlayerContainer = Instantiate(playerContainer3D, this.gameObject.transform);

            obj = Instantiate(PlayerPrefabs3D.playerPrefabs[(int)GameManager.instance.gameData.selectedCharacter], spawnPlayerContainer.transform);

            obj.transform.position = spawnPoints[spawnPointIndex].position;
            obj.transform.rotation = spawnPoints[spawnPointIndex].rotation;

            obj.SetActive(true);

            EnemyAI3D[] tempEnemies = FindObjectsOfType<EnemyAI3D>();

            foreach (EnemyAI3D enemy in tempEnemies)
            {
                enemy.player = obj.GetComponent<Collider>();
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

        /*if (PlayerPrefs.GetInt("owl") == 1)
        {
            Debug.Log("Owl is active");
            GameObject owl = Instantiate(owlPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            owl.transform.parent = this.transform;
        } */

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

