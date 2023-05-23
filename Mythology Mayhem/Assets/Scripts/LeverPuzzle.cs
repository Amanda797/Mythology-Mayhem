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
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true && Input.GetKeyDown("1"))
        {
            anim.Play("LeverAnim");
            switchOn = true;
        }
        else if (inRange == true && switchOn == true && Input.GetKeyDown("2"))
        {
            anim.Play("LeverOff");
            switchOn = false;
        }
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

}
