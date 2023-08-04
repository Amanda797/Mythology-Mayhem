using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPuzzleItem : MonoBehaviour
{

    public enum Item
    {
        Apple, Torch, Fish, Air
    }
    public ItemTransfer itemTransfer;

    public Item item;

    private void OnTriggerEnter(Collider other) 
    {

        if(other.tag != "Player" )
        {
            return;
        }

        switch(item)
        {
            case Item.Apple:
            
                
                itemTransfer.apple = true;
                PlayerPrefs.SetInt("appleBool", itemTransfer.apple ? 1 :0);
                
                break;
             case Item.Torch:

                
                    itemTransfer.torch = true;
                    PlayerPrefs.SetInt("torchBool", itemTransfer.torch ? 1 :0);
                
                break;
             case Item.Fish:
                
                
                    itemTransfer.fish = true;
                    PlayerPrefs.SetInt("fishBool", itemTransfer.fish ? 1 :0);
                
                break;
             case Item.Air:
               
                
                    itemTransfer.air = true;
                    PlayerPrefs.SetInt("airBool", itemTransfer.air ? 1 :0);
                
                break;
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    /*void Start()
    {
                switch(item)
        {
            case Item.Apple:
                if(GameObject.Find("Apple"))
                {
                    itemTransfer.apple = false;
                    PlayerPrefs.SetInt("appleBool", itemTransfer.apple ? 1 :0);
                }
                break;
             case Item.Torch:
                if(GameObject.Find("Torch"))
                {
                    itemTransfer.torch = false;
                    PlayerPrefs.SetInt("torchBool", itemTransfer.torch ? 1 :0);
                }
                break;
             case Item.Fish:
                if(GameObject.Find("Fish"))
                {
                    itemTransfer.fish = false;
                    PlayerPrefs.SetInt("fishBool", itemTransfer.fish ? 1 :0);
                }
                break;
             case Item.Air:
                if(GameObject.Find("Air"))
                {
                    itemTransfer.air = false;
                    PlayerPrefs.SetInt("airBool", itemTransfer.air ? 1 :0);
                }
                break;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
