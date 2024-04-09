using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPuzzleItem : MonoBehaviour
{
    public PedastalsPuzzleManager puzzleManager;
    public enum Item
    {
        Apple, Torch, Fish, Air
    }
    public ItemTransfer itemTransfer;

    public Item item;

    void Update()
    {
        if(puzzleManager == null)
        {
            FindPuzzleManager();
        }
    }

    void FindPuzzleManager()
    {
        PedastalsPuzzleManager[] puzzleManagers = FindObjectsOfType<PedastalsPuzzleManager>();
        foreach (var tempManager in puzzleManagers)
        {
            if(tempManager.gameObject.scene.buildIndex == -1)
            {
                puzzleManager = tempManager;
                switch (item)
                {
                    case Item.Apple:
                        if(puzzleManager.apple)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    case Item.Torch:
                        if(puzzleManager.torch)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    case Item.Fish:
                        if(puzzleManager.fish)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    case Item.Air:
                        if(puzzleManager.air)
                        {
                            Destroy(gameObject);
                        }
                        break;
                }
            }
        }
    }

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
                //PlayerPrefs.SetInt("appleBool", itemTransfer.apple ? 1 :0);
                puzzleManager.apple = true;
                
                break;
             case Item.Torch:

                
                    itemTransfer.torch = true;
                    //PlayerPrefs.SetInt("torchBool", itemTransfer.torch ? 1 :0);
                    puzzleManager.torch = true;
                
                break;
             case Item.Fish:
                
                
                    itemTransfer.fish = true;
                    //PlayerPrefs.SetInt("fishBool", itemTransfer.fish ? 1 :0);
                    puzzleManager.fish = true;
                
                break;
             case Item.Air:
               
                
                    itemTransfer.air = true;
                    //PlayerPrefs.SetInt("airBool", itemTransfer.air ? 1 :0);
                    puzzleManager.air = true;
                
                break;
        }

        Destroy(gameObject);
    }
}
