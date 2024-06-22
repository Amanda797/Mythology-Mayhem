using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
        string website = "https://www.google.com/search?q=mythology-mayhem";

        [SerializeField] GameObject[] menuPanels;
        [SerializeField] int pauseBackgroundElement = -1;
        [SerializeField] int pausePanelElement = -1;
        int sceneIndex;

    public AudioSource menuEffectSource;

    // Retain this game object across scenes.
    private void Awake() {
        menuEffectSource = GetComponent<AudioSource>();

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

    public void ToggleOptions(int options) {
        menuEffectSource.Play();
        // Make sure there is a background present (needs to be active to display children)
        if(pauseBackgroundElement != -1)
            menuPanels[pauseBackgroundElement].SetActive(true);

        //toggle between options and pause view
        if(menuPanels[options].activeInHierarchy) {            
            menuPanels[options].SetActive(false);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(true);
        } else {
            menuPanels[options].SetActive(true);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(false);
        }

    }// end toggle options

    public void ToggleCredits(int credits) {
        menuEffectSource.Play();
        // Make sure there is a background present (needs to be active to display children)
        if(pauseBackgroundElement != -1)
            menuPanels[pauseBackgroundElement].SetActive(true);

        //toggle between credits and pause view
        if(menuPanels[credits].activeInHierarchy) {            
            menuPanels[credits].SetActive(false);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(true);
        } else {
            menuPanels[credits].SetActive(true);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(false);
        }
    }

    public void ToggleHelp(int help) {
        menuEffectSource.Play();
        // Make sure there is a background present (needs to be active to display children)
        if(pauseBackgroundElement != -1)
            menuPanels[pauseBackgroundElement].SetActive(true);

        //toggle between help and pause view
        if(menuPanels[help].activeInHierarchy) {            
            menuPanels[help].SetActive(false);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(true);
        } else {
            menuPanels[help].SetActive(true);
            if(pausePanelElement != -1)
                menuPanels[pausePanelElement].SetActive(false);
        }
    }

    public void TogglePause() {
        if(pausePanelElement != -1 && pauseBackgroundElement != -1)
        {
            menuEffectSource.Play();
            // Toggle the background and pause panels
            if(menuPanels[pauseBackgroundElement].activeInHierarchy) 
            {
                menuPanels[pauseBackgroundElement].SetActive(false);
                menuPanels[pausePanelElement].SetActive(false);
            } else {            
                menuPanels[pauseBackgroundElement].SetActive(true);
                menuPanels[pausePanelElement].SetActive(true);
            } 
        }
        else {
            print("Pause Panel Element: " + pausePanelElement + ", Pause Background Element: " + pauseBackgroundElement + ". Resolve.");
        }
        
    } // end toggle pause

    public void OpenWebsite() {
        print(website);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
