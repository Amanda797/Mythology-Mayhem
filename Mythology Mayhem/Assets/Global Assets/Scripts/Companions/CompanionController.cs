using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompanionController : MythologyMayhem
{
    GameManager gameManager;
    public LocalGameManager localGameManager;



    public List<Companion> companions = new List<Companion>();
    public GameObject owl, wolf, currentCompanion, nextcompanion;
    [SerializeField] int currentCompanionIndex = 0;
    [SerializeField] float cooldown = .5f;
    [SerializeField] bool canCall = true;

    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }

    void Update()
    {
        if (gameManager.currentLocalManager != localGameManager) return;
        if (!gameManager.gameData.collectedWolf && !gameManager.gameData.collectedOwl) return;

        if (!canCall) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            canCall = false;

            CallCompanion();
        }
    }
    public void CallCompanion()
    {
        currentCompanionIndex++;
        if (currentCompanionIndex > companions.Count) currentCompanionIndex = 0;
        if (currentCompanionIndex == companions.Count)
        {
            currentCompanion.SetActive(false);
            StartCoroutine(Cooldown());
            return;
        }

        nextcompanion = companions[currentCompanionIndex].gameObject;

        //if (currentCompanion == nextcompanion) return;

        if (nextcompanion.name.Contains("Owl"))
        {
            if (gameManager.gameData.collectedOwl)
            {
                // switch to owl
                if (currentCompanion != null) currentCompanion.gameObject.SetActive(false);
                nextcompanion.SetActive(true);
                nextcompanion.GetComponent<Companion>()._player = localGameManager.player.gameObject;
                currentCompanion = nextcompanion;
                StartCoroutine(Cooldown());
            }
            else CallCompanion();
        }
        else if (nextcompanion.gameObject.name.Contains("Wolf"))
        {
            if (gameManager.gameData.collectedWolf)
            {
                // switch to wolf
                if (currentCompanion != null) currentCompanion.gameObject.SetActive(false);
                nextcompanion.SetActive(true);
                nextcompanion.GetComponent<Companion>()._player = localGameManager.player.gameObject;
                currentCompanion = nextcompanion;
                StartCoroutine(Cooldown());
            }
            else CallCompanion();
        }
        else StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canCall = true;
    }
}
