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
    [SerializeField] TextAsset textFile;
    [SerializeField] string[] textLines;
    //to parse sections of scrolls
    //[SerializeField] int[] sections;
    //[SerializeField] int section;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "";
        ParseText();
        LoadText();
    }

    void Update()
    {
        // if any player objects are in the overlap sphere of the scroll, open the scroll
        // if any player objects are not in the overlap sphere of the scroll, close the scroll

        Collider2D player = Physics2D.OverlapCircle(transform.position, 1f);
        if(player != null) {
            if(player.transform.tag == "Player") {
                if(Vector2.Distance(player.transform.position, transform.position) < 1f) {
                    OpenScroll();
                }
                else {
                    CloseScroll();
                }
            }
        }
        else {
            CloseScroll();
        }
        
    }

    public void OpenScroll() {
        ScrollPanel.SetActive(true);
    }//end open scroll

    public void CloseScroll() {
        ScrollPanel.SetActive(false);
    }//end close scroll

    void LoadText() {
        string txt = "";
        for(int i = 0; i < 6; i++) {
            //if(textLines[i] == )
            txt += textLines[i] + "\n";
        }
        textUI.text = txt;
    }//end load text

    void ParseText() {
        textLines = Regex.Split(textFile.text, "\n");
    }//end parse text

    // private void OnTriggerEnter2D(Collision2D other) {
    //     if(other.transform.tag == "Player") {
    //         OpenScroll();
    //     }
    // }//end on collision enter 2d

    // private void OnTriggerEnter(Collision other) {
    //     if(other.transform.tag == "Player") {
    //         OpenScroll();
    //     }
    // }//end on collision enter

    // private void OnTriggerExit2D(Collision2D other) {
    //     if(other.transform.tag == "Player") {
    //         CloseScroll();
    //     }
    // }//end on collision exit 2d

    // private void OnTriggerExit(Collision other) {
    //     if(other.transform.tag == "Player") {
    //         CloseScroll();
    //     }
    // }//end on collision exit

}
