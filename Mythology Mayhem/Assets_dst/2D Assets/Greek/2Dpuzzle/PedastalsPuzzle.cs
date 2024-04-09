using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedastalsPuzzle : MonoBehaviour
{
    private PedastalsPuzzleManager puzzleManager;
    public ElementalPuzzleItem.Item item;


    public SpriteRenderer elementalIcon;
    [SerializeField] private bool isPlayerInRange = false;


    // Start is called before the first frame update
    void Awake()
    {
        puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
        if (puzzleManager != null)
        {
            switch (item)
            {
                case ElementalPuzzleItem.Item.Fish:
                    if (puzzleManager.fishDone)
                    {
                        elementalIcon.color = new Color(1, 1, 1);
                    }
                    break;
                case ElementalPuzzleItem.Item.Apple:
                    if (puzzleManager.appleDone)
                    {
                        elementalIcon.color = new Color(1, 1, 1);
                    }
                    break;
                case ElementalPuzzleItem.Item.Torch:
                    if (puzzleManager.torchDone)
                    {
                        elementalIcon.color = new Color(1, 1, 1);
                    }
                    break;
                case ElementalPuzzleItem.Item.Air:
                    if (puzzleManager.airDone)
                    {
                        elementalIcon.color = new Color(1, 1, 1);
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key is pressed!");
            if (isPlayerInRange)
            {
                if(item == ElementalPuzzleItem.Item.Fish && puzzleManager.fish && !puzzleManager.fishDone)
                {
                    Debug.Log("Fish is true!");
                    elementalIcon.color = new Color(1, 1, 1);
                    puzzleManager.fishDone = true;
                    
                }
                else if(item == ElementalPuzzleItem.Item.Apple && puzzleManager.apple && !puzzleManager.appleDone )
                {
                    elementalIcon.color = new Color(1, 1, 1);
                    puzzleManager.appleDone = true;
                }
                else if(item == ElementalPuzzleItem.Item.Torch && puzzleManager.torch && !puzzleManager.torchDone)
                {
                    elementalIcon.color = new Color(1, 1, 1);
                    puzzleManager.torchDone = true;
                }
                else if(item == ElementalPuzzleItem.Item.Air && puzzleManager.air && !puzzleManager.airDone)
                {
                    elementalIcon.color = new Color(1, 1, 1);
                    puzzleManager.airDone = true;
                }
                puzzleManager.UpdateDoor();
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag != "Player")
        {
            return;
        }
        else
        {
            isPlayerInRange = true;
            Debug.Log("Player is inside the trigger!");
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag != "Player")
        {
            return;
        }
        else
        {
            isPlayerInRange = false;
            Debug.Log("Player is inside the trigger!");
        }
    }

}
