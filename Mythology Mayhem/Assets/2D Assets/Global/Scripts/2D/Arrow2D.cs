using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2D : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player") return;
        if (col.collider.tag == "Enemy")
        {
            if (!col.gameObject.GetComponent<Health>().isDead) col.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
