using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernLeverPuzzleManager : MonoBehaviour
{
    public GameObject Lever1;
    public GameObject Lever2;
    public GameObject Lever3;
    public GameObject Lever4;
    public GameObject Lever5;
    public GameObject Lever6;
    public GameObject Lever7;
    public GameObject Lever8;
    public GameObject Lever9;
    public GameObject Lever10;
    public GameObject Door;
    //public GameObject SpawnLocation;
    public GameObject owl;

    public Transform owlSpawnPos;
    public bool owlSpawned = false;
    public bool resetOwl;
    
    // Start is called before the first frame update
    void Awake()
    {
        Door.GetComponent<DoorCode>().doorOpen = true;
        Door.GetComponent<DoorCode>().blocked = false;


        if(resetOwl == true)
        {
            PlayerPrefs.SetInt("owl", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Lever1.GetComponent<LeverPuzzle>().switchOn == true /*&& Lever2.GetComponent<LeverPuzzle>().switchOn == false && Lever3.GetComponent<LeverPuzzle>().switchOn == true && Lever4.GetComponent<LeverPuzzle>().switchOn == false && Lever5.GetComponent<LeverPuzzle>().switchOn == true && Lever6.GetComponent<LeverPuzzle>().switchOn == false && Lever7.GetComponent<LeverPuzzle>().switchOn == true && Lever8.GetComponent<LeverPuzzle>().switchOn == false && Lever9.GetComponent<LeverPuzzle>().switchOn == true && Lever10.GetComponent<LeverPuzzle>().switchOn == false*/)
        {
            if(owlSpawned == false && PlayerPrefs.GetInt("owl") == 0)
            {
                PlayerPrefs.SetInt("owl", 1);
                Instantiate(owl, owlSpawnPos.position, Quaternion.identity);
                owlSpawned = true;
            }
        }
    }
}
