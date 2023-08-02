using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
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
    public GameObject item;

    public bool mirrorCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        Door.GetComponent<DoorCode>().doorOpen = true;
        Door.GetComponent<DoorCode>().blocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Lever1.GetComponent<LeverPuzzle>().switchOn == false && Lever2.GetComponent<LeverPuzzle>().switchOn == false && Lever3.GetComponent<LeverPuzzle>().switchOn == true && Lever4.GetComponent<LeverPuzzle>().switchOn == false && Lever5.GetComponent<LeverPuzzle>().switchOn == false && Lever6.GetComponent<LeverPuzzle>().switchOn == true && Lever7.GetComponent<LeverPuzzle>().switchOn == false && Lever8.GetComponent<LeverPuzzle>().switchOn == false && Lever9.GetComponent<LeverPuzzle>().switchOn == true && Lever10.GetComponent<LeverPuzzle>().switchOn == false)
        {
            if(!mirrorCollected)
            {
                mirrorCollected = true;
                item.SetActive(true);
            }
            
        }
    }
}
