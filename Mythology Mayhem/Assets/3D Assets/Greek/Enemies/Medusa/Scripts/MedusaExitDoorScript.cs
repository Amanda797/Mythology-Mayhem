using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaExitDoorScript : MonoBehaviour
{

    public float yStart;
    public float yEnd;
    public float speed;
    public bool raiseDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (raiseDoor)
        {
            if (transform.position.y < yEnd) 
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                if (transform.position.y >= yEnd) 
                {
                    transform.position = new Vector3(transform.position.x, yEnd, transform.position.z);
                }
            }
        }
        else 
        {
            if (transform.position.y > yStart) 
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                if (transform.position.y <= yStart) 
                {
                    transform.position = new Vector3(transform.position.x, yStart, transform.position.z);
                }
            }
        }   
    }

    public void RaiseDoor(bool upDown) 
    {
        raiseDoor = upDown;
    }
}
