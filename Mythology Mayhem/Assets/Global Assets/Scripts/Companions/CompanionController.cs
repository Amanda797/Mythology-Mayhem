using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompanionController : MythologyMayhem
{
    public GameplayActions gameActions;
    [SerializeField] public Companion[] companions;
    [HideInInspector] public GameObject _player;
    int currentCompanion = -1; //-1 equals no companion active
    bool callLock = false;

    private void Awake()
    {
        gameActions = new GameplayActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.GetComponentInParent<Transform>().gameObject;

        foreach(Companion pet in companions)
        {
            pet._player = _player;
            pet.transform.position = _player.transform.position;
            pet.gameObject.SetActive(false);
        }       
    }

    private void OnEnable()
    {
        gameActions.Player.Enable();
    }
    private void OnDisable()
    {
        gameActions.Player.Disable();
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
            if (gameActions.Player.DismissCompanion.IsPressed())
            {
                Debug.Log("Dismiss Companion");
                DismissCompanion();

                StartCoroutine(CallLock(2f));
            }
            else if (gameActions.Player.CallCompanion.IsPressed())
            {
                Debug.Log("Call Companion");
                CallCompanion();

                StartCoroutine(CallLock(2f));
            }
        }
        
    }//end update
}
