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
        textUI.text = text;
    }//end load text

}
