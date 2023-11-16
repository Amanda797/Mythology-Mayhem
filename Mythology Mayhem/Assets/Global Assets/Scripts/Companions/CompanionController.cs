using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MythologyMayhem
{
    [SerializeField] public Companion[] companions;
    [HideInInspector] public GameObject _player;
    int currentCompanion = -1; //-1 equals no companion active
    bool callLock = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Companion pet in companions)
        {
            pet.gameObject.SetActive(false);
            pet._player = _player;
        }       
    }

    public void CallCompanion()
    {
        if(companions.Length >= 1)
        {
            //Disable current companion if set
            if (currentCompanion != -1)
            {
                companions[currentCompanion].gameObject.SetActive(false);
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
            companions[currentCompanion].gameObject.SetActive(true);
        }
    }

    public void DismissCompanion()
    {
        if (companions.Length >= 1)
        {
            companions[currentCompanion].gameObject.SetActive(false);
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
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Dismiss Companion");
                DismissCompanion();

                StartCoroutine(CallLock(2f));
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Call Companion");
                CallCompanion();

                StartCoroutine(CallLock(2f));
            }
        }
        
    }//end update
}
