using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
        [SerializeField] int startingSceneIndex; // The index of the scene to open with this button (found in Build Settings)     
        //[SerializeField] UILevelIcon levelIcon;
        string website = "https://www.google.com/search?q=mythology-mayhem";

        [SerializeField] GameObject[] menuPanels;
        [SerializeField] int pauseBackgroundElement = -1;
        [SerializeField] int pausePanelElement = -1;
        //[SerializeField] GameObject[] testingPanels;


    // Retain this game object across scenes.
    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if(objs.Length > 1)
        {
            for(int i = 0; i < objs.Length - 1; i++) {
                Destroy(objs[i].gameObject);
            }
        }     

        //print(SceneManager.GetActiveScene().buildIndex);

        // Level Icon
        /* switch(SceneManager.GetActiveScene().buildIndex) {
            case 0: break;
            default: levelIcon.ChangeIcon(SceneManager.GetActiveScene().buildIndex); break;
        } */
        

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
        SceneManager.LoadScene(startingSceneIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToggleOptions(int options) {
        
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

        // Deprecated Version, keeping for reference...
        /*
        optionsPanelActive = !optionsPanelActive;
        optionsPanel.SetActive(optionsPanelActive);

        if(optionsPanelActive) {
            pausePanelActive = false;
            pausePanel.SetActive(pausePanelActive);
        } else {
            pausePanelActive = true;
            pausePanel.SetActive(pausePanelActive);
        }
        */

    }// end toggle options

    public void ToggleCredits(int credits) {
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
