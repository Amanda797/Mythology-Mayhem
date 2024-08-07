using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// This script manages the Main Menu
public class MainMenuController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterSelection;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] Button continueButton;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        // if there is no save file, disable the continue button.
        if (!System.IO.File.Exists(Application.persistentDataPath + "SaveData.json")) continueButton.enabled = false;
        gameManager.LoadOptionsData();
    }
    public void NewGame()
    {
        audioSource.Play();
        gameManager.LoadSystemsStart(true);
    }

    public void Continue()
    {
        audioSource.Play();
        gameManager.LoadSystemsStart(false);
    }

    public void CharacterSelection ()
    {
        audioSource.Play();
        characterSelection.SetActive(!characterSelection.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void Options()
    {
        audioSource.Play();

        if(options.activeSelf) gameManager.SaveOptionsData();
        options.SetActive(!options.activeSelf);
    }

    public void Credits()
    {
        audioSource.Play();
        credits.SetActive(!credits.activeSelf);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
