using System;
using System.Linq;
using UnityEngine;

public class CavernLeverPuzzleManager : MonoBehaviour
{

    public GameObject Door;

    public bool[] currentLeverAnswer = new bool[10];
    public bool[] correctLeverAnswer = new bool[] { true, false, true, false, true, false, true, false, true, false, };

    // Start is called before the first frame update
    void Awake()
    {
        Door.GetComponent<DoorCode>().doorOpen = true;
        Door.GetComponent<DoorCode>().blocked = false;
    }

    public void CheckPuzzel(int pos, bool isOn)
    {
        currentLeverAnswer[pos] = isOn;

        if (currentLeverAnswer.SequenceEqual(correctLeverAnswer))
        {
            if (!GameManager.instance.gameData.saveData.playerData.collectedOwl)
            {
                GameManager.instance.gameData.saveData.playerData.collectedOwl = true;
                GameManager.instance.gameData.saveData.Save();
                foreach (CompanionController cc in FindObjectsOfType<CompanionController>()) cc.owl.SetActive(true);
            }
        }
    }
}
