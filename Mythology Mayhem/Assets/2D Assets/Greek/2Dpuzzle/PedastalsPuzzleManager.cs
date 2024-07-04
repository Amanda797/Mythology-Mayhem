using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedastalsPuzzleManager : MonoBehaviour
{
    public  bool fish;
    public  bool apple;
    public  bool torch;
    public  bool air;

    public bool fishDone;
    public bool appleDone;
    public bool torchDone;
    public bool airDone;

    public GameObject door;
    
    //public GameObject SpawnLocation
    public GameObject itemBow;
    public bool bowCollected;

    // Start is called before the first frame update
    void Awake()
    {
        PedastalsPuzzleManager[] puzzleManagers = FindObjectsOfType<PedastalsPuzzleManager>();
        //Debug.Log(puzzleManagers.Length);
        //if(puzzleManagers.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateDoor()
    {
        if(fishDone && appleDone && torchDone && airDone)
        {
            door.GetComponent<DoorCode>().doorOpen = true;
            door.GetComponent<DoorCode>().blocked = false;
            
            if(itemBow.GetComponent<TwoDBow>().pickedUp == false)
            {
                itemBow.SetActive(true);
            }
        }
    }

}
