using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflPieceScript : MonoBehaviour
{
    public int x;
    public int y;
    public TaflPosition.TaflPieces type;
    public TaflTileScript occupying;
    public Renderer rend;

    public void CapturePiece(GameObject ghostToSpawn) 
    {
        GameObject obj = Instantiate(ghostToSpawn, transform.position, transform.rotation);
        
        gameObject.SetActive(false);
    }
}
