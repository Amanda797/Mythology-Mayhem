using System;
using System.Linq;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    GameManager gameManager;
    public DoorCode Door;
    public GameObject itemToDisplay;
    public bool[] currentLeverAnswer = new bool[10];
    public bool[] correctLeverAnswer = new bool[] { true, false, true, false, true, false, true, false, true, false };
    public enum Puzzle
    {
        mirror,
        owl
    }

    public Puzzle puzzle = Puzzle.mirror;
    // Start is called before the first frame update
    void Awake()
    {
        Door.doorOpen = true;
        Door.blocked = false;
    }
    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }

    public void CheckPuzzel(int pos, bool isOn)
    {
        currentLeverAnswer[pos] = isOn;

        if (currentLeverAnswer.SequenceEqual(correctLeverAnswer))
        {
            switch (puzzle)
            {
                case Puzzle.mirror:
                    CollectMirror();
                    break;

                case Puzzle.owl:
                    CollectOwl();
                    break;
            }
        }
    }
    public void CollectMirror()
    {
        if (!gameManager.gameData.collectedMirror)
        {
            itemToDisplay.SetActive(true);
        }
    }
    public void CollectOwl()
    {
        if (!gameManager.gameData.collectedOwl)
        {
            gameManager.gameData.collectedOwl = true;
            gameManager.SaveGame();
            foreach (CompanionController cc in FindObjectsOfType<CompanionController>()) cc.owl.SetActive(true);
        }
    }
}
