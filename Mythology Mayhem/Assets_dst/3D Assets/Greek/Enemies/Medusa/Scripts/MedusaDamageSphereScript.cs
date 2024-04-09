using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaDamageSphereScript : MonoBehaviour
{
    public float damageAmount;

    public bool damageOverTime;

    public float damageTimer;

    bool damageLock;
    // Start is called before the first frame update
    void Start()
    {
        damageLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        else
        {
            if (damageTimer < 0)
            {
                damageTimer = 0;
            }
        }
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
        if (damageOverTime && damageTimer <= 0)
        {
            if (other.tag == "Player")
            {
                FPSHealth playerHealth = other.gameObject.GetComponent<FPSHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                    damageTimer = 1;
                }
            }
        }
    }
}
