using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    string website = "https://www.google.com/search?q=mythology-mayhem";

    [SerializeField] GameObject pauseParent;
    [SerializeField] GameObject scrollPnl;
    [SerializeField] GameObject pausePnl;
    [SerializeField] GameObject optionsPnl;
    [SerializeField] GameObject creditsPnl;
    [SerializeField] GameObject helpPnl;
    int sceneIndex;

    public AudioSource menuEffectSource;

    // Retain this game object across scenes.
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if(objs.Length > 1)
        {
            for(int i = 0; i < objs.Length - 1; i++) {
                Destroy(objs[i].gameObject);
            }
        }     

        VolumeSaveSlider vss = GetComponent<VolumeSaveSlider>();
        vss.LoadVolume();
    }//end awake

    void Update() {
        //Open/Close Pause Menu
        if(Input.GetKeyDown(KeyCode.P)) {
            TogglePause();
        }
    }//end update

    // Starts the game, triggered by Start Game Button's OnClick function
    public void StartGame()
    {
        //Reset Player Prefs
        int spwanPointIndex = 0;
        int playerIndex = 0;

        PlayerPrefs.SetInt("spawnPointIndex", 0);
        spwanPointIndex = PlayerPrefs.GetInt("spawnPointIndex");

        PlayerPrefs.SetInt("playerIndex", 0);
        playerIndex = PlayerPrefs.GetInt("playerIndex");

        //Load First Scene

        //PlayerPrefs.SetInt("sceneIndex", 1);
        //sceneIndex = PlayerPrefs.GetInt("sceneIndex");

        PlayerPrefs.SetString("spawningScene", "Cutscene1");
        string loadScene = PlayerPrefs.GetString("spawningScene");
        SceneManager.LoadScene(loadScene, LoadSceneMode.Single);
    }

    public void ContinueGame() {
        if(PlayerPrefs.HasKey("spawningScene"))
        {
            string loadScene = PlayerPrefs.GetString("spawningScene");
            SceneManager.LoadScene(loadScene, LoadSceneMode.Single);
        }
        else
        {
            StartGame();
        }

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }//end continue game

    public void CharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToggleOptions() {
        menuEffectSource.Play();

        //toggle between options and pause view
        if (!optionsPnl.activeSelf)
        {
            pausePnl.SetActive(false);
            optionsPnl.SetActive(true);
        }
        else
        {
            pausePnl.SetActive(true);
            optionsPnl.SetActive(false);
        }
    }

    public void ToggleCredits() {
        menuEffectSource.Play();

        //toggle between options and pause view
        if (!creditsPnl.activeSelf)
        {
            pausePnl.SetActive(false);
            creditsPnl.SetActive(true);
        }
        else
        {
            pausePnl.SetActive(true);
            creditsPnl.SetActive(false);
        }
    }

    public void ToggleHelp() {
        menuEffectSource.Play();

        //toggle between options and pause view
        if (!helpPnl.activeSelf)
        {
            pausePnl.SetActive(false);
            helpPnl.SetActive(true);
        }
        else
        {
            pausePnl.SetActive(true);
            helpPnl.SetActive(false);
        }
    }

    public void TogglePause() {
        pauseParent.SetActive(!pauseParent.activeSelf);
        optionsPnl.SetActive(!pauseParent.activeSelf);
        creditsPnl.SetActive(!pauseParent.activeSelf);
        helpPnl.SetActive(!pauseParent.activeSelf);
        pausePnl.SetActive(pauseParent.activeSelf);

        if (pauseParent.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        else if (scrollPnl.activeSelf) Cursor.visible = true;
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void OpenWebsite() {
        print(website);
    }

    public void MainMenu() {
        Time.timeScale = 1;
        GameManager.instance.playerControllers.Clear();
        GameManager.instance.loadedLocalManagers.Clear();
        GameManager.instance.inMainMenu = true;
        GameManager.instance.huic.heartState.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void CloseScroll()
    {
        Time.timeScale = 1;
        scrollPnl.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
