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
    }

    public void ToggleCredits() {
        creditsPanelActive = !creditsPanelActive;
        creditsPanel.SetActive(creditsPanelActive);
    }

    public void ToggleHelp() {
        helpPanelActive = !helpPanelActive;
        helpPanel.SetActive(helpPanelActive);
    }

    public void TogglePause() {
        if(pausePanelActive) {
            pausePanelActive = !pausePanelActive;
            pausePanel.SetActive(pausePanelActive);

            helpPanelActive = false;
            helpPanel.SetActive(helpPanelActive);

            creditsPanelActive = false;
            creditsPanel.SetActive(creditsPanelActive);

            optionsPanelActive = false;
            optionsPanel.SetActive(optionsPanelActive);
        } else {
            pausePanelActive = !pausePanelActive;
            pausePanel.SetActive(pausePanelActive);
        }

        
    }
}
