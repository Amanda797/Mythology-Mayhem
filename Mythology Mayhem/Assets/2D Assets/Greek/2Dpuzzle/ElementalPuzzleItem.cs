using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPuzzleItem : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip clip;
    public PedastalsPuzzleManager puzzleManager;
    public enum Item
    {
        Apple, Torch, Fish, Air
    }
    public ItemTransfer itemTransfer;

    public Item item;
    bool canPickup;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }

    void Update()
    {
        if (!canPickup) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.Popup("", false);
            canPickup = false;
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pickup", true);

            canPickup = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("", false);

            canPickup = false;
        }
    }

    void PickUp()
    {
        switch (item)
        {
            case Item.Apple:
                itemTransfer.apple = true;
                puzzleManager.apple = true;
                break;
            case Item.Torch:
                itemTransfer.torch = true;
                puzzleManager.torch = true;
                break;
            case Item.Fish:
                itemTransfer.fish = true;
                puzzleManager.fish = true;
                break;
            case Item.Air:
                itemTransfer.air = true;
                puzzleManager.air = true;
                break;
        }
        AudioSource source = gameManager.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
