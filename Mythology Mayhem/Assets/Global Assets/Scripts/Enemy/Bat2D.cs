using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat2D : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravityScale = 1f;

    public void Death()
    {
        Debug.Log("Death2");
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Enemy>().player.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}