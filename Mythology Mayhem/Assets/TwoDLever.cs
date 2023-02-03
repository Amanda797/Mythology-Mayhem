using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDLever : MonoBehaviour
{
    [SerializeField] private Animator anim;

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
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                anim.SetTrigger("Pulled");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        entered = true;
    }
}
