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

    public bool[] rightPuzzle;

    public bool puzzleCompleted = false;

    public Vector3 threeDLever1Pos;
    public Vector3 threeDLever2Pos;
    public Vector3 threeDLever3Pos;
    public Vector3 threeDLever4Pos;
    public Vector3 threeDLever5Pos;
    public Vector3 threeDLever6Pos;
    public Vector3 threeDLever7Pos;
    public Vector3 threeDLever8Pos;
    public Vector3 threeDLever9Pos;
    public Vector3 threeDLever10Pos;

    



  
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Get3DPosition();
        

    }

    // Update is called once per frame
    void Update()
    {
        TwoDSwitchOnOff();
        PuzzleCheckPosition1();
        PuzzleCheckPosition2();
        PuzzleCheckPosition3();
        PuzzleCheckPosition4();
        PuzzleCheckPosition5();
        PuzzleCheckPosition6();
        PuzzleCheckPosition7();
        PuzzleCheckPosition8();
        PuzzleCheckPosition9();
        PuzzleCheckPosition10();
        PuzzleCheck(); 

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
    }

    private void Get3DPosition()
    {
        threeDLever1Pos = transform.position;

        threeDLever1Pos.z = 180;

        transform.position = threeDLever1Pos; 


        threeDLever2Pos = transform.position;
        
        threeDLever2Pos.z = 0;

        transform.position =threeDLever2Pos;


        threeDLever3Pos = transform.position;

        threeDLever3Pos.z = 180;

        transform.position = threeDLever3Pos; 


        threeDLever4Pos = transform.position;

        threeDLever4Pos.z = 0;

        transform.position = threeDLever4Pos; 

        
        threeDLever5Pos = transform.position;

        threeDLever5Pos.z = 180;

        transform.position = threeDLever5Pos; 


        threeDLever6Pos = transform.position;

        threeDLever6Pos.z = 0;

        transform.position = threeDLever6Pos; 


        threeDLever7Pos = transform.position;

        threeDLever7Pos.z = 180;

        transform.position = threeDLever7Pos; 


        threeDLever8Pos = transform.position;

        threeDLever8Pos.z = 0;

        transform.position = threeDLever8Pos; 


        threeDLever9Pos = transform.position;

        threeDLever9Pos.z = 180;

        transform.position = threeDLever9Pos;


        threeDLever10Pos = transform.position;

        threeDLever10Pos.z = 0;

        transform.position = threeDLever10Pos;  

    }

    private void PuzzleCheckPosition1()
    {
        if(threeDLever1Pos.z == 180 && switchOn == true)
            {
                rightPuzzle[0] = true;
            } 
        else if(threeDLever1Pos.z == 180 && switchOn == false)
            {
                rightPuzzle[0] = false;
            }
    }

    private void PuzzleCheckPosition2()
    {
        if(threeDLever2Pos.z == 0 && switchOn == false)
            {
                rightPuzzle[1] = true;
            }
        else if(threeDLever2Pos.z == 0 && switchOn == true)
            {
                rightPuzzle[1] = false;
            }
    }

    private void PuzzleCheckPosition3()
    {
        if(threeDLever3Pos.z == 180 && switchOn == true)
            {
                rightPuzzle[2] = true;
            }
        else
            {
                rightPuzzle[2] = false;
            }
    }

     private void PuzzleCheckPosition4()
    {
         if(threeDLever4Pos.z == 0 && switchOn == false)
            {
                rightPuzzle[3] = true;
            }
        else
            {
                rightPuzzle[3] = false;
            }
    }

     private void PuzzleCheckPosition5()
    {
               if(threeDLever5Pos.z == 180 && switchOn == true)
            {
                rightPuzzle[4] = true;
            }
        else
            {
                rightPuzzle[4] = false;
            }
    }

     private void PuzzleCheckPosition6()
    {
        if(threeDLever6Pos.z == 0 && switchOn == false)
            {
                rightPuzzle[5] = true;
            }
        else
            {
                rightPuzzle[5] = false;
            } 
    }

     private void PuzzleCheckPosition7()
    {
        if(threeDLever7Pos.z == 180 && switchOn == true)
            {
                rightPuzzle[6] = true;
            }
        else
            {
                rightPuzzle[6] = false;
            }
    }

     private void PuzzleCheckPosition8()
    {
           if(threeDLever8Pos.z == 0 && switchOn == false)
            {
                rightPuzzle[7] = true;
            }
        else
            {
                rightPuzzle[7] = false;
            }
    }

     private void PuzzleCheckPosition9()
    {
        
        if(threeDLever9Pos.z == 180 && switchOn == true)
            {
                rightPuzzle[8] = true;
            }
        else
            {
                rightPuzzle[8] = false;
            }
    }

     private void PuzzleCheckPosition10()
    {
            if(threeDLever10Pos.z == 0 && switchOn == false)
            {
                rightPuzzle[9] = true;
            }
        else
            {
                rightPuzzle[9] = false;
            }
    }

    private void PuzzleCheck()
    {
        if(rightPuzzle[0] == true && rightPuzzle[1] == true && rightPuzzle[2] == true && rightPuzzle[3] == true && rightPuzzle[4] == true && rightPuzzle[5] == true && rightPuzzle[6] == true && rightPuzzle[7] == true && rightPuzzle[8] == true && rightPuzzle[9] == true) 
        {
            puzzleCompleted =true;
        }
        else
        {
            puzzleCompleted = false;
        }
    }

}
