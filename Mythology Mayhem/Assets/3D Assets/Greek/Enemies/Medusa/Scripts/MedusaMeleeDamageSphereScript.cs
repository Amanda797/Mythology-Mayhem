using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaMeleeDamageSphereScript : MonoBehaviour
{

    public float damage;
    float x = 0.2f;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        x = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        x -= Time.deltaTime;
        if (x <= 0) 
        {
            x = 0.2f;
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            FPSHealth playerHealth = other.gameObject.GetComponent<FPSHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
