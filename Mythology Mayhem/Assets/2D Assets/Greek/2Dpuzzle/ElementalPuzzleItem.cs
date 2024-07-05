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

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.tag == "Player")
        {
            switch (item)
            {
                case Item.Apple:
                    itemTransfer.apple = true;
                    puzzleManager.apple = true;
                    break;
                case Item.Torch:
                    itemTransfer.torch = true;
                    puzzleManager.torch = true;
                    break;
                case Item.Fish:
                    itemTransfer.fish = true;
                    puzzleManager.fish = true;
                    break;
                case Item.Air:
                    itemTransfer.air = true;
                    puzzleManager.air = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
