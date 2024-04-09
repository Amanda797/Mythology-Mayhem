using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2D : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<Health>())
                col.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
