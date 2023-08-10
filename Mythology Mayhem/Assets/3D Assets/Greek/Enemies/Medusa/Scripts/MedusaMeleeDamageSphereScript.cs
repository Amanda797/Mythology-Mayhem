using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaMeleeDamageSphereScript : MonoBehaviour
{

    public float damage;
    float x = 1;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        x -= Time.deltaTime;
        if (x <= 0) 
        {
            x = 1;
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        if (other.tag == "Player")
        {
            print("Hit Player");
            FPSHealth playerHealth = other.gameObject.GetComponent<FPSHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
  
    }
}
