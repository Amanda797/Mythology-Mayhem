using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverPuzzle : MonoBehaviour
{
    public Animator anim;

    public bool inRange = false;
    public bool switchOn = false;
  
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (GameManager.instance != null) 
        {
            LoadState(GameManager.instance.gameData.GL2D_Lever);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TwoDSwitchOnOff();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3) 
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer  == 3)
        {
            inRange = false;
        }
    }

    private void TwoDSwitchOnOff()
    {
        if (inRange == true && Input.GetKeyDown("1") && !switchOn)
        {
            anim.Play("LeverAnim");
            switchOn = true;
        }
        else if (inRange == true && Input.GetKeyDown("2") && switchOn)
        {
            anim.Play("LeverOff");
            switchOn = false;
        }

        if (inRange == true && Input.GetKey(KeyCode.E) && !switchOn)
        {
            anim.Play("LeverAnim");
            switchOn = true;
        }
        else if (inRange == true && Input.GetKey(KeyCode.E) && switchOn)
        {
            anim.Play("LeverOff");
            switchOn = false;
        }
    }

    public void LoadState(bool on) 
    {
        if (on)
        {
            anim.Play("LeverAnim");
            switchOn = true;
        }
        else 
        {
            anim.Play("LeverOff");
            switchOn = false;
        }
    }
}
