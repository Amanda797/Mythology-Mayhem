using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            if(GameManager.instance.gameData.saveData.playerData.curHealth < 100) {
                other.gameObject.GetComponent<PlayerStats>().Heal(100, false);
            }
        }
    }//end on trigger enter 2d
}
