using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MythologyMayhem;

public class PotionLoadSystem : MythologyMayhem
{
    public Level currentLevel;

    public GameObject[] potions;
    bool[] tempPotionData;
    private void Start()
    {
        if (GameManager.instance != null)
        {
            tempPotionData = GameManager.instance.gameData.FetchBoolArrayData(currentLevel, GameData.BoolArrayType.Potion);

            if (tempPotionData.Length != 0) SyncToLoad(tempPotionData);
            else
            {
                tempPotionData = new bool[potions.Length];

                for (int i = 0; i < tempPotionData.Length; i++)
                {
                    potions[i].SetActive(true);
                    tempPotionData[i] = true;
                }
            }
        }
    }
    public void SyncToLoad(bool[] potionData)
    {
        for (int i = 0; i < potionData.Length; i++)
        {
            potions[i].SetActive(potionData[i]);
        }
    }
    public void SyncToSave(GameObject potion)
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i] == potion)
            {
                tempPotionData[i] = false;

                GameManager.instance.gameData.SaveBoolArrayData(currentLevel, tempPotionData, GameData.BoolArrayType.Potion);
                return;
            }
        }
    }
}
