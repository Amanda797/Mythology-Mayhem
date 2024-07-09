using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    [SerializeField] QuizManager _quizManager;
    //bool triggered = false;
    public bool shrinking;
    public bool growing;
    float orgLens;
    [SerializeField] float incLens = 11;

    float elapsedTime;
    [SerializeField] float duration = 2;

    GameObject player;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            player = other.gameObject;
            orgLens = player.gameObject.GetComponent<PlayerAttach>().vCam.m_Lens.OrthographicSize;
            _quizManager.playerStats = player.gameObject.GetComponent<PlayerStats>();
            _quizManager.gameObject.SetActive(true);
            _quizManager.StartQuiz();

            //triggered = true;
            //growing = true;
        }
    } // end on trigger enter 2d

    void Update() {
        if(growing) {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            player.gameObject.GetComponent<PlayerAttach>().vCam.m_Lens.OrthographicSize = Mathf.Lerp(orgLens, incLens, percentageComplete);
            if(percentageComplete == 1f) {
                growing = false;
                elapsedTime = 0;
            }
        } 

        if(shrinking) {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            player.gameObject.GetComponent<PlayerAttach>().vCam.m_Lens.OrthographicSize = Mathf.Lerp(incLens, orgLens, percentageComplete);
            if(percentageComplete == 1f) {
                shrinking = false;
                elapsedTime = 0;
            }
        }
    }//end update
}
