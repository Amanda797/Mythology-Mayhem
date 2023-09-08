using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDBow : MonoBehaviour
{

    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool deBugBowReset;

    public PedastalsPuzzleManager puzzleManager;
    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
        puzzleManager.itemBow = this.gameObject;

        if (puzzleManager.fishDone && puzzleManager.appleDone && puzzleManager.torchDone && puzzleManager.airDone && !puzzleManager.bowCollected)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpAllowed == true && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        pickedUp = true;

        if(puzzleManager != null)
        {
            puzzleManager.bowCollected = true;
        }
        gameObject.SetActive(false);
    }

}
