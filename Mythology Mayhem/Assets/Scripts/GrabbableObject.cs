using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{

    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public Transform objectHoldPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!grabbed)
            {
                //grab
                Physics2D.queriesStartInColliders = false;


            hit = Physics2D.Raycast(transform.position,Vector2.left * transform.localScale.x,distance);

                if ( hit.collider != null)
                {
                    grabbed = true;

                }
                

            }
            else
            {
                //throw
            }

        }

        if (grabbed)
        {
            hit.collider.gameObject.transform.position = objectHoldPoint.position;
        }

    }

private void OnDrawGizmos() 
{
    Gizmos.color = Color.green;

    Gizmos.DrawLine(transform.position,transform.position + Vector3.left * transform.localScale.x * distance);
}

}
