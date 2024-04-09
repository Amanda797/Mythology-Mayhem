using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalItemManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] elementalItems;

    public bool reset;


    // Start is called before the first frame update
    void Start()
    {
        if(!reset)
        {
        if(PlayerPrefs.GetInt("appleBool") == 1)
        {
            elementalItems[0].SetActive(false);
        }
        else
        {
            elementalItems[0].SetActive(true);
        }

        if(PlayerPrefs.GetInt("torchBool") == 1)
        {
            elementalItems[1].SetActive(false);
        }
        else
        {
            elementalItems[1].SetActive(true);
        }

        if(PlayerPrefs.GetInt("fishBool") == 1)
        {
            elementalItems[2].SetActive(false);
        }
        else
        {
            elementalItems[2].SetActive(true);
        }

        if(PlayerPrefs.GetInt("airBool") == 1)
        {
            elementalItems[3].SetActive(false);
        }
        else
        {
            elementalItems[3].SetActive(true);
        }
        }
        else
        {
            PlayerPrefs.SetInt("appleBool", 0);
            PlayerPrefs.SetInt("torchBool", 0);
            PlayerPrefs.SetInt("fishBool", 0);
            PlayerPrefs.SetInt("airBool", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
