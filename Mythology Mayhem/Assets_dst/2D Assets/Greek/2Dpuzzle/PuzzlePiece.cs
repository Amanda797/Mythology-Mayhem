using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Piece piece;
    public PuzzlePed ped;
    Rigidbody2D rb;
    Collider2D col;
    SaveScene saveScene;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        PuzzlePed[] peds = FindObjectsOfType<PuzzlePed>();
        saveScene = FindObjectOfType<SaveScene>();
        foreach (PuzzlePed p in peds)
        {
            if (p.key == piece.key)
            {
                ped = p;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(transform.position, ped.target.position));
        if(piece.inPlace)
        {
            rb.isKinematic = true;
            col.enabled = false;
        }
        else
        {
            rb.isKinematic = false;
            col.enabled = true;
        }

        if(ped != null)
        {
            //print(Vector3.Distance(transform.position, ped.target.position));
            if (Vector3.Distance(transform.position, ped.target.position) < ped.snapDistance)
            {
                transform.position = ped.target.position;
                if(!piece.inPlace) saveScene.Save();
                piece.inPlace = true;
                //saveScene.Save();
            }
        }
    }
}
[System.Serializable]
public class Piece
{
    public string key;
    public bool inPlace;
}
