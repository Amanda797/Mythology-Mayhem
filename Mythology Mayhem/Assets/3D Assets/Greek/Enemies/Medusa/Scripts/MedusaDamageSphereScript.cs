using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaDamageSphereScript : MonoBehaviour
{
    public float damageAmount;

    public bool damageOverTime;

    bool damageLock;
    // Start is called before the first frame update
    void Start()
    {
        damageLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!damageOverTime && !damageLock)
        {
            if (other.tag == "Player")
            {
                Health playerHealth = other.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                    damageLock = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (damageOverTime)
        {
            if (other.tag == "Player")
            {
                Health playerHealth = other.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    //playerHealth.TakeDamage(1);
                }
            }
        }
    }
}
