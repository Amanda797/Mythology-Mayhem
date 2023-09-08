using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNextSpawn : MonoBehaviour
{
    public void NextSpawn(int index)
    {
        PlayerPrefs.SetInt("spwanPointIndex", index);
    }
    public void NextCharacter(int index)
    {
        PlayerPrefs.SetInt("playerIndex", index);
    }
}
