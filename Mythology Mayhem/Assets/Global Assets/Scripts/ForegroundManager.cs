using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundManager : MonoBehaviour
{
    SpriteRenderer sr;
    Color original;
    [SerializeField] float opacity = .706f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sr.color = new Color(original.r, original.g, original.b, opacity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sr.color = original;
        }
    }
}
