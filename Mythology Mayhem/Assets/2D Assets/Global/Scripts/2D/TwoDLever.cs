using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MonoBehaviour
{
    [SerializeField] private Animator leverAnim;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private DoorCode door;

    [SerializeField] private bool canOpen = false;

    [SerializeField] private bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        doorAnim.SetTrigger("Open");
        door.OpenDoor();
    }
    public void SetCanOpen(bool open)
    {
        canOpen = open;
    }
}
