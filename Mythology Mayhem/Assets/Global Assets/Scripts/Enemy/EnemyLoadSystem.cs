using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoadSystem : MythologyMayhem
{
    public Level currentLevel;

    public GameObject[] enemies;
    public int tickClock;
    public int syncDataTickAmount;
    private void Start()
    {
        tickClock = syncDataTickAmount;
        if (GameManager.instance != null) 
        {
            bool[] tempEnemyData = GameManager.instance.gameData.FetchBoolArrayData(currentLevel, GameData.BoolArrayType.Enemy);
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
            if (enemies.Length > i) 
            {
                enemies[i].SetActive(enemyData[i]);
            }
        }
    }
    public void SyncToSave() 
    {
        bool[] tempEnemyData = new bool[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            tempEnemyData[i] = enemies[i].activeInHierarchy;
        }

        if (GameManager.instance != null) 
        {
            GameManager.instance.gameData.SaveBoolArrayData(currentLevel, tempEnemyData, GameData.BoolArrayType.Enemy);
        }
    }
}
