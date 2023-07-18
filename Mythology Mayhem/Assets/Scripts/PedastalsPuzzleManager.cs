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

    //public GameObject SpawnLocation;
    public GameObject earth;
    public GameObject water;
    public GameObject fire;
    
    //public PlayerInventory elementalCheck;




    // Start is called before the first frame update
    void Awake()
    {
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
    /*void Update()
    {
        PlayerInventory elementalCheck = GetComponent<earthCollected>();


        if (EarthCollect == true)
        {
            earth.SetActive(true);
        }
        else
        {
            earth.SetActive(false);
        }
    }*/
}
