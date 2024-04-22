using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("Sound")]
    public AudioListener listener;
    public AudioSource backgroundMusic;

    [Header("UI")]
    public HealthUIController huic;
    public bool UIActive;
    public float closeButtonPressTimer;
    public GameObject PressEObj;
    public TextMeshProUGUI PressEText;

    [Header("Player Stats")]
    public PlayerStats_SO stats;

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
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        inMainMenu = SceneManager.GetSceneByName(Level.MainMenu.ToString()).isLoaded;

        ListenerTrackPlayer();

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

        if (closeButtonPressTimer > 0)
        {
            PressEObj.SetActive(true);
            closeButtonPressTimer -= Time.deltaTime;
            if (closeButtonPressTimer <= 0)
            {
                PressEObj.SetActive(false);
            }
        }
    }
    public void LoadSystemsStart(bool newGame) 
    {
        if (newGame)
        {
            cutscenePlaying = true;
            inMainMenu = false;
            backgroundMusic.Stop();
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
    void AlignCurrentPlayerCharacter(string spawnPointName) 
    {
        for (int i = 0; i < currentLocalManager.activePlayerSpawner.spawnPoints.Count; i++)
        {
            if (currentLocalManager.activePlayerSpawner.spawnPoints[i].name == spawnPointName)
            {
                currentPlayer.gameObject.SetActive(false);
                currentPlayer.transform.position = currentLocalManager.activePlayerSpawner.spawnPoints[i].position;
                currentPlayer.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void TransitionScene(Level scene, string spawnpointOverride) 
    {
        Level previousScene = currentScene;
        currentScene = scene;

        if (currentScene != Level.None)
        {
            SetCurrentLocalGameManager(currentScene);
            SetCurrentPlayerCharacter(currentScene);
            if (spawnpointOverride == "")
            {
                AlignCurrentPlayerCharacter(previousScene.ToString());
            }
            else 
            {
                AlignCurrentPlayerCharacter(spawnpointOverride);
            }
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
            newData.GenerateNewData();
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
    public ScenePlayerObject GetPlayer(Level inScene) 
    {
        for (int i = 0; i < playerControllers.Count; i++) 
        {
            if (playerControllers[i].inScene == inScene) 
            {
                return playerControllers[i];
            }
        }
        return null;
    }
    public LocalGameManager GetLocalGameManager(Level inScene) 
    {
        for (int i = 0; i < loadedLocalManagers.Count; i++) 
        {
            if (loadedLocalManagers[i].inScene == inScene) 
            {
                return loadedLocalManagers[i];
            }
        }

        return null;
    }
    void ListenerTrackPlayer() 
    {
        if (currentLocalManager != null)
        {
            listener.transform.position = currentLocalManager.player.transform.position;
        }
        else 
        {
            listener.transform.position = Vector3.zero;
        }
    }
    public void Popup(string message) 
    {
        closeButtonPressTimer = 0.5f;
        PressEText.SetText(message);
    }

    public void UpdateCollectedHearts(int totalCollectedHearts, float curHealth) 
    {
        int MaxHealth = 16 + (totalCollectedHearts * 4);
        stats.MaxHealth = MaxHealth;
        stats.CurrHealth = curHealth;
        huic.PlayerMaxHealth = MaxHealth;
        huic.PlayerCurrHealth = curHealth;
    }
}
