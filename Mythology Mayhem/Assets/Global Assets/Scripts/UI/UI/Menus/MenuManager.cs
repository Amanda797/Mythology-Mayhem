using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the Pause Menu
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseParent;
    [SerializeField] GameObject scrollPnl;
    [SerializeField] GameObject pausePnl;
    [SerializeField] GameObject optionsPnl;
    [SerializeField] GameObject creditsPnl;
    [SerializeField] GameObject helpPnl;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] Animator animator;
    public TMP_Text scrollDisplayText;
    public AudioSource menuEffectSource;

    void Update()
    {
        //TODO: prevent while in main menu and cutscenes.
        if(Input.GetKeyDown(KeyCode.P)) TogglePause(false);
    }

    public void ToggleOptions()
    {
        if (!optionsPnl.activeSelf) StartCoroutine(ChangeMenu("Options"));
        else StartCoroutine(ChangeMenu("Pause Menu"));
    }

    public void ToggleCredits()
    {
        if (!creditsPnl.activeSelf) StartCoroutine(ChangeMenu("Credits"));
        else StartCoroutine(ChangeMenu("Pause Menu"));
    }

    public void ToggleHelp()
    {
        if (!helpPnl.activeSelf) StartCoroutine(ChangeMenu("Help"));
        else StartCoroutine(ChangeMenu("Pause Menu"));
    }

    public void TogglePause(bool isScroll)
    {
        if (pauseParent.activeSelf == false)
        {
            pauseParent.SetActive(true);
            pausePnl.SetActive(false);
            optionsPnl.SetActive(false);
            creditsPnl.SetActive(false);
            helpPnl.SetActive(false);
            scrollPnl.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            menuEffectSource.Play();
            animator.Play("scroll", 0);
            if (!isScroll) StartCoroutine(OpenPauseMenu());
            else StartCoroutine(OpenScroll());
        }
        else
        {
            pausePnl.SetActive(false);
            optionsPnl.SetActive(false);
            creditsPnl.SetActive(false);
            helpPnl.SetActive(false);
            scrollPnl.SetActive(false);
            menuEffectSource.Play();
            animator.Play("Scroll_Close", 0);
            StartCoroutine(ClosePauseMenu());
        }
    }

    public void MainMenu()
    {
        StartCoroutine(ChangeMenu("Main Menu"));
    }

    IEnumerator OpenScroll()
    {
        yield return new WaitForSecondsRealtime(1);

        scrollPnl.SetActive(true);
    }

    public void CloseScroll()
    {
        scrollPnl.SetActive(false);
        TogglePause(true);
    }

    IEnumerator OpenPauseMenu()
    {
        yield return new WaitForSecondsRealtime(1);

        pausePnl.SetActive(true);
    }
    IEnumerator ClosePauseMenu()
    {
        yield return new WaitForSecondsRealtime(1);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseParent.SetActive(false);
    }
    IEnumerator ChangeMenu(string name)
    {
        menuEffectSource.Play();
        animator.Play("Scroll_Close", 0);
        optionsPnl.SetActive(false);
        creditsPnl.SetActive(false);
        helpPnl.SetActive(false);
        pausePnl.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        switch (name)
        {
            case "Help":
                menuEffectSource.Play();
                animator.Play("scroll");
                yield return new WaitForSecondsRealtime(1);
                helpPnl.SetActive(true);
                break;

            case "Options":
                menuEffectSource.Play();
                animator.Play("scroll");
                yield return new WaitForSecondsRealtime(1);
                optionsPnl.SetActive(true);
                break;

            case "Credits":
                menuEffectSource.Play();
                animator.Play("scroll");
                yield return new WaitForSecondsRealtime(1);
                creditsPnl.SetActive(true);
                break;

            case "Main Menu":
                Time.timeScale = 1;
                GameManager.instance.playerControllers.Clear();
                GameManager.instance.loadedLocalManagers.Clear();
                GameManager.instance.inMainMenu = true;
                GameManager.instance.huic.heartState.gameObject.SetActive(false);
                SceneManager.LoadScene(1);
                gameplayUI.SetActive(false);
                break;

            case "Pause Menu":
                menuEffectSource.Play();
                animator.Play("scroll");
                yield return new WaitForSecondsRealtime(1);
                pausePnl.SetActive(true);
                break;

            default:
                break;
        }
    }
}
