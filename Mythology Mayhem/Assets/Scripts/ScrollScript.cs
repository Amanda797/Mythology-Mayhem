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

    public void OpenScroll() {
        ScrollPanel.SetActive(true);
    }//end open scroll

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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Player") {
            OpenScroll();
        }
    }//end on collision enter 2d

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Player") {
            OpenScroll();
        }
    }//end on collision enter

}
