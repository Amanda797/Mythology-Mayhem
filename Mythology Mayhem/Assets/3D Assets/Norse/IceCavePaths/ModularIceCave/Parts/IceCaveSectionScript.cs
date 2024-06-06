using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveSectionScript : MonoBehaviour
{
    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deadzone") 
        {
            Destroy(this.gameObject);
        }
    }
}
