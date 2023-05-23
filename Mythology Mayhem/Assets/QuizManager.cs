using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] TextMeshProUGUI answer1;
    [SerializeField] TextMeshProUGUI answer2;
    [SerializeField] TextMeshProUGUI answer3;
    [SerializeField] Questions[] allQuestions;
    [TextArea(3,7)]
    [SerializeField] string introduction;
    int currentQuestion;
    int score;
    bool answered;

    void Start() {
        currentQuestion = -1;
        score = 0;
        answered = false;

        question.text = introduction;
        answer1.text = "Begin";
        answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
        answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
    }//end start

    void DisplayQuestion() {
        question.text = allQuestions[currentQuestion].questionText;
        answer1.text = allQuestions[currentQuestion].answers[0];
        answer2.text = allQuestions[currentQuestion].answers[1];
        answer3.text = allQuestions[currentQuestion].answers[2];        
    }//end display question

    public void AnswerQuestion(int x) {
        if(currentQuestion == -1) {
            answer2.gameObject.transform.parent.transform.gameObject.SetActive(true);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(true);
            currentQuestion++;
            DisplayQuestion();
        } else if(currentQuestion == allQuestions.Length-1) {
            question.text = "Final Score: " + score;
            answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
            if(score >= 2) {                
                answer1.text = "You won a boost!";
            } else {
                answer1.text = "You lost and bear a curse!";
            }
            currentQuestion++;
        } else if(currentQuestion >= allQuestions.Length) {
            Destroy(this.gameObject);
        }
        else {
            if(!answered) {
                if(x == allQuestions[currentQuestion].correctAnswer) {
                    score++;
                    question.text = "Correct Answer!!";
                } else {
                    question.text = "Wrong Answer...";
                }
                answer1.text = "Continue";
                answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
                answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
                answered = true;
            }
            else {
                DelayMovement();
                answered = false;
                answer2.gameObject.transform.parent.transform.gameObject.SetActive(true);
                answer3.gameObject.transform.parent.transform.gameObject.SetActive(true);
            }            
        }        
        
    }//end answer questions

    void DelayMovement() {
        currentQuestion++;
        DisplayQuestion();
    }//end delay movement
    
}

