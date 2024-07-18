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
    [SerializeField] int callDelay = 1;
    bool callLock = false;

    private void Awake()
    {
        gameActions = new GameplayActions();
    }

    private void Start()
    {
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
        Debug.Log("CallCompanion");
        if(companions.Length >= 1)
        {
            //Disable current companion if set
            if (currentCompanion != -1) companions[currentCompanion].gameObject.SetActive(false);

            if (currentCompanion + 1 >= companions.Length) currentCompanion = 0;
            else currentCompanion++;

            if (companions[currentCompanion].gameObject == owl)
            {
                if (GameManager.instance.gameData.saveData.playerData.collectedOwl) companions[currentCompanion].gameObject.SetActive(true);
                else companions[currentCompanion].gameObject.SetActive(false);
            }
            else if (companions[currentCompanion].gameObject == wolf)
            {
                if (GameManager.instance.gameData.saveData.playerData.collectedWolf) companions[currentCompanion].gameObject.SetActive(true);
                else companions[currentCompanion].gameObject.SetActive(false);
            }
        }
    }

    public void DismissCompanion()
    {
        Debug.Log("DismissCompanion");
        if (companions.Length >= 1)
        {
            companions[currentCompanion].gameObject.SetActive(false);
            currentCompanion = -1;
        }
    }

    IEnumerator CallLock(float time)
    {
        yield return new WaitForSeconds(callDelay);
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
                callLock = true;
                DismissCompanion();

                StartCoroutine(CallLock(2f));
            }
            else if (gameActions.Player.CallCompanion.IsPressed())
            {
                callLock = true;
                CallCompanion();

                StartCoroutine(CallLock(2f));
            }
        }
        
    }//end update
}
