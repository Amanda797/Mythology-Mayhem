using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector2D : MonoBehaviour
{
    public bool triggered;
    public Collider2D otherCollider2D;
    public List<Collider2D> otherColliders2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggered = true;
        otherCollider2D = other;
        //
        otherColliders2D.Add(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        triggered = true;
        otherCollider2D = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggered = false;
        otherCollider2D = null;
        //
        otherColliders2D.Remove(other);
    }
}
