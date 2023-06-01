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
    [SerializeField] GameObject ScrollPanel;
    [SerializeField] TextMeshProUGUI textUI;
    [TextArea(7,10)]
    [SerializeField] string text;
    [SerializeField] Image pressEText;
    bool keyTriggered;
    float keyCooldown;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "";
        print(gameObject.name + ": " + text);
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
            //enable tooltip for scroll interaction
            if(pressEText != null) {
                pressEText.transform.gameObject.SetActive(true);
            }

            if(keyTriggered && !ScrollPanel.activeSelf) {
                OpenScroll();
                keyTriggered = false;
            } else if(keyTriggered && ScrollPanel.activeSelf) {
                CloseScroll();
                keyTriggered = false;
            }
        }       
    }//end on collision stay 2d

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            CloseScroll();
        }   
        
        // Destroy "Press E" tooltip
        if(GameObject.Find("PressE_Text") is var result && result != null) {
            Destroy(result);
        }
    }//end on collision exit 2d

    public void OpenScroll() {        
        LoadText();
        ScrollPanel.SetActive(true);
    }//end open scroll

    public void CloseScroll() {
        ScrollPanel.SetActive(false);
    }//end close scroll

    void LoadText() {
        textUI.text = this.text;
    }//end load text

}
