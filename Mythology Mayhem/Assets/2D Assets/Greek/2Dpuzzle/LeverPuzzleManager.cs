using System;
using System.Linq;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    SaveData saveData;
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
        saveData = GameManager.instance.gameData.saveData;
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
        if (!saveData.playerData.collectedMirror)
        {
            itemToDisplay.SetActive(true);
            //saveData.playerData.collectedMirror = true;
            //saveData.Save();
        }
    }
    public void CollectOwl()
    {
        if (!saveData.playerData.collectedOwl)
        {
            saveData.playerData.collectedOwl = true;
            saveData.Save();
            foreach (CompanionController cc in FindObjectsOfType<CompanionController>()) cc.owl.SetActive(true);
        }
    }
}
