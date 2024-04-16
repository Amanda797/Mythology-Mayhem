using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool inMainMenu;
    public bool cutscenePlaying;

    [Header("Background Music")]
    public AudioSource bgm;

    public GameObject gameplayUI;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        if (SceneManager.GetSceneByName(Level.MainMenu.ToString()).isLoaded)
        {
            gameplayUI.SetActive(false);
            inMainMenu = true;
            currentScene = Level.MainMenu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) 
        {
            LoadGame();
        }
        if (!inMainMenu && !cutscenePlaying)
        {
            LoadSystemsUpdate();
        }
    }
    public void LoadSystemsStart(bool newGame) 
    {
        if (newGame)
        {
            cutscenePlaying = true;
            inMainMenu = false;
            bgm.Stop();
            currentScene = Level.CutScene1;
            gameData.NewGame();

            LoadScene(Level.CutScene1, true);
        }
        else
        {
            inMainMenu = false;
            cutscenePlaying = false;
            gameplayUI.SetActive(true);

            gameData.SetStartScene();

            playerControllers = new List<ScenePlayerObject>();
            if (gameData.overrideLoad)
            {
                LoadScene(gameData.overrideStartScene, true);
            }
            else
            {
                LoadScene(gameData.startScene, true);
            }
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
                if (SceneManager.GetSceneByName("StartScene").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("StartScene");
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

    public void LoadScene(Level scene, bool single) 
    {
        if (single)
        {
            SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
        }
    }
    public void LoadScene(string scene) 
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
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
        SceneManager.UnloadSceneAsync(scene);
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

    public void CloseApplication()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SaveGame()
    {
        if (gameData.saveData == null)
        {
            SaveData newData = new SaveData();
            newData.settingsData = new SettingsData();
            gameData.saveData = newData;

            gameData.saveData.UpdateData(gameData);
        }
        else
        {
            gameData.saveData.UpdateData(gameData);
        }
    }
    public void LoadGame()
    {
        if (gameData.saveData == null)
        {
            SaveData newData = new SaveData();
            newData.settingsData = new SettingsData();
            gameData.saveData = newData;

            gameData.saveData.Load();
        }
        else
        {
            gameData.saveData.Load();
        }
    }
}
