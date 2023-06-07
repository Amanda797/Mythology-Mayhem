using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            float waitTime = 1;
            while(other.gameObject.GetComponent<PlayerStats>().CurrHealth < 100) {
                if(waitTime < 0) {
                    other.gameObject.GetComponent<PlayerStats>().Heal(5);
                    waitTime = 1;
                } else {
                    waitTime -= 1 * Time.deltaTime;
                }
            }
        }
    }//end on trigger enter 2d
}
