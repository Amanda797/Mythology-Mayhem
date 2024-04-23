using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MythologyMayhem
{
    [SerializeField] private Animator leverAnim;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private DoorCode door;

    [SerializeField] private bool canOpen = false;

    [SerializeField] private bool entered = false;

    [SerializeField] private SceneTransitionPoint doorTransition;
    public SaveDataBool boolData;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadState(boolData.boolData);
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && canOpen)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                leverAnim.SetTrigger("Pulled");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            entered = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (entered && canOpen)
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.Popup("Press E to Pull Lever");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            entered = false;
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
                }
            }  
        }
        doorAnim.SetTrigger("Open");
        door.OpenDoor();

        if (boolData != null) 
        {
            boolData.boolData = true;
        }
    }
    public void SetCanOpen(bool open)
    {
        canOpen = open;
    }

    public void LoadState(bool on) 
    {
        if (on) 
        {
            canOpen = true;
            entered = true;
            leverAnim.SetTrigger("Pulled");

            if (doorTransition != null)
            {
                if (doorTransition.conditions.Count > 0)
                {
                    if (doorTransition.conditions[0].condition == Conditions.Condition.Toggle)
                    {
                        doorTransition.conditions[0].currentToggle = true;
                    }
                }
            }
        }
    }
}
