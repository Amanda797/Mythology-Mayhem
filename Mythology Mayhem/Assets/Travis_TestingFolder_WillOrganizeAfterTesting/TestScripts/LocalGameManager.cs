using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LocalGameManager : MonoBehaviour
{
    public GameManager mainGameManager;
    public ScenePlayerObject.PlayerType sceneType;
    public PlayerMovement3D player3D;
    public PlayerController player2D;

    public PlayerAttach player;

    public string inScene;
    [SerializeField] public List<string> scenesNeeded;
    [SerializeField] public List<string> scenesLoadedOnStart;
    [SerializeField] public List<ProximityLoad> scenesLoadedOnProximity;
    [SerializeField] public List<SceneTransitionPoint> transitionPoints;
    public bool loadNextOnStart;

    [SerializeField] public playerSpwaner activePlayerSpawner;
    public GameObject playerSpawnerPrefab;
    public GameObject spawnPointPrefab;
    public GameObject sceneTransitionPoint2D;
    public GameObject sceneTransitionPoint3D;

    public Transform spawnPointCreator;

    public string sceneToConnect;
    public bool canTransition;
    public bool activeByDefault;

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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayerLocalAndGlobal(PlayerAttach _player)
    {
        player = _player;
        mainGameManager.AddLoadedPlayer(player, inScene);
    }

    public void AddScene(string scene)
    {
        mainGameManager.LoadScene(scene);
    }

    public void SceneTransition(string scene)
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
                Scene sceneCheck = SceneManager.GetSceneByName(scenesLoadedOnProximity[i].sceneToLoad);
                if (sceneCheck.name == scenesLoadedOnProximity[i].sceneToLoad)
                {
                    if (!SceneManager.GetSceneByName(scenesLoadedOnProximity[i].sceneToLoad).isLoaded)
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
            Scene sceneCheck = SceneManager.GetSceneByName(scenesLoadedOnStart[i]);
            if (sceneCheck.name == scenesLoadedOnStart[i])
            {
                if (!SceneManager.GetSceneByName(scenesLoadedOnStart[i]).isLoaded)
                {
                    check = false;
                }
            }
            else if(sceneCheck.name != "Null")
            {
                mainGameManager.LoadScene(scenesLoadedOnStart[i]);
                check = false;
            }

        }

        return check;
    }

    public void AddStartScene(string scene) 
    {
        scenesLoadedOnStart.Add(scene);
        mainGameManager.checkStart = false;
        mainGameManager.checkUnneeded = false;
    }

    public void AddProxScene(string scene, SceneTransitionPoint transitionPoint, float distance)
    {
        scenesLoadedOnProximity.Add(new ProximityLoad(scene,transitionPoint, distance));
        mainGameManager.checkProx = false;
        mainGameManager.checkUnneeded = false;
    }

    public void AddSpawner()
    {
        GameObject obj = Instantiate(playerSpawnerPrefab, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, this.transform);
        activePlayerSpawner = obj.GetComponent<playerSpwaner>();
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
                if (sceneType == ScenePlayerObject.PlayerType.TwoD)
                {
                    GameObject transitionObj = Instantiate(sceneTransitionPoint2D, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, activePlayerSpawner.transform);
                    SceneTransitionPoint2D transitionScript = transitionObj.GetComponentInChildren<SceneTransitionPoint2D>();
                    if (transitionScript != null)
                    {
                        transitionScript.localGameManager = this;
                        transitionScript.sceneToTransition = sceneToConnect;
                        transitionScript.isActive = activeByDefault;
                        transitionScript.gameObject.name = (sceneToConnect);
                        transitionPoints.Add(transitionScript);
                    }
                    GameObject spawnPointObj = Instantiate(spawnPointPrefab, transitionScript.transform.position, transitionScript.transform.rotation, transitionObj.transform);
                    spawnPointObj.name = sceneToConnect;
                    AddSceneToNeeded(sceneToConnect);
                    AddSceneToStart(sceneToConnect);
                    activePlayerSpawner.spwanPoints.Add(spawnPointObj.transform);
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
                        transitionScript.gameObject.name = (sceneToConnect);
                        transitionPoints.Add(transitionScript);
                    }
                    GameObject spawnPointObj = Instantiate(spawnPointPrefab, transitionScript.transform.position, transitionScript.transform.rotation, transitionObj.transform);
                    spawnPointObj.name = sceneToConnect;
                    AddSceneToNeeded(sceneToConnect);
                    AddSceneToStart(sceneToConnect);
                    activePlayerSpawner.spwanPoints.Add(spawnPointObj.transform);
                    return;
                }
            }
            else
            {
                GameObject obj = Instantiate(spawnPointPrefab, spawnPointCreator.transform.position, spawnPointCreator.transform.rotation, activePlayerSpawner.transform);
                obj.name = sceneToConnect;
                activePlayerSpawner.spwanPoints.Add(obj.transform);
            }
        }
    }

    void AddSceneToNeeded(string scene) 
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

    void AddSceneToStart(string scene)
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
