using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    [SerializeField] private GameObject floor;
    public bool entered = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            floor.GetComponent<Collider2D>().enabled = false;
        }
        else 
        {
            floor.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3) 
        {
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 3)
        {
            entered = false;
        }
    }
}
