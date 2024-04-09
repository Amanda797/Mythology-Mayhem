using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlayer : MonoBehaviour
{
    Vector3 shrunk;
    Vector3 originalScale = Vector3.zero;
    [Tooltip("Percentage of shrinking as a decimal between 0 and 1 (float)")]
    [SerializeField] float shrinkage = .85f;
    Collider2D player;
    bool shrinking = false;
    bool growing = false;
    [Tooltip("How long should the animation take?")]
    [SerializeField] float duration = 1f;
    float elapsedTime;


    void Update() {
        if(shrinking) {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            player.gameObject.transform.localScale = Vector3.Lerp(originalScale, shrunk, percentageComplete);
            //if current player scale is the size of shrunk, set shrinking = false to stop updating;
            if(percentageComplete == 1f) {
                shrinking = false;
                elapsedTime = 0;
            }
        } 

        if(growing) {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            player.gameObject.transform.localScale = Vector3.Lerp(shrunk, originalScale, percentageComplete);
            //if current player scale is the original size, set growing = false to stop updating;
            if(percentageComplete == 1f) {
                growing = false;
                elapsedTime = 0;
                player = null;
                originalScale = Vector3.zero;
            }
        }
    }//end update

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            //Save Player's original Transform information
            if(originalScale == Vector3.zero) {
                originalScale = other.gameObject.transform.localScale;
            }
            //Make a new Vector3 that is a reduced version of the original Transform
            shrunk = new Vector3(other.gameObject.transform.localScale.x * shrinkage, other.gameObject.transform.localScale.y * shrinkage, other.gameObject.transform.localScale.z * shrinkage);
            //Assign player for the Update function to use
            player = other;
            shrinking = true;
        }
    }//on trigger enter 2d
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            //Reset the Player's local scale back to its original form via the Update method
            growing = true;
        }
    }//on trigger enter 2d
}
