using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMMPickupScript : MonoBehaviour
{
    public int move;
    public NineMMAnimScript nineMMMain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nineMMMain.SetMove(move);
            this.gameObject.SetActive(false);
        }
    }
}
