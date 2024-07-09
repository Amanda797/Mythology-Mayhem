using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MythologyMayhem
{
    GameManager gameManager;

    [SerializeField] private Animator leverAnim;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private DoorCode door;

    public bool canOpen = false;

    [SerializeField] private SceneTransitionPoint doorTransition;

    public SaveDataBool boolData;
    
    // Start is called before the first frame update
    void Start()
    {
        // try to find the GameManager object
        if (GameManager.instance != null) gameManager = GameManager.instance;
        // else display a warning that it is missing
        else Debug.LogWarning("GameManager Missing.");

        if (boolData != null) LoadState(boolData.boolData);
        else Debug.LogWarning("boolData Missing.");

    }

    // Update is called once per frame
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
            gameManager.Popup("Press E to Pull Lever", true);

            canOpen = true;
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
        //door.OpenDoor();

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
