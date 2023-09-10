using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
    
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Input.GetKey(KeyCode.G))
            {
                grabCheck.collider.gameObject.transform.parent = boxHolder;
                grabCheck.collider.gameObject.transform.position = boxHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }

    private void OnDrawGizmos() 
{
    Gizmos.color = Color.green;

    Gizmos.DrawLine(transform.position,transform.position + Vector3.right * transform.localScale.x * rayDist);
}
}
