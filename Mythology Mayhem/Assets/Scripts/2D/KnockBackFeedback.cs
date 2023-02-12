using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float strength = 16f, delay = .15f;

    public UnityEvent OnBegin, OnDone;

    private void Start() 
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void PlayerFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        direction = new Vector2(0,1f) + direction;
        rb2d.AddForce(direction*strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
        StartCoroutine(ResetY());
    }

    private IEnumerator Reset() 
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

    private IEnumerator ResetY()
    {
        yield return new WaitForSeconds(delay/8);
        rb2d.velocity = new Vector3(rb2d.velocity.x, 0f, 0f);
    
    }
}
