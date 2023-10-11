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
    [SerializeField] GameObject pressEText;
    bool keyTriggered;
    float keyCooldown;

    [SerializeField] bool requirements;

    // EFFECTS
    [SerializeField] float textSize = 30f;
    [SerializeField] bool textSizeShift = false;
    [SerializeField] float shiftedTextSize;
    [SerializeField] bool bold;
    [SerializeField] bool italics;
    [SerializeField] bool underline;
    [SerializeField] bool strikethrough;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "";
        //print(gameObject.name + ": " + text);
        keyTriggered = false;
        keyCooldown = 1f;

        textUI.fontSize = textSize;
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

            if(keyTriggered && !ScrollPanel.activeSelf) {
                this.gameObject.GetComponent<AudioSource>().Play();
                OpenScroll();
                keyTriggered = false;
            } else if(keyTriggered && ScrollPanel.activeSelf) {
                this.gameObject.GetComponent<AudioSource>().Play();
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
        if(pressEText is var result && result != null) {
            Destroy(result);
        }
    }//end on collision exit 2d

    public void OpenScroll()
    {
        LoadText();
        ScrollPanel.SetActive(true);
        textUI.fontSize = textSize;
        textUI.fontStyle = FontStyles.Normal;
        ScrollRect scroll = textUI.gameObject.GetComponentInParent<ScrollRect>();
        scroll.verticalNormalizedPosition = 1f;
        //FontStyle fontEffect = new FontStyle();
        //fontEffect = FontStyles.Normal;
        if (textSizeShift)
        {
            textUI.fontSize = shiftedTextSize;
        }
        if(bold)
        {
            textUI.fontStyle = FontStyles.Bold;
        }
        if (italics)
        {
            textUI.fontStyle = FontStyles.Italic;
        }
        if (underline)
        {
            textUI.fontStyle = FontStyles.Underline;
        }
        if (strikethrough)
        {
            textUI.fontStyle = FontStyles.Strikethrough;
        }
    }//end open scroll

    public void CloseScroll() {
        ScrollPanel.SetActive(false);
    }//end close scroll

    void LoadText() {
        textUI.text = this.text;
    }//end load text

}
