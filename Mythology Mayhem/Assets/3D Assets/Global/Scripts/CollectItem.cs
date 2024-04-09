using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        

        if (playerInventory != null )
        {
            playerInventory.EarthCollect();
            gameObject.SetActive(false);

            /*PedPuzzleItems elements = GetComponent<earth1>();
            earth1 = true;
            PedastalsPuzzleManager earth = GetComponent<apple>();
            apple = true;*/
        }
    }
}
