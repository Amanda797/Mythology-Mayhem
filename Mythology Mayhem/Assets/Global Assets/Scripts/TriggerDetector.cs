using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool triggered;

    public Collider otherCollider3D;
    public Collider2D otherCollider2D;

    #region 3D Triggers

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        otherCollider3D = other;
    }

    private void OnTriggerStay(Collider other)
    {
        triggered = true;
        otherCollider3D = other;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false ;
        otherCollider3D = null;
    }

    #endregion

    #region 2D Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggered = true;
        otherCollider2D = other;
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
    }

    #endregion


}
