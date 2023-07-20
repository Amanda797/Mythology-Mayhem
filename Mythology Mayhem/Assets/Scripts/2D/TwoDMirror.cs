 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMirror : MonoBehaviour
{
    public bool pickUpAllowed = false;
    public bool pickedUp = false;

    public bool isEquipped;
    public bool isInRangeOfEnemy;

    public float slowingValue;

    public bool deBugMirrorReset;

    

    // Start is called before the first frame update
    void Start()
    {
        if(deBugMirrorReset == true)
        {
            PlayerPrefs.SetInt("mirrorBool", 0);
        }
    }
                                        
    // Update is called once per frame
    void Update()
    {
        if(pickUpAllowed == true && Input.GetKeyDown(KeyCode.E))
        {
            //Destroy(gameObject);
            PickUp();
        }

        if(PlayerPrefs.GetInt("mirrorBool") == 1)
        {
            //Destroy(gameObject);
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
        //SetMinotaurSpeed();
        PlayerPrefs.SetInt("mirrorBool", 1);
        gameObject.SetActive(false);
    }

    private void SetMinotaurSpeed()
    {
        MouseAI[] enemies = FindObjectsOfType<MouseAI>();

        foreach(MouseAI enemy in enemies)
        {
            enemy.SetMovementSpeed(slowingValue);

        }
    }

}
