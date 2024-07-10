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
    [SerializeField] LeverPuzzleManager leverPuzzleManager;
    public Animator anim;
    public bool switchOn = false;
    bool canOpen = false;
    [SerializeField] int arrayPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null) anim = GetComponent<Animator>();
        else Debug.LogWarning("Animator Missing.");

        if (GameManager.instance != null) 
        {
            gameManager = GameManager.instance;

            //CHECK IF THE PLAYER ALREADY HAS THE OWL. IF SO SET ALL LEVERS TO THE CORRECT POSITION.
        }
        else Debug.LogWarning("GameManager Missing.");

        if (!switchOn) anim.Play("LeverAnim");
        else anim.Play("LeverOff");

        leverPuzzleManager = GetComponentInParent<LeverPuzzleManager>();

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
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("LeverAnim")) return;
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("LeverOff")) return;
        if (switchOn) anim.Play("LeverAnim");
        else anim.Play("LeverOff");
        switchOn = !switchOn;
        canOpen = false;

        leverPuzzleManager.CheckPuzzel(arrayPos, switchOn);
    }
}
