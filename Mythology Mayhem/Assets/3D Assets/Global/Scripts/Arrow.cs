using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Enemy")
        {
            if(col.gameObject.GetComponent<Health>()) col.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else if (col.gameObject.tag == "Medusa")
        {
            MedusaControlScript mcs = col.gameObject.GetComponent<MedusaControlScript>();
            if (mcs != null) mcs.MedusaDamage(damage);
        }
        Destroy(gameObject);
    }
}
