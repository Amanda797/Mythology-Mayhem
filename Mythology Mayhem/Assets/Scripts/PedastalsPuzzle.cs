using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedastalsPuzzle : MonoBehaviour
{
    private PedastalsPuzzleManager puzzleManager;

    public bool isFish, isEarth, isFire, isAir;

    public SpriteRenderer elementalIcon;
    [SerializeField] private bool isPlayerInRange = false;
    public bool isFishDone, isEarthDone, isFireDone, isAirDone;


    // Start is called before the first frame update
    void Awake()
    {
        puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
        isFish = puzzleManager.fish;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isFish && isPlayerInRange)
            {
                elementalIcon.color = new Color(1, 1, 1);
                isFish = false;
                isPlayerInRange = false;
                isFishDone = true;
            }
            else if(isEarth && isPlayerInRange )
            {
                elementalIcon.color = new Color(1, 1, 1);
                isEarth = false;
                isPlayerInRange = false;
                isEarthDone = true;
            }
            else if(isFire && isPlayerInRange)
            {
                elementalIcon.color = new Color(1, 1, 1);
                isFire = false;
                isPlayerInRange = false;
                isFireDone = true;
            }
            else if(isAir && isPlayerInRange)
            {
                elementalIcon.color = new Color(1, 1, 1);
                isAir = false;
                isPlayerInRange = false;
                isAirDone = true;
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
