using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDoorUniversalScript : MonoBehaviour
{ 
    public bool canActivate;
    public bool inRange;

    public PlayerController player;

    public CaveDoorUniversalScript exitDoor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player == null) 
            {
                player = other.gameObject.GetComponent<PlayerController>();
            }
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }

    }

    void UseDoor()
    {
        if (player != null) 
        {
            player.transform.position = exitDoor.transform.position;
        }
    }

}
