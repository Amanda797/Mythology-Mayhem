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
    bool scrollOpen;

    // --------------------------
    // ***METHODS***
    // --------------------------

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "";
        LoadText();
        scrollOpen = false;
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 5f, 3);         
    }//end update

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            if(Input.GetKeyDown(KeyCode.E)) {
                ToggleScroll();
                print("Scroll Toggled");
            }
        }       
    }//end on trigger 2d

    public void ToggleScroll() {
        scrollOpen = !scrollOpen;
        if(scrollOpen) {
            ScrollPanel.SetActive(true);
        } else {
            ScrollPanel.SetActive(false);
        }
    }//end toggle scroll

    void LoadText() {
        textUI.text = text;
    }//end load text

}
