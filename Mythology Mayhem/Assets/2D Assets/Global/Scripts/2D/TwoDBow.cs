using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDBow : MonoBehaviour
{
    GameManager gameManager;
    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool deBugBowReset;

    public PedastalsPuzzleManager puzzleManager;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        if (FindObjectOfType<PedastalsPuzzleManager>() != null)
        {
            puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
            puzzleManager.itemBow = this.gameObject;
        }
        else Debug.LogWarning("PedastalsPuzzleManager Missing.");

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
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pick up", true);

            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pick up", false);

            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        pickedUp = true;

        gameManager.gameData.collectedBow = true;
        gameManager.SaveGame();
        if (puzzleManager != null)
        {
            puzzleManager.bowCollected = true;
        }
        gameObject.SetActive(false);
    }

}
