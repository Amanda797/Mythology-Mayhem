using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MythologyMayhem
{
    GameManager gameManager;
    Animator leverAnim;

    [SerializeField] private Animator doorAnim;
    [SerializeField] private SceneTransitionPoint doorTransition;

    public SaveDataBool boolData;
    bool canOpen = false;
    public bool isBlocked;


    private void Awake()
    {
        leverAnim = GetComponent<Animator>();
    }
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        if (boolData != null) LoadState(boolData.boolData);
        else Debug.LogWarning("boolData Missing.");
    }

    void Update()
    {
        if (!canOpen) return;

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            canOpen = false;
            leverAnim.SetTrigger("Pulled");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            if (!isBlocked)
            {
                gameManager.Popup("Press E to Pull Lever", true);

                canOpen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            gameManager.Popup("Press E to Pull Lever", false);

            canOpen = false;
        }        
    }
    private void Opendoor()
    {
        //This method is called from the Lever's Animation so don't put door.OpenDoor() anywhere else or it breaks
        if (doorTransition != null) 
        {
            if (doorTransition.conditions.Count > 0)
            {
                if (doorTransition.conditions[0].condition == Conditions.Condition.Toggle)
                {
                    doorTransition.conditions[0].currentToggle = true;
                    doorTransition.isActive = doorTransition.CheckConditionsMeet();
                }
            }  
        }

        doorAnim.SetTrigger("Open");

        if (boolData != null) 
        {
            boolData.boolData = true;
        }
    }
    public void LoadState(bool on) 
    {
        if (on) 
        {
            if (doorTransition != null)
            {
                if (doorTransition.conditions.Count > 0)
                {
                    if (doorTransition.conditions[0].condition == Conditions.Condition.Toggle)
                    {
                        doorTransition.conditions[0].currentToggle = true;
                        doorTransition.isActive = doorTransition.CheckConditionsMeet();
                    }
                }
            }
        }
    }
}
