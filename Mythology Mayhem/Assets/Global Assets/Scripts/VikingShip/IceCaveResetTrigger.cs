using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveResetTrigger : MonoBehaviour
{
    public IceCaveShipEntrance shipEntrance;

    public bool used;
    public string animTrigger;
    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            if (other.tag == "Player")
            {
                shipEntrance.ResetTrigger(animTrigger);
                used = true;
            }
        }
    }
}
