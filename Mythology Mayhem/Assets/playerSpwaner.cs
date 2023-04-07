using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class playerSpwaner : MonoBehaviour
{
    int spwanPointIndex;
    int playerIndex;
    public List<Transform> spwanPoints = new List<Transform>();
    public List<GameObject> PlayerPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("spwanPointIndex", 0);
        if(PlayerPrefs.HasKey("spwanPointIndex"))
        {
            spwanPointIndex = PlayerPrefs.GetInt("spwanPointIndex");
        }
        else
        {
            PlayerPrefs.SetInt("spwanPointIndex", 0);
            spwanPointIndex = PlayerPrefs.GetInt("spwanPointIndex");
        }

        if(PlayerPrefs.HasKey("playerIndex"))
        {
            playerIndex = PlayerPrefs.GetInt("playerIndex");
        }
        else
        {
            PlayerPrefs.SetInt("playerIndex", 0);
            playerIndex = PlayerPrefs.GetInt("playerIndex");
        }

        Instantiate(PlayerPrefabs[playerIndex],spwanPoints[spwanPointIndex].position,Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


