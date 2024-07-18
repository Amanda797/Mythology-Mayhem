using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Button continueButton;
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        gameManager = GameManager.instance;
        if (!System.IO.File.Exists(Application.persistentDataPath + "SaveData.json")) continueButton.enabled = false;
    }
    public void NewGame()
    {
        gameManager.LoadSystemsStart(true);
    }

    public void Continue()
    {
        gameManager.LoadSystemsStart(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
