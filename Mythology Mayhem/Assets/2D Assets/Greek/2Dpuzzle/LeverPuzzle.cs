using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverPuzzle : MonoBehaviour
{
    GameManager gameManager;
    public Animator anim;

    public bool inRange = false;
    public bool switchOn = false;
    bool canOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null) anim = GetComponent<Animator>();
        else Debug.LogWarning("Animator Missing.");

        if (GameManager.instance != null) 
        {
            gameManager = GameManager.instance;
            LoadState(GameManager.instance.gameData.GL2D_Lever);
        }
        else Debug.LogWarning("GameManager Missing.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpen) return;
        if (Input.GetKeyDown(KeyCode.E)) TwoDSwitchOnOff();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pull Lever", true);

            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pull Lever", false);

            canOpen = false;
        }
    }

    private void TwoDSwitchOnOff()
    {
        if (!switchOn) anim.Play("LeverAnim");
        else anim.Play("LeverOff");
        switchOn = !switchOn;
    }

    public void LoadState(bool on) 
    {
        if (on) anim.Play("LeverAnim");
        else anim.Play("LeverOff");
        switchOn = on;
    }
}
