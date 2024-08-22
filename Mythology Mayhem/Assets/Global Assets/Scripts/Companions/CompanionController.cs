using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompanionController : MythologyMayhem
{
    GameManager gameManager;
    public LocalGameManager localGameManager;
    public GameplayActions gameActions;
    public List<Companion> companions = new List<Companion>();
    public GameObject _player;
    public GameObject owl, wolf;
    [SerializeField] int currentCompanion = -1; //-1 equals no companion active
    [SerializeField] int callDelay = 1;
    bool callLock = false;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        foreach (LocalGameManager lgm in GameObject.FindObjectsOfType<LocalGameManager>())
        {
            if (lgm.inScene.ToString() == gameObject.scene.name)
            {
                localGameManager = lgm;
            }
        }
        if (!gameManager.gameData.collectedOwl && owl.activeSelf) owl.SetActive(false);
        if (!gameManager.gameData.collectedWolf && wolf.activeSelf) owl.SetActive(false);
    }

    void Update()
    {
        if (gameManager.currentLocalManager != localGameManager) return;
        if (_player == null && localGameManager.player != null) _player = localGameManager.player.gameObject;
        if (_player == null) return;
        if (gameManager.gameData.collectedOwl && !companions.Contains(owl.GetComponent<Companion>())) companions.Add(owl.GetComponent<Companion>());
        if (gameManager.gameData.collectedWolf && !companions.Contains(wolf.GetComponent<Companion>())) companions.Add(wolf.GetComponent<Companion>());
        if (companions.Count <= 0) return;

        //if (gameManager.gameData.collectedOwl && !owl.activeSelf)
        //{
        //    if (currentCompanion != -1)
        //    {
        //        if (companions[currentCompanion].gameObject == owl)
        //        {
        //            if (!owl.GetComponent<Companion>()._health.isDead)
        //            {
        //                owl.SetActive(true);
        //                owl.GetComponent<Companion>()._player = _player;
        //                owl.GetComponent<Companion>().localGameManager = localGameManager;
        //                owl.GetComponent<Companion>()._health = owl.GetComponent<Health>();
        //            }
        //        }
        //    }
        //}
        //else if (!gameManager.gameData.collectedOwl && owl.activeSelf) owl.SetActive(false);

        //if (gameManager.gameData.collectedWolf && !wolf.activeSelf)
        //{
        //    if (currentCompanion != -1)
        //    {
        //        if (companions[currentCompanion].gameObject == wolf)
        //        {
        //            if (!wolf.GetComponent<Companion>()._health.isDead)
        //            {
        //                wolf.SetActive(true);
        //                wolf.GetComponent<Companion>()._player = _player;
        //                wolf.GetComponent<Companion>().localGameManager = localGameManager;
        //                wolf.GetComponent<Companion>()._health = wolf.GetComponent<Health>();
        //            }
        //        }
        //    }
        //}
        //else if (!gameManager.gameData.collectedWolf && wolf.activeSelf) wolf.SetActive(false);

        if(!callLock)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                callLock = true;

                if (currentCompanion != -1)
                {
                    if (companions[currentCompanion].gameObject.activeSelf) DismissCompanion();
                    else CallCompanion();
                }
                else CallCompanion();


                StartCoroutine(CallLock(2f));
            }
        }        
    }
    public void CallCompanion()
    {
        Debug.Log("CallCompanion");
        if (currentCompanion != -1) companions[currentCompanion].gameObject.SetActive(false);

        if (currentCompanion + 1 >= companions.Count) currentCompanion = 0;
        else currentCompanion++;

        if (companions[currentCompanion].gameObject == owl)
        {
            if (gameManager.gameData.collectedOwl)
            {
                owl.SetActive(true);
                owl.GetComponent<Companion>()._player = _player;
                owl.GetComponent<Companion>().localGameManager = localGameManager;
                owl.GetComponent<Companion>()._health = owl.GetComponent<Health>();

                if (owl.GetComponent<Companion>()._health.isDead) owl.SetActive(false);
            }
            else owl.SetActive(false);
        }
        else if (companions[currentCompanion].gameObject == wolf)
        {
            if (gameManager.gameData.collectedWolf)
            {
                wolf.SetActive(true);
                wolf.GetComponent<Companion>()._player = _player;
                wolf.GetComponent<Companion>().localGameManager = localGameManager;
                wolf.GetComponent<Companion>()._health = wolf.GetComponent<Health>();

                if (wolf.GetComponent<Companion>()._health.isDead) wolf.SetActive(false);
            }
            else wolf.SetActive(false);
        }
    }

    public void DismissCompanion()
    {
        Debug.Log("DismissCompanion");
        companions[currentCompanion].gameObject.SetActive(false);
    }

    IEnumerator CallLock(float time)
    {
        yield return new WaitForSeconds(callDelay);
        callLock = false;
    }
}
