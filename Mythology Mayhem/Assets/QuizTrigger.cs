using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    [SerializeField] GameObject _quizManager;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!triggered && other.tag == "Player") {
            _quizManager.SetActive(true);
            triggered = true;
        }
    } // end on trigger enter 2d
}
