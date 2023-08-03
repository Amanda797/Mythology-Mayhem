using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedastalsPuzzleManager : MonoBehaviour
{

    public GameObject Pedastal1;
    public GameObject Pedastal2;
    public GameObject Pedastal3;
    public GameObject Pedastal4;

    public  bool fish;
    public  bool apple;
    public  bool torch;
    public  bool air;

    
    public GameObject earth;
    public GameObject water;
    public GameObject fire;

    public GameObject door;
    
    //public GameObject SpawnLocation
    public GameObject itemBow;

    // Start is called before the first frame update
    void Awake()
    {
        door.GetComponent<DoorCode>().blocked = true;
        if(PlayerPrefs.GetInt("fishBool") == 1)
        {
            fish = true;
        }
        if(PlayerPrefs.GetInt("appleBool") == 1)
        {
            apple = true;
        }
        if(PlayerPrefs.GetInt("torchBool") == 1)
        {
             torch = true;
        }
        if(PlayerPrefs.GetInt("airBool") == 1)
        {
            air = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Pedastal1.GetComponent<PedastalsPuzzle>().isFishDone && Pedastal2.GetComponent<PedastalsPuzzle>().isFishDone && Pedastal3.GetComponent<PedastalsPuzzle>().isFishDone && Pedastal4.GetComponent<PedastalsPuzzle>().isFishDone)
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
