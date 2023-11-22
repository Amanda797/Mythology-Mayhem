using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector3D : MonoBehaviour
{
    public bool triggered;
    public Collider otherCollider3D;

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
        triggered = false;
        otherCollider3D = null;
    }
}
