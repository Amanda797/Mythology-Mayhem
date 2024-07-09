using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
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
