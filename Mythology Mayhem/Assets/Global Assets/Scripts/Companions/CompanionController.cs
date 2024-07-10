using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompanionController : MythologyMayhem
{
    public LocalGameManager localGameManager;
    public GameplayActions gameActions;
    public Companion[] companions;
    public GameObject _player;
    public GameObject owl, wolf;
    int currentCompanion = -1; //-1 equals no companion active
    bool callLock = false;

    private void Awake()
    {
        gameActions = new GameplayActions();
        Debug.Log("Awake");

        foreach (LocalGameManager lgm in GameObject.FindObjectsOfType<LocalGameManager>())
        {
            if (lgm.inScene.ToString() == gameObject.scene.name)
            {
                localGameManager = lgm;
            }
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
        if (_player == null && localGameManager.player != null)
            {
                _player = localGameManager.player.gameObject;
                if (companions.Length > 0)
                {
                    foreach (var companion in companions)
                    {
                        if (companion.name.Contains("Owl")) owl = companion.gameObject;
                        if (companion.name.Contains("Wolf")) wolf = companion.gameObject;
                    }
                    if (owl != null)
                    {
                        owl.SetActive(true);
                        owl.GetComponent<Companion>()._player = _player;
                        if (!GameManager.instance.gameData.saveData.playerData.collectedOwl) owl.SetActive(false);
                    }
                    if (wolf != null)
                    {
                        wolf.SetActive(true);
                        wolf.GetComponent<Companion>()._player = _player;
                        if (!GameManager.instance.gameData.saveData.playerData.collectedWolf) wolf.SetActive(false);
                    }
                }
            }


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
