using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [Header("Load Scene System")]
    public string currentScene;
    public List<LocalGameManager> loadedLocalManagers;
    public LocalGameManager currentLocalManager;
    public List<ScenePlayerObject> playerControllers;
    public PlayerAttach currentPlayer;

    public bool startSceneLoaded;
    public bool currentSceneLoaded;

    public string startScene;

    public bool checkStart;
    public bool checkProx;
    public bool checkUnneeded;

    // Start is called before the first frame update
    void Start()
    {
        LoadSystemsStart();
    }

    // Update is called once per frame
    void Update()
    {
        LoadSystemsUpdate();
    }

    void LoadSystemsStart() 
    {
        playerControllers = new List<ScenePlayerObject>();
        LoadScene(startScene);
        DontDestroyOnLoad(this.gameObject);
        playerControllers = new List<ScenePlayerObject>();
        startSceneLoaded = false;
        checkStart = false;
        checkProx = false;
        checkUnneeded = false;
    }

    void LoadSystemsUpdate() 
    {
        if (!startSceneLoaded)
        {
            if (SceneManager.GetSceneByName(startScene).isLoaded)
            {
                startSceneLoaded = true;
                currentScene = startScene;
                for (int i = 0; i < loadedLocalManagers.Count; i++)
                {
                    if (loadedLocalManagers[i].inScene == currentScene)
                    {
                        currentLocalManager = loadedLocalManagers[i];
                    }
                }
                SceneManager.UnloadSceneAsync("StartScene");
            }
        }
        if (!currentSceneLoaded && startSceneLoaded)
        {
            if (SceneManager.GetSceneByName(currentScene).isLoaded)
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

    public void AddLoadedPlayer(PlayerAttach player, string sceneName) 
    {
        playerControllers.Add(new ScenePlayerObject(player, player.type, player.inScene));
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
                if (currentLocalManager.scenesNeeded[j] == tempSceneName)
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

    public void LoadScene(string name) 
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public void UnloadScene(string name) 
    {
        for (int i = loadedLocalManagers.Count - 1; i >= 0; i--) 
        {
            if (loadedLocalManagers[i].inScene == name) 
            {
                loadedLocalManagers.RemoveAt(i);
            }
        }
        for (int i = playerControllers.Count - 1; i >= 0; i--)
        {
            if (playerControllers[i].inScene == name)
            {
                playerControllers.RemoveAt(i);
            }
        }
        SceneManager.UnloadSceneAsync(name);
    }

    void SetCurrentLocalGameManager(string scene) 
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

    void SetCurrentPlayerCharacter(string scene)
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

    void AlignCurrentPlayerCharacter(string scene)
    {
        for (int i = 0; i < currentLocalManager.activePlayerSpawner.spwanPoints.Count; i++)
        {
            if (currentLocalManager.activePlayerSpawner.spwanPoints[i].name == scene)
            {
                currentPlayer.gameObject.SetActive(false);
                currentPlayer.transform.position = currentLocalManager.activePlayerSpawner.spwanPoints[i].position;
                currentPlayer.gameObject.SetActive(true);
            }
        }
    }

    public void TransitionScene(string scene) 
    {
        string previousScene = currentScene;
        currentScene = scene;

        if (currentScene != "")
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
