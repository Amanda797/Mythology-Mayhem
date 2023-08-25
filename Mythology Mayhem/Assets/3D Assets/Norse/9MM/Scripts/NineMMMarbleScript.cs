using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMMMarbleScript : MonoBehaviour
{
    public int x;
    public int y;
    public NineMMPosition.NineMMMarble type;
    public NineMMSlotScript occupying;
    public Renderer rend;

    public void CapturePiece(GameObject ghostToSpawn)
    {
        /*
        GameObject obj = Instantiate(ghostToSpawn, transform.position, transform.rotation);

        print("Captured Piece " + name);
        */
        gameObject.SetActive(false);
        
    }
}
