using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
        public int startingSceneIndex; // The index of the scene to open with this button (found in Build Settings)     
        public GameObject optionsPanel;   
        bool optionsPanelActive;
        public GameObject creditsPanel;
        bool creditsPanelActive;
        public GameObject helpPanel;
        bool helpPanelActive;
        public GameObject pausePanel;
        bool pausePanelActive;
        public GameObject pauseBackground;
        bool pauseBackgroundActive;
        public UILevelIcon levelIcon;
        string website = "https://www.google.com/search?q=mythology-mayhem";

    // Retain this game object across scenes.
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if(objs.Length > 1)
        {
            for(int i = 0; i < objs.Length - 1; i++) {
                Destroy(objs[i].gameObject);
            }
        }      

        optionsPanelActive = false;
        optionsPanel.SetActive(optionsPanelActive);

        creditsPanelActive = false;
        creditsPanel.SetActive(creditsPanelActive);

        helpPanelActive = false;
        helpPanel.SetActive(helpPanelActive);

        pausePanelActive = false;
        pausePanel.SetActive(pausePanelActive);

        pauseBackgroundActive = false;
        pauseBackground.SetActive(pauseBackgroundActive);

        print(SceneManager.GetActiveScene().buildIndex);
        levelIcon.ChangeIcon(SceneManager.GetActiveScene().buildIndex);

        VolumeSaveSlider vss = GetComponent<VolumeSaveSlider>();
        vss.LoadVolume();
    }

    // Starts the game, triggered by Start Game Button's OnClick function
    public void StartGame()
    {
        SceneManager.LoadScene(startingSceneIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToggleOptions() {
        optionsPanelActive = !optionsPanelActive;
        optionsPanel.SetActive(optionsPanelActive);

        if(optionsPanelActive) {
            pausePanelActive = false;
            pausePanel.SetActive(pausePanelActive);
        } else {
            pausePanelActive = true;
            pausePanel.SetActive(pausePanelActive);
        }
    }

    public void ToggleCredits() {
        creditsPanelActive = !creditsPanelActive;
        creditsPanel.SetActive(creditsPanelActive);

        if(creditsPanelActive) {
            pausePanelActive = false;
            pausePanel.SetActive(pausePanelActive);
        } else {
            pausePanelActive = true;
            pausePanel.SetActive(pausePanelActive);
        }
    }

    public void ToggleHelp() {
        helpPanelActive = !helpPanelActive;
        helpPanel.SetActive(helpPanelActive);

        if(helpPanelActive) {
            pausePanelActive = false;
            pausePanel.SetActive(pausePanelActive);
        } else {
            pausePanelActive = true;
            pausePanel.SetActive(pausePanelActive);
        }
    }

    public void TogglePause() {
        
        // Toggle the background
        pauseBackgroundActive = !pauseBackgroundActive;
        pauseBackground.SetActive(pauseBackgroundActive);

        // Toggle the pause panel
        pausePanelActive = !pausePanelActive;
        pausePanel.SetActive(pausePanelActive);

        // If the pause panel is active and is going to be inactive, set the other panels to inactive as well
        if(pausePanelActive) {
            helpPanelActive = false;
            helpPanel.SetActive(helpPanelActive);

            creditsPanelActive = false;
            creditsPanel.SetActive(creditsPanelActive);

            optionsPanelActive = false;
            optionsPanel.SetActive(optionsPanelActive);
        }
        
    }

    public void OpenWebsite() {
        print(website);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
