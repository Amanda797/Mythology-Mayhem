using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLoadSystem : MythologyMayhem
{
    public Level currentLevel;

    public GameObject[] potions;
    public int tickClock;
    public int syncDataTickAmount;
    private void Start()
    {
        tickClock = syncDataTickAmount;
        if (GameManager.instance != null)
        {
            bool[] tempEnemyData = GameManager.instance.gameData.FetchBoolArrayData(currentLevel, GameData.BoolArrayType.Potion);
            if (tempEnemyData != null)
            {
                SyncToLoad(tempEnemyData);
            }
        }
    }
    public void Update()
    {
        tickClock--;
        if (tickClock <= 0)
        {
            SyncToSave();
            tickClock = syncDataTickAmount;
        }
    }
    public void SyncToLoad(bool[] enemyData)
    {
        for (int i = 0; i < enemyData.Length; i++)
        {
            if (potions.Length > i)
            {
                potions[i].SetActive(enemyData[i]);
            }
        }
    }
    public void SyncToSave()
    {
        bool[] tempPotionData = new bool[potions.Length];

        for (int i = 0; i < potions.Length; i++)
        {
            tempPotionData[i] = potions[i].activeInHierarchy;
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.gameData.SaveBoolArrayData(currentLevel, tempPotionData, GameData.BoolArrayType.Potion);
        }
    }
}
