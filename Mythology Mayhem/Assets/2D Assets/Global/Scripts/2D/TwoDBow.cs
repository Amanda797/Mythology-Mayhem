using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDBow : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip clip;
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
            gameObject.SetActive(false);
        }
        else Debug.LogWarning("PedastalsPuzzleManager Missing.");
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
        AudioSource source = gameManager.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        gameManager.gameData.collectedBow = true;
        gameManager.SaveGame();
        gameObject.SetActive(false);
    }

}
