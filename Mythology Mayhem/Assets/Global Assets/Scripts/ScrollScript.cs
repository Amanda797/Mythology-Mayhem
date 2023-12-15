using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;

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
    [TextArea(7,10)]
    [SerializeField] string text;
    [SerializeField] GameObject pressEText;
    bool keyTriggered;
    bool activeStatus = false;
    float keyCooldown;

    [SerializeField] bool requirements;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        keyTriggered = false;
        keyCooldown = 1f;

        CloseScroll();
    }//end start

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 5f, 3);   

        // Listen for key press and mark as received
        if(Input.GetKeyDown(KeyCode.E)) {
            keyTriggered = true;
        }
        // Begin countdown for key activation period
        if(keyTriggered) {
            keyCooldown -= 1 * Time.deltaTime;
        }
        // Reset key activation and cooldown
        if(keyCooldown <= 0) {
            keyTriggered = false;
            keyCooldown = 1f;
        }
    }//end update

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {        
            //enable tooltip for scroll interaction
            if(pressEText != null) {
                pressEText.SetActive(true);
            }

            if(keyTriggered && !activeStatus) {
                gameObject.GetComponent<AudioSource>().Play();
                OpenScroll();
                keyTriggered = false;
                activeStatus = true;
            } else if(keyTriggered && activeStatus) {
                gameObject.GetComponent<AudioSource>().Play();
                CloseScroll();
                keyTriggered = false;
                activeStatus = false;
            }
        }       
    }//end on collision stay 2d

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            CloseScroll();
        }   
        
        // Destroy "Press E" tooltip
        if(pressEText is var result && result != null) {
            Destroy(result);
        }
    }//end on collision exit 2d

    public void OpenScroll()
    {
        //ScrollController.OpenScroll(text);
    }//end open scroll

    public void CloseScroll() {
        //ScrollController.CloseScroll();
    }//end close scroll

}
