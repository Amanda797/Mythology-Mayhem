using System;
using System.Linq;
using UnityEngine;

public class CavernLeverPuzzleManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject Door;

    public bool[] currentLeverAnswer = new bool[10];
    public bool[] correctLeverAnswer = new bool[] { true, false, true, false, true, false, true, false, true, false, };

    // Start is called before the first frame update
    void Awake()
    {
        Door.GetComponent<DoorCode>().doorOpen = true;
        Door.GetComponent<DoorCode>().blocked = false;
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
            if (!gameManager.gameData.collectedOwl)
            {
                gameManager.gameData.collectedOwl = true;
                gameManager.SaveGame();
                foreach (CompanionController cc in FindObjectsOfType<CompanionController>()) cc.owl.SetActive(true);
            }
        }
    }
}
