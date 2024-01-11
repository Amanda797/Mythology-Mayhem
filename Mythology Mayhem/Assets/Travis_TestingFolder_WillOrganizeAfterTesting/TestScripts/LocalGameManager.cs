using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LocalGameManager : MythologyMayhem
{
    public GameManager mainGameManager;
    public Dimension sceneType;
    public PlayerMovement3D player3D;
    public PlayerController player2D;

    public PlayerAttach player;

    public Transform sceneOrigin;
    public Vector3 sceneOriginOnLoad;

    public Level inScene;
    [SerializeField] public List<Level> scenesNeeded;
    [SerializeField] public List<Level> scenesLoadedOnStart;
    [SerializeField] public List<ProximityLoad> scenesLoadedOnProximity;
    [SerializeField] public List<SceneTransitionPoint> transitionPoints;
    public bool loadNextOnStart;

    [SerializeField] public playerSpawner activePlayerSpawner;
    public GameObject playerSpawnerPrefab;
    public GameObject spawnPointPrefab;
    public GameObject sceneTransitionPoint2D;
    public GameObject sceneTransitionPoint3D;

    public Transform spawnPointCreator;

    public Level sceneToConnect;
    public bool canTransition;
    public bool activeByDefault;

    [Header("Level Offset System")]
    public bool useOffset;
    public Vector3 levelOffset;
    public Vector3 moveLevel;

    public List<Level> levelsToDrag;

    [Header("CustomCameraSettings")]
    public bool useCustom;
    public bool aimAtTarget;
    public Vector3 followOffset;
    public float orthoSize;

    // Start is called before the first frame update
    void Start()
    {
        if (!mainGameManager)
        {
            mainGameManager = FindObjectOfType<GameManager>();
            if (!mainGameManager)
            {
                print("Could Not Find GameManager for Local in " + SceneManager.GetActiveScene().ToString());
            }
            else
            {
                mainGameManager.loadedLocalManagers.Add(this);
                sceneOriginOnLoad = this.sceneOrigin.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useOffset && mainGameManager.currentScene == inScene)
        {
            levelOffset += UpdateOffsets();
        }
    }

    Vector3 UpdateOffsets()
    {
        sceneOrigin.position += moveLevel * Time.deltaTime;
    
        Vector3 currentOffset = sceneOrigin.position - sceneOriginOnLoad;
        sceneOrigin.position = sceneOriginOnLoad;

        for (int i = 0; i < mainGameManager.loadedLocalManagers.Count; i++)
        {
            if (mainGameManager.loadedLocalManagers[i] != this)
            {
                bool checkLevel = false;
                for (int j = 0; j < levelsToDrag.Count; j++) 
                {
                    if (mainGameManager.loadedLocalManagers[i].inScene == levelsToDrag[j]) 
                    {
                        mainGameManager.loadedLocalManagers[i].sceneOrigin.position = mainGameManager.loadedLocalManagers[i].sceneOriginOnLoad;
                        mainGameManager.loadedLocalManagers[i].levelOffset = currentOffset + levelOffset;
                        checkLevel = true;
                    }
                }
                if (!checkLevel)
                {
                    mainGameManager.loadedLocalManagers[i].sceneOrigin.position = (mainGameManager.loadedLocalManagers[i].sceneOriginOnLoad + levelOffset);
                }
            }
        }
        return currentOffset;
    }

    public void AddPlayerLocalAndGlobal(PlayerAttach _player)
    {
        player = _player;
        if (useCustom && player.vCam != null)
        {
            if (!aimAtTarget)
            {
                player.vCam.LookAt = null;
            }
            player.vCam.m_Lens.OrthographicSize = orthoSize;
            CinemachineTransposer cTrans = player.vCam.GetCinemachineComponent<CinemachineTransposer>();
            cTrans.m_FollowOffset = followOffset;
        }
        mainGameManager.AddLoadedPlayer(player);
    }

    public void AddScene(Level scene)
    {
        mainGameManager.LoadScene(scene);
    }

    public void SceneTransition(Level scene)
    {
        if (mainGameManager.checkStart && mainGameManager.checkProx)
        {
            mainGameManager.TransitionScene(scene);
        }
    }

    public bool CheckProxScenes()
    {
        if (player != null)
        {
            bool check = true;

            for (int i = 0; i < scenesLoadedOnProximity.Count; i++)
            {
                Scene sceneCheck = SceneManager.GetSceneByName(scenesLoadedOnProximity[i].sceneToLoad.ToString());
                if (sceneCheck.name == scenesLoadedOnProximity[i].sceneToLoad.ToString())
                {
                    if (!SceneManager.GetSceneByName(scenesLoadedOnProximity[i].sceneToLoad.ToString()).isLoaded)
                    {
                        check = false;
                    }
                }
                else
                {
                    float playerDist = Vector2.Distance(player.transform.position, scenesLoadedOnProximity[i].transitionPoint.transform.position);
                    if (playerDist <= scenesLoadedOnProximity[i].proximityDistance)
                    {
                        AddScene(scenesLoadedOnProximity[i].sceneToLoad);
                        scenesLoadedOnProximity[i].loaded = true;
                    }
                    check = false;
                }

            }

            return check;

        }
        else
        {
            return false;
        }
    }

    public bool CheckStartScenes()
    {
        bool check = true;

        for (int i = 0; i < scenesLoadedOnStart.Count; i++)
        {
            Scene sceneCheck = SceneManager.GetSceneByName(scenesLoadedOnStart[i].ToString());
            if (sceneCheck.name == scenesLoadedOnStart[i].ToString())
            {
                if (!SceneManager.GetSceneByName(scenesLoadedOnStart[i].ToString()).isLoaded)
                {
                    check = false;
                }
            }
            else if(sceneCheck.name != "Null")
            {
                StartCoroutine(mainGameManager.LoadScene(scenesLoadedOnStart[i]));
                check = false;
            }

        }

        return check;
    }

    public void AddStartScene(Level scene) 
    {
        scenesLoadedOnStart.Add(scene);
        mainGameManager.checkStart = false;
        mainGameManager.checkUnneeded = false;
    }

    public void AddProxScene(Level scene, SceneTransitionPoint transitionPoint, float distance)
    {
        scenesLoadedOnProximity.Add(new ProximityLoad(scene,transitionPoint, distance));
        mainGameManager.checkProx = false;
        mainGameManager.checkUnneeded = false;
    }

    public void AddSpawner()
    {
        GameObject obj = Instantiate(playerSpawnerPrefab, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, this.transform);
        activePlayerSpawner = obj.GetComponent<playerSpawner>();
        if (activePlayerSpawner != null)
        {
            activePlayerSpawner.type = sceneType;

            activePlayerSpawner.localGameManager = this;
        }

        AddSceneToNeeded(inScene);
    }

    public void AddSpawnPoint()
    {
        if (activePlayerSpawner != null)
        {
            if (canTransition)
            {
                if (sceneType == Dimension.TwoD)
                {
                    GameObject transitionObj = Instantiate(sceneTransitionPoint2D, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, activePlayerSpawner.transform);
                    SceneTransitionPoint2D transitionScript = transitionObj.GetComponentInChildren<SceneTransitionPoint2D>();
                    if (transitionScript != null)
                    {
                        transitionScript.localGameManager = this;
                        transitionScript.sceneToTransition = sceneToConnect;
                        transitionScript.isActive = activeByDefault;
                        transitionScript.gameObject.name = (sceneToConnect.ToString());
                        transitionPoints.Add(transitionScript);
                    }
                    GameObject spawnPointObj = Instantiate(spawnPointPrefab, transitionScript.transform.position, transitionScript.transform.rotation, transitionObj.transform);
                    spawnPointObj.name = sceneToConnect.ToString();
                    AddSceneToNeeded(sceneToConnect);
                    AddSceneToStart(sceneToConnect);
                    activePlayerSpawner.spawnPoints.Add(spawnPointObj.transform);
                    return;
                }
                else
                {
                    GameObject transitionObj = Instantiate(sceneTransitionPoint3D, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, activePlayerSpawner.transform);
                    SceneTransitionPoint3D transitionScript = transitionObj.GetComponentInChildren<SceneTransitionPoint3D>();
                    if (transitionScript != null)
                    {
                        transitionScript.localGameManager = this;
                        transitionScript.sceneToTransition = sceneToConnect;
                        transitionScript.isActive = activeByDefault;
                        transitionScript.gameObject.name = (sceneToConnect.ToString());
                        transitionPoints.Add(transitionScript);
                    }
                    GameObject spawnPointObj = Instantiate(spawnPointPrefab, transitionScript.transform.position, transitionScript.transform.rotation, transitionObj.transform);
                    spawnPointObj.name = sceneToConnect.ToString();
                    AddSceneToNeeded(sceneToConnect);
                    AddSceneToStart(sceneToConnect);
                    activePlayerSpawner.spawnPoints.Add(spawnPointObj.transform);
                    return;
                }
            }
            else
            {
                GameObject obj = Instantiate(spawnPointPrefab, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, activePlayerSpawner.transform);
                obj.name = sceneToConnect.ToString();
                activePlayerSpawner.spawnPoints.Add(obj.transform);
            }
        }
    }

    void AddSceneToNeeded(Level scene) 
    {
        bool sceneCheck = false;
        for (int i = 0; i < scenesNeeded.Count; i++)
        {
            if (scenesNeeded[i] == scene)
            {
                sceneCheck = true;
            }
        }
        if (!sceneCheck)
        {
            scenesNeeded.Add(scene);
        }
    }

    void AddSceneToStart(Level scene)
    {
        bool sceneCheck = false;
        for (int i = 0; i < scenesLoadedOnStart.Count; i++)
        {
            if (scenesLoadedOnStart[i] == scene)
            {
                sceneCheck = true;
            }
        }
        if (!sceneCheck)
        {
            scenesLoadedOnStart.Add(scene);
        }
    }

}
