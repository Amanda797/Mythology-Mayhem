using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflBoardPickup : MonoBehaviour
{
    public int move;
    public TaflAnimatedScript taflMain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            taflMain.SetMove(move);
            this.gameObject.SetActive(false);
        }
    }
}
