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

    // Retain this game object across scenes.
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if(objs.Length > 1) {
            // Destroy new menu managers if they exist in new scenes
            Destroy(this.gameObject);
        } 
        
        DontDestroyOnLoad(this.gameObject);        

        optionsPanelActive = false;
        optionsPanel.SetActive(optionsPanelActive);

        creditsPanelActive = false;
        creditsPanel.SetActive(creditsPanelActive);
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
}
