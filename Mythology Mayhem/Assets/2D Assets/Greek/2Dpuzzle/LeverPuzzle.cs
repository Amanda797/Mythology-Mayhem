using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverPuzzle : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    LeverPuzzleManager leverPuzzleManager;
    Animator anim;
    bool switchOn = false;
    bool canOpen = false;
    [SerializeField] int arrayPos = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        leverPuzzleManager = GetComponentInParent<LeverPuzzleManager>();
    }
    void Start()
    {
        if (GameManager.instance != null)  gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        if (!switchOn) anim.Play("LeverAnim");
        else anim.Play("LeverOff");
    }

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
        if (switchOn) anim.Play("LeverAnim");
        else anim.Play("LeverOff");
        switchOn = !switchOn;
        canOpen = false;
        audioSource.Play();
        leverPuzzleManager.CheckPuzzel(arrayPos, switchOn);
    }
}
