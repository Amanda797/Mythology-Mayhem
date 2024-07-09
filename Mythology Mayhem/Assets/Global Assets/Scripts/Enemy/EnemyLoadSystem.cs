using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MythologyMayhem;

public class EnemyLoadSystem : MythologyMayhem
{
    public Level currentLevel;

    public GameObject[] enemies;
    bool[] tempEnemyData;
    private void Start()
    {
        if (GameManager.instance != null)
        {
            tempEnemyData = GameManager.instance.gameData.FetchBoolArrayData(currentLevel, GameData.BoolArrayType.Enemy);

            if (tempEnemyData.Length != 0) SyncToLoad(tempEnemyData);
            else
            {
                tempEnemyData = new bool[enemies.Length];

                for (int i = 0; i < tempEnemyData.Length; i++)
                {
                    enemies[i].SetActive(true);
                    tempEnemyData[i] = true;
                }
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponentInChildren<Health>().loadSystem = this;
        }
    }
    public void SyncToLoad(bool[] enemyData)
    {
        Debug.Log("enemyData: " + enemyData.Length);

        for (int i = 0; i < enemyData.Length; i++)
        {
            enemies[i].GetComponentInChildren<Health>().loadSystem = this;
            enemies[i].SetActive(enemyData[i]);
        }
    }
    public void SyncToSave(GameObject enemy)
    {
        Debug.Log("SyncToSave");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == enemy)
            {
                tempEnemyData[i] = false;

                GameManager.instance.gameData.SaveBoolArrayData(currentLevel, tempEnemyData, GameData.BoolArrayType.Enemy);

                bool[] asd = GameManager.instance.gameData.FetchBoolArrayData(currentLevel, GameData.BoolArrayType.Enemy);
                Debug.Log("asd: " + asd.Length);
                foreach (bool s in asd) Debug.Log(s);
                return;
            }
        }
    }
}
