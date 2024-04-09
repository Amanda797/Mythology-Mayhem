using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool triggered;

    public Collider otherCollider3D;
    public Collider2D otherCollider2D;

    public LayerMask enemyLayer;
    public LayerMask companionLayer;


    #region 3D Triggers

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            triggered = true;
            otherCollider3D = other;
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggered = true;
            otherCollider3D = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggered = false;
            otherCollider3D = null;
        }
    }

    #endregion

    #region 2D Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggered = true;
            otherCollider2D = other;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggered = true;
            otherCollider2D = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggered = false;
            otherCollider2D = null;
        }
    }

    #endregion


}
