using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MythologyMayhem
{
    [Header("Game Data")]
    public static GameManager instance;
    public GameData gameData;

    [Header("Load Scene System")]
    public Level currentScene;
    public List<LocalGameManager> loadedLocalManagers;
    public LocalGameManager currentLocalManager;
    public List<ScenePlayerObject> playerControllers;
    public PlayerAttach currentPlayer;

    public bool startSceneLoaded;
    public bool currentSceneLoaded;

    public bool checkStart;
    public bool checkProx;
    public bool checkUnneeded;

    public float startDelay;

    [Header("Testing")]
    public string testSceneLoad;
    public Camera loadScreenCamera;
    public Text loadProgress;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            if (startDelay <= 0)
            {
                startDelay = 0;
                LoadSystemsStart();
            }
        }
        else
        {
            LoadSystemsUpdate();
        }
    }

    void LoadSystemsStart() 
    {
        gameData.SetStartScene();

        playerControllers = new List<ScenePlayerObject>();
        if (gameData.overrideLoad) 
        {
            loadScreenCamera.enabled = true;
            StartCoroutine(LoadStartScene(testSceneLoad));
        }
        else
        {
            StartCoroutine(LoadStartScene(gameData.startScene.ToString()));
        }
        DontDestroyOnLoad(this.gameObject);
        startSceneLoaded = false;
        checkStart = false;
        checkProx = false;
        checkUnneeded = false;
    }

    void LoadSystemsUpdate() 
    {
        if (!startSceneLoaded)
        {
            if (gameData.overrideLoad) 
            {
                if (SceneManager.GetSceneByName(testSceneLoad).isLoaded)
                {
                    startSceneLoaded = true;
                    currentScene = gameData.startScene;
                    for (int i = 0; i < loadedLocalManagers.Count; i++)
                    {
                        if (loadedLocalManagers[i].inScene == currentScene)
                        {
                            currentLocalManager = loadedLocalManagers[i];
                        }
                    }
                    SceneManager.UnloadSceneAsync("StartScene");

                    loadScreenCamera.enabled = false; ;
                }
            }
            else
            {
                if (SceneManager.GetSceneByName(gameData.startScene.ToString()).isLoaded)
                {
                    startSceneLoaded = true;
                    currentScene = gameData.startScene;
                    for (int i = 0; i < loadedLocalManagers.Count; i++)
                    {
                        if (loadedLocalManagers[i].inScene == currentScene)
                        {
                            currentLocalManager = loadedLocalManagers[i];
                        }
                    }
                    SceneManager.UnloadSceneAsync("StartScene");

                    loadScreenCamera.enabled = false;
                }
            }
        }
        if (!currentSceneLoaded && startSceneLoaded)
        {
            if (SceneManager.GetSceneByName(currentScene.ToString()).isLoaded)
            {
                currentSceneLoaded = true;
            }
        }
        if (currentLocalManager != null)
        {
            if (currentLocalManager.inScene != currentScene)
            {
                SetCurrentLocalGameManager(currentScene);
            }
        }
        else
        {
            SetCurrentLocalGameManager(currentScene);
        }
        if (currentPlayer != null)
        {
            if (currentPlayer.inScene != currentScene)
            {
                SetCurrentPlayerCharacter(currentScene);
            }
        }
        else
        {
            SetCurrentPlayerCharacter(currentScene);
        }

        if (currentSceneLoaded && currentLocalManager != null)
        {
            if (!checkStart)
            {
                checkStart = CheckLocalGameManagerStartLoad();
            }
            if (!checkProx)
            {
                checkProx = CheckLocalGameManagerProximityLoad();
            }
            if (!checkUnneeded)
            {
                checkUnneeded = UnloadUnneededScenes();
            }

        }
    }

    public void AddLoadedPlayer(PlayerAttach player) 
    {
        ScenePlayerObject spo = new ScenePlayerObject(player, player.type, player.inScene);
        playerControllers.Add(spo);
    }

    bool CheckLocalGameManagerProximityLoad() 
    {
        return currentLocalManager.CheckProxScenes();
    }

    bool CheckLocalGameManagerStartLoad() 
    {
        return currentLocalManager.CheckStartScenes();
    }

    bool UnloadUnneededScenes() 
    {
        bool check = true;

        int totalSceneCount = SceneManager.sceneCount;
        for (int i = totalSceneCount - 1; i >= 0; i--)
        {
            string tempSceneName = SceneManager.GetSceneAt(i).name;
            bool found = false;
            for (int j = 0; j < currentLocalManager.scenesNeeded.Count; j++)
            {
                if (currentLocalManager.scenesNeeded[j].ToString() == tempSceneName)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                UnloadScene(SceneManager.GetSceneAt(i).name);
                check = false;
            }
        }

        return check;
    }

    public IEnumerator LoadStartScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            loadProgress.text = ("Loading : " + (asyncLoad.progress * 100) + "%");
            yield return null;
        }
    }
    public IEnumerator LoadScene(Level scene) 
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public IEnumerator LoadScene(string scene) 
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        print(asyncLoad.isDone + ": Loaded");
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void UnloadScene(string scene) 
    {
        for (int i = loadedLocalManagers.Count - 1; i >= 0; i--) 
        {
            if (loadedLocalManagers[i].inScene.ToString() == scene) 
            {
                loadedLocalManagers.RemoveAt(i);
            }
        }
        for (int i = playerControllers.Count - 1; i >= 0; i--)
        {
            if (playerControllers[i].inScene.ToString() == scene)
            {
                playerControllers.RemoveAt(i);
            }
        }
        StartCoroutine(UnloadSceneIEnum(scene));
    }
    public IEnumerator UnloadSceneIEnum(string scene) 
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene.ToString());

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    void SetCurrentLocalGameManager(Level scene) 
    {
        for (int i = 0; i < loadedLocalManagers.Count; i++)
        {
            if (loadedLocalManagers[i].inScene == scene)
            {
                currentLocalManager = loadedLocalManagers[i];
                return;
            }
        }
    }

    void SetCurrentPlayerCharacter(Level scene)
    {
        for (int i = 0; i < playerControllers.Count; i++)
        {
            if (playerControllers[i].inScene == scene)
            {
                currentPlayer = playerControllers[i].player;
                playerControllers[i].player.gameObject.SetActive(true);
            }
            else 
            {
                playerControllers[i].player.gameObject.SetActive(false);
            }
        }
    }

    void AlignCurrentPlayerCharacter(Level scene)
    {
        for (int i = 0; i < currentLocalManager.activePlayerSpawner.spawnPoints.Count; i++)
        {
            if (currentLocalManager.activePlayerSpawner.spawnPoints[i].name == scene.ToString())
            {
                currentPlayer.gameObject.SetActive(false);
                currentPlayer.transform.position = currentLocalManager.activePlayerSpawner.spawnPoints[i].position;
                currentPlayer.gameObject.SetActive(true);
            }
        }
    }

    public void TransitionScene(Level scene) 
    {
        Level previousScene = currentScene;
        currentScene = scene;

        if (currentScene != Level.None)
        {
            SetCurrentLocalGameManager(currentScene);
            SetCurrentPlayerCharacter(currentScene);
            AlignCurrentPlayerCharacter(previousScene);
        }
        checkStart = false;
        checkProx = false;
        checkUnneeded = false;
    }
}
