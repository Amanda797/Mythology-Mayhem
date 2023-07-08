using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMirror : MonoBehaviour
{
    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool isEquipped;
    public bool isInRangeOfEnemy;

    public MouseAI mouseAI;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(mouseAI.walkSpeed); 
        Debug.Log(mouseAI.runSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpAllowed == true && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();

            Debug.Log(mouseAI.walkSpeed);
            Debug.Log(mouseAI.runSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            pickUpAllowed = false;
        }
    }



    private void PickUp()
    {
        pickedUp = true;
        gameObject.SetActive(false);

        mouseAI.walkSpeed = 2f;
        mouseAI.runSpeed = 4f;
    }

}
