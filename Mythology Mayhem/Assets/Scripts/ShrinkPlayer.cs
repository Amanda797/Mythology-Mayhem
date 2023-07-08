using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlayer : MonoBehaviour
{
    [SerializeField] Transform original;
    [SerializeField] Vector3 shrunk;
    float shrinkage = .95f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            //Save Player's original Transform information
            if(original == null) {
                original = other.gameObject.transform;
            }
            //Make a new Vector3 that is a reduced version of the original Transform
            shrunk = new Vector3(original.transform.localScale.x * shrinkage, original.transform.localScale.y * shrinkage, original.transform.localScale.z * shrinkage);
            //Change the Player's local scale to the shrunken local scale
            other.gameObject.transform.localScale = shrunk;
        }
    }//on trigger enter 2d
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            //Reset the Player's local scale back to its original form
            other.gameObject.transform.localScale = original.localScale;
            original = null;
        }
    }//on trigger enter 2d
}
