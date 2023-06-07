using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDRayCastScript : MonoBehaviour
{

    Camera cam;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        print(cam.name);
    }

    // Update is called once per frame
    void Update()
    {
        //Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100,mask))
            {
                Debug.Log(hit.transform.name);
                hit. transform.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
