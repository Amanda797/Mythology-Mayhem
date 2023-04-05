using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MonoBehaviour
{
    [SerializeField] private Animator leverAnim;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private DoorCode door;

    private bool canOpen = false;

    public bool entered = false;
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
        if (other.gameObject.layer == 3) 
        {
            entered = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3) 
        {
            entered = false;
        }
        
    }
    private void Opendoor()
    {
        doorAnim.SetTrigger("Open");
        door.OpenDoor();
    }
    public void SetCanOpen(bool open) {
        {
            canOpen = open;
        }
    }
}
