using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class GameManager : MythologyMayhem
{
    [Header("Game Data")]
    public static GameManager instance;
    public GameData gameData;
    public OptionsData optionsData;

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
    public AudioSource ambianceAudioSource;
    [SerializeField] AudioMixer audioMixer;

    [Header("UI")]
    public HealthUIController huic;
    public bool UIActive;
    public float closeButtonPressTimer;
    public GameObject PressEObj;
    public TextMeshProUGUI PressEText;
    public GameObject eventSystem;
    public GameObject pauseMenu;
    public MenuManager pauseMenuManager;

    [Header("Player Stats")]
    public PlayerStats_SO stats;
    [SerializeField] Light uniDirLight;
    public GameObject gameplayUI;

    public bool startSceneDebugLoad;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;

        if( FindObjectOfType<EventSystem>() == null) eventSystem.SetActive(true);




        Application.backgroundLoadingPriority = ThreadPriority.Low;
        if (SceneManager.GetSceneByName(Level.MainMenu.ToString()).isLoaded)
        {
            gameplayUI.SetActive(false);
            inMainMenu = true;
            currentScene = Level.MainMenu;
        }
        startSceneDebugLoad = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            int index = loadedLocalManagers.IndexOf(currentLocalManager);
            TransitionScene(loadedLocalManagers[index + 1].inScene, "");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            int index = loadedLocalManagers.IndexOf(currentLocalManager);
            TransitionScene(Level.VikingVillage_2D, "");
        }
        if (!startSceneDebugLoad)
        {
            bool inStartScene = SceneManager.GetSceneByName("StartScene").isLoaded;
            if (inStartScene)
            {
                if (gameData.overrideLoad)
                {
                    startSceneDebugLoad = true;
                    LoadSystemsStart(false);
                }
            }
        }
        inMainMenu = SceneManager.GetSceneByName(Level.MainMenu.ToString()).isLoaded;

        ListenerTrackPlayer();

        if (Input.GetKeyDown(KeyCode.O)) 
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) 
        {
            //LoadGame();
        }
        if (!inMainMenu && !cutscenePlaying)
        {
            LoadSystemsUpdate();
        }
        if (currentScene == Level.GreekLabyrinth_3D && uniDirLight.enabled)
        {
            FindObjectOfType<TorchSFXManager>().ToggleAudioSources(true);
            uniDirLight.enabled = false;

            Camera camera = currentPlayer.gameObject.GetComponentInChildren<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;
        }
        else if (currentScene != Level.GreekLabyrinth_3D && !uniDirLight.enabled)
        {
            FindObjectOfType<TorchSFXManager>().ToggleAudioSources(false);
            uniDirLight.enabled = true;
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
            print("GameManager starting New Game");
            LoadScene(Level.CutScene1, true);
        }
        else
        {
            inMainMenu = false;
            cutscenePlaying = false;
            gameplayUI.SetActive(true);

            playerControllers = new List<ScenePlayerObject>();
            if (gameData.overrideLoad && startSceneDebugLoad)
            {
                print("Game Manager loading override Scene");
                currentScene = gameData.overrideStartScene;
                gameData.startScene = currentScene;
                gameData.spawnerToUse = currentScene;
                LoadScene(gameData.overrideStartScene, true);
            }
            else
            {
                print("Game Manager loading from save file");
                LoadGame();
                gameData.SetStartScene(false);
                LoadScene(gameData.startScene, true);
            }
        }
        DontDestroyOnLoad(this.gameObject);
        startSceneLoaded = false;
        checkStart = false;
        checkProx = false;
        checkUnneeded = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            else if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.clip = currentLocalManager.backgroundMusic;
                backgroundMusic.Play();
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

        //if(!currentPlayer.gameObject.activeSelf) currentPlayer.gameObject.SetActive(true);
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

                if (currentLocalManager.backgroundMusic != null)
                {
                    if (backgroundMusic.clip != currentLocalManager.backgroundMusic)
                    {
                        backgroundMusic.clip = currentLocalManager.backgroundMusic;
                        backgroundMusic.Play();
                    }
                }

                if (currentLocalManager.ambianceClip != null)
                {
                    if (ambianceAudioSource.clip != currentLocalManager.ambianceClip)
                    {
                        ambianceAudioSource.clip = currentLocalManager.ambianceClip;
                        ambianceAudioSource.Play();
                    }
                }
                //return;
            }
        }
    }

    void SetCurrentPlayerCharacter(Level scene)
    {
        if (scene == Level.MainMenu) return;
        if (scene.ToString() == "TitleSequence") return;

        for (int i = 0; i < playerControllers.Count; i++)
        {
            if (playerControllers[i].inScene == scene)
            {
                currentPlayer = playerControllers[i].player;
                playerControllers[i].player.gameObject.SetActive(true);
                if (currentPlayer.gameObject.GetComponent<PlayerMovement3D>() != null) currentPlayer.gameObject.GetComponent<PlayerMovement3D>().canPlayFootstepClip = true;
                else if (currentPlayer.gameObject.GetComponent<PlayerController>() != null) currentPlayer.gameObject.GetComponent<PlayerController>().canPlayFootstepClip = true;
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
                //break;
            }
        }
    }

    public void TransitionScene(Level scene, string spawnpointOverride)
    {
        huic.UpdateHealth();
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
            gameData.saveData = new SaveData();
            gameData.saveData.GenerateNewData();

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
    public void Popup(string message, bool isOpen) 
    {
        if (isOpen)
        {
            PressEObj.SetActive(true);
            PressEText.SetText(message);
        }
        else
        {
            PressEObj.SetActive(false);
            PressEText.SetText("");
        }
    }

    public void UpdateCollectedHearts(int totalCollectedHearts, float curHealth) 
    {
        int MaxHealth = 16 + (totalCollectedHearts * 4);
        stats.MaxHealth = MaxHealth;
        stats.CurrHealth = curHealth;

        gameData.maxHealth = MaxHealth;
        gameData.curHealth = curHealth;
        huic.UpdateHealth();
    }

    public void SaveOptionsData()
    {
        Debug.Log("Save Options");
        string sceneObjectsString = JsonUtility.ToJson(optionsData, true);

        System.IO.File.WriteAllText(Application.persistentDataPath + "OptionsData.json", sceneObjectsString);
    }
    public void LoadOptionsData()
    {
        Debug.Log("Load Options");
        if (!System.IO.File.Exists(Application.persistentDataPath + "OptionsData.json"))
        {
            SaveOptionsData();
            return;
        }

        string sceneObjectsString = System.IO.File.ReadAllText(Application.persistentDataPath + "OptionsData.json");
        optionsData = JsonUtility.FromJson<OptionsData>(sceneObjectsString);

        audioMixer.SetFloat("MasterVolume", optionsData.masterVolume);
        audioMixer.SetFloat("AmbianceVolume", optionsData.ambianceVolume);
        audioMixer.SetFloat("EnemyVolume", optionsData.enemyVolume);
        audioMixer.SetFloat("FootstepVolume", optionsData.footstepVolume);
        audioMixer.SetFloat("MusicVolume", optionsData.musicVolume);
        audioMixer.SetFloat("SoundEffectVolume", optionsData.sfxVolume);

    }
}
