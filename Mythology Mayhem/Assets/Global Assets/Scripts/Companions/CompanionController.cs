using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MythologyMayhem
{

    [SerializeField] GameObject[] companions;
    int currentCompanion = -1; //-1 equals no companion active
    bool callLock = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject pet in companions)
        {
            pet.SetActive(false);
        }       
    }

    public void CallCompanion()
    {
        if(companions.Length >= 1)
        {
            //Disable current companion if set
            if (currentCompanion != -1)
            {
                companions[currentCompanion].SetActive(false);
            }

            //Iterate Companions
            if (currentCompanion + 1 == companions.Length)
            {
                currentCompanion = 0;
            }
            else
            {
                currentCompanion++;
            }

            //Activate next companion
            companions[currentCompanion].SetActive(true);
            
            //Assign player to companion
            if(gameObject.GetComponent<BoxCollider2D>())
            {
                companions[currentCompanion].GetComponent<WolfCompanion>().player2D = gameObject;
            } else if (gameObject.GetComponent<BoxCollider>())
            {
                companions[currentCompanion].GetComponent<WolfCompanion>().player3D = gameObject;
            }
        }
    }

    public void DismissCompanion()
    {
        if (companions.Length >= 1)
        {
            companions[currentCompanion].SetActive(false);
            currentCompanion = -1;
        }
    }

    IEnumerator CallLock(float time)
    {
        callLock = true;
        yield return new WaitForSeconds(time);
        callLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!callLock)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.P))
            {
                Debug.Log("Dismiss Companion");
                DismissCompanion();

                StartCoroutine(CallLock(2f));
            }
            else if (Input.GetKey(KeyCode.P))
            {
                Debug.Log("Call Companion");
                CallCompanion();

                StartCoroutine(CallLock(2f));
            }
        }
        
    }//end update
}
