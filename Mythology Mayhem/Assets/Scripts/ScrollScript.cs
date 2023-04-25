using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;

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
    [SerializeField] GameObject ScrollPanel;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] string text;
    bool keyTriggered;
    float keyCooldown;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "";
        LoadText();
        keyTriggered = false;
        keyCooldown = 1f;
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
            if(keyTriggered) {
                OpenScroll();
                print("Scroll Opened");
                keyTriggered = false;
            }
        }       
    }//end on collision stay 2d

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            CloseScroll();
        }   
    }//end on collision exit 2d

    public void OpenScroll() {
        ScrollPanel.SetActive(true);
    }//end open scroll

    public void CloseScroll() {
        ScrollPanel.SetActive(false);
    }//end close scroll

    void LoadText() {
        textUI.text = text;
    }//end load text

}
