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
    public GameObject owl, wolf, currentCompanion;
    [SerializeField] int currentCompanionIndex = 0;
    [SerializeField] int cooldown = 1;
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
        if (currentCompanionIndex >= companions.Count) currentCompanionIndex = 0;

        var nextcompanion = companions[currentCompanionIndex];

        if (currentCompanion == nextcompanion.gameObject) return;

        if (nextcompanion.gameObject.name.Contains("Owl"))
        {
            if (gameManager.gameData.collectedOwl)
            {
                // switch to owl
                if (currentCompanion != null) currentCompanion.gameObject.SetActive(false);
                nextcompanion.gameObject.SetActive(true);
                nextcompanion.GetComponent<Companion>()._player = localGameManager.player.gameObject;
                currentCompanion = nextcompanion.gameObject;
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
                nextcompanion.gameObject.SetActive(true);
                nextcompanion.GetComponent<Companion>()._player = localGameManager.player.gameObject;
                currentCompanion = nextcompanion.gameObject;
                StartCoroutine(Cooldown());
            }
            else CallCompanion();
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canCall = true;
    }
}
