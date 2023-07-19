using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDBow : MonoBehaviour
{

    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool deBugBowReset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpAllowed == true && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if(PlayerPrefs.GetInt("bowBool") == 1)
        {
            Destroy(gameObject);
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
        PlayerPrefs.SetInt("bowBool", 1);
        gameObject.SetActive(false);
    }

}
