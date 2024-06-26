using System.Collections;
using UnityEngine;
using TMPro;

public class ScrollScript : MonoBehaviour
{
    // --------------------------
    // ***SECTIONS***
    // - PROPERTIES
    // - METHODS
    // - TESTS
    // --------------------------

    // --------------------------
    // ***PROPERTIES***
    // --------------------------

    [TextArea(7, 10)]
    [SerializeField] string text;
    GameplayUILink gameplayUI;
    GameObject scrollPanel;
    TMP_Text scrollDisplayText;
    AudioSource audioSource;
    GameManager gameManager;

    bool activeStatus = false;
    bool scrollOpen = false;
    bool scrollClosed = true;

    // --------------------------
    // ***METHODS***
    // --------------------------

    void Start()
    {
        // try to find the GameManager object
        if (GameManager.instance != null)
        {
            // if found set variable(s)
            gameManager = GameManager.instance;
        }
        // else display a warning that it is missing
        else Debug.LogWarning("GameManager Missing or Inactive.");

        // try to find the GameplayUILink object
        if (FindObjectOfType<GameplayUILink>() != null)
        {
            // if found set variable(s)
            gameplayUI = FindObjectOfType<GameplayUILink>();
            scrollPanel = gameplayUI.scrollPanel;
            scrollDisplayText = gameplayUI.scrollDisplayText;
        }
        // else display a warning that it is missing
        else Debug.LogWarning("GameplayUILink Missing or Inactive.");

        // try to find the AudioSource object
        if (GetComponent<AudioSource>() != null)
        {
            // if found set variable(s)
            audioSource = GetComponent<AudioSource>();
        }
        // else display a warning that it is missing
        else Debug.LogWarning("AudioSource Missing or Inactive.");
    }

    void Update()
    {
        // if the player is not within the trigger, stop
        if (!activeStatus) return;

        // if the E key was pressed
        if (Input.GetKeyUp(KeyCode.E))
        {
            // if the scroll is currently closed
            if (scrollClosed)
            {
                // open the scroll
                scrollClosed = false;
                StartCoroutine("OpenScroll");
            }
            // if the scroll is currently open
            else if (scrollOpen)
            {
                // close the scroll
                scrollOpen = false;
                CloseScroll();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the object entering the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // activate and display text on the popup ui
            gameManager.Popup("Press E to Read", true);
            activeStatus = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if the object entering the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // deactivate the popup ui
            gameManager.Popup("Press E to Read", false);
            activeStatus = false;
        }
    }

    IEnumerator OpenScroll()
    {
        // play sound, set scroll text and activate the scroll ui
        audioSource.Play();
        scrollDisplayText.text = text;
        scrollPanel.SetActive(true);

        // wait for .1 seconds to update booleans
        yield return new WaitForSeconds(.1f);

        // update booleans
        scrollOpen = true;
        scrollClosed = false;
    }

    public void CloseScroll()
    {
        // play sound, and deactivate the scroll ui
        audioSource.Play();
        scrollPanel.SetActive(false);
        // update booleans
        scrollOpen = false;
        scrollClosed = true;
    }
}



// --------------------------
// ***OLD***
// --------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using System.IO;
//using System.Text.RegularExpressions;
//using UnityEngine.UI;

//public class ScrollScript : MonoBehaviour
//{
//    // --------------------------
//    // ***SECTIONS***
//    // - PROPERTIES
//    // - METHODS
//    // - TESTS
//    // --------------------------

//    // --------------------------
//    // ***PROPERTIES***
//    // --------------------------
//    [TextArea(7,10)]
//    [SerializeField] string text;
//    [SerializeField] GameplayUILink gameplayUI;
//    [SerializeField] GameObject scrollPanel;
//    [SerializeField] TMP_Text scrollDisplayText;
//    [SerializeField] GameObject pressEText;
//    bool keyTriggered;
//    bool activeStatus = false;
//    float keyCooldown;

//    [SerializeField] bool requirements;

//    // --------------------------
//    // ***METHODS***
//    // --------------------------

    //// Start is called before the first frame update
    //void Start()
    //{
    //    keyTriggered = false;
    //    keyCooldown = 1f;

    //    gameplayUI = FindObjectOfType<GameplayUILink>();

    //    if (gameplayUI != null)
    //    {
    //        scrollPanel = gameplayUI.scrollPanel;
    //        scrollDisplayText = gameplayUI.scrollDisplayText;
    //        pressEText = gameplayUI.pressEText;
    //    }

    //    CloseScroll();
    //}//end start

    //void Update()
    //{
    //    Collider2D player = Physics2D.OverlapCircle(transform.position, 5f, 3);   

    //    // Listen for key press and mark as received
    //    if(Input.GetKeyDown(KeyCode.E)) {
    //        keyTriggered = true;
    //    }
    //    // Begin countdown for key activation period
    //    if(keyTriggered) {
    //        keyCooldown -= 1 * Time.deltaTime;
    //    }
    //    // Reset key activation and cooldown
    //    if(keyCooldown <= 0) {
    //        keyTriggered = false;
    //        keyCooldown = 1f;
    //    }
    //}//end update

    //void OnTriggerStay2D(Collider2D other) {
    //    if(other.gameObject.tag == "Player") {        
    //        //enable tooltip for scroll interaction
    //        if(GameManager.instance != null) {
    //            GameManager.instance.Popup("Press E to Read");
    //        }

    //        if(keyTriggered && !activeStatus) {
    //            gameObject.GetComponent<AudioSource>().Play();
    //            OpenScroll();
    //            keyTriggered = false;
    //            activeStatus = true;
    //        } else if(keyTriggered && activeStatus) {
    //            gameObject.GetComponent<AudioSource>().Play();
    //            CloseScroll();
    //            keyTriggered = false;
    //            activeStatus = false;
    //        }
    //    }       
    //}//end on collision stay 2d

    //void OnTriggerExit2D(Collider2D other) {
    //    if(other.gameObject.tag == "Player") {
    //        CloseScroll();
    //    }   

    //    /*
    //    // Destroy "Press E" tooltip
    //    if(pressEText is var result && result != null) {
    //        result.SetActive(false);
    //    }
    //    */
    //}//end on collision exit 2d

    //public void OpenScroll()
    //{
    //    scrollDisplayText.text = text;
    //    scrollPanel.SetActive(true);
    //}//end open scroll

    //public void CloseScroll()
    //{
    //    scrollPanel.SetActive(false);
    //}//end close scroll
//}
