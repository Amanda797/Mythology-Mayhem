using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlacementScript : MonoBehaviour
{

    public GameObject[] Runes;

    public IceCavePuzzleScript puzzleScript;

    public bool[] runesPlaced;

    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }

    }

    void Activate() 
    {
        if (puzzleScript.Rune1) 
        {
            Runes[0].SetActive(true);
            puzzleScript.Rune1 = false;
            runesPlaced[0] = true;
        }
        if (puzzleScript.Rune2)
        {
            Runes[1].SetActive(true);
            puzzleScript.Rune2 = false;
            runesPlaced[1] = true;
        }
        if (puzzleScript.Rune3)
        {
            Runes[2].SetActive(true);
            puzzleScript.Rune3 = false;
            runesPlaced[2] = true;
        }

        if (runesPlaced[0] && runesPlaced[1] && runesPlaced[2]) 
        {
            print("Completed");
            puzzleScript.CompletePuzzle();
        }
    }
}
