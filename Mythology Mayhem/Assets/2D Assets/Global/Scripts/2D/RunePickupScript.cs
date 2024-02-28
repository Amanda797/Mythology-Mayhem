using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePickupScript : MonoBehaviour
{
    public IceCavePuzzleScript puzzleScript;
    public Rune rune;

    public enum Rune 
    { 
        One,
        Two,
        Three
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (rune) 
            {
                case Rune.One:
                    puzzleScript.Rune1 = true;
                    break;
                case Rune.Two:
                    puzzleScript.Rune2 = true;
                    break;
                case Rune.Three:
                    puzzleScript.Rune3 = true;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
