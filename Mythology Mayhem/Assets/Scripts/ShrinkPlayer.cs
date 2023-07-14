using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlayer : MonoBehaviour
{
    Vector3 shrunk;
    Vector3 originalScale = Vector3.zero;
    [SerializeField] float shrinkage = .85f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            //Save Player's original Transform information
            if(originalScale == Vector3.zero) {
                originalScale = other.gameObject.transform.localScale;
            }
            //Make a new Vector3 that is a reduced version of the original Transform
            shrunk = new Vector3(other.gameObject.transform.localScale.x * shrinkage, other.gameObject.transform.localScale.y * shrinkage, other.gameObject.transform.localScale.z * shrinkage);
            //Change the Player's local scale to the shrunken local scale
            other.gameObject.transform.localScale = shrunk;
        }
    }//on trigger enter 2d
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            //Reset the Player's local scale back to its original form
            print("Exit Ladder Shrink");
            other.gameObject.transform.localScale = originalScale;
            originalScale = Vector3.zero;
        }
    }//on trigger enter 2d
}
