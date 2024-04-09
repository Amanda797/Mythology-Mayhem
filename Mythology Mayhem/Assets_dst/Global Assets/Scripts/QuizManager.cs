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
    [SerializeField] TextMeshProUGUI answer4;
    [SerializeField] Questions[] _AllQuestions;
    Questions[] chosenQuestions;
    [TextArea(3,7)]
    [SerializeField] string introduction;
    int currentQuestion;
    int score;
    bool answered;
    bool won;

    [SerializeField] GameObject[] enemy_go;
    [SerializeField] GameObject player;
    [SerializeField] GameObject quizTrigger;

    void Start() {
        Object[] al = GameObject.FindObjectsOfType<AudioListener>();
        foreach(Object _al in al) {
            print("Location: " + _al.ToString());
        }
        chosenQuestions = new Questions[3];

        currentQuestion = -1;
        score = 0;
        answered = false;

        question.text = introduction;
        answer1.text = "Begin";
        answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
        answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
        answer4.gameObject.transform.parent.transform.gameObject.SetActive(false);

        won = false;

        RandomQuestions();

        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
    }//end start

    void DisplayQuestion() {
        if(currentQuestion < chosenQuestions.Length) {
            question.text = chosenQuestions[currentQuestion].questionText;
            answer1.text = chosenQuestions[currentQuestion].answers[0];
            answer2.text = chosenQuestions[currentQuestion].answers[1];
            answer3.text = chosenQuestions[currentQuestion].answers[2];
            answer4.text = chosenQuestions[currentQuestion].answers[3];

            answer2.gameObject.transform.parent.transform.gameObject.SetActive(true);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(true);
            answer4.gameObject.transform.parent.transform.gameObject.SetActive(true);  
        } else {
            answered = true;
        }
                
    }//end display question

    public void AnswerQuestion(int x) {

        if(currentQuestion == -1) {
            answer2.gameObject.transform.parent.transform.gameObject.SetActive(true);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(true);
            answer4.gameObject.transform.parent.transform.gameObject.SetActive(true);
            currentQuestion++;
            DisplayQuestion();
        } else if(currentQuestion == chosenQuestions.Length - 1 & answered) {
            question.text = "Final Score: " + score;
            if(score >= 2) {                
                answer1.text = "You won! You may pass...";
                won = true;
            } else {
                answer1.text = "You lost and bear a curse!";
                won = false;
            }
            answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
            answer4.gameObject.transform.parent.transform.gameObject.SetActive(false);
            currentQuestion++;
        } else if(currentQuestion > chosenQuestions.Length) {
            player.GetComponent<PlayerController>().enabled = true;

            if(won) {
                foreach(GameObject enemy in enemy_go) {
                    enemy.GetComponent<BoxCollider2D>().enabled = false;
                    enemy.GetComponent<Enemy>().CanAttack = false;
                    enemy.GetComponent<Animator>().SetBool("AttackMode", false);
                }
            } else {
                foreach(GameObject enemy in enemy_go) {
                    enemy.GetComponent<Enemy>().CanAttack = true;
                    enemy.GetComponent<Animator>().SetBool("AttackMode", true);
                }
            }

            quizTrigger.GetComponent<QuizTrigger>().shrinking = true;

            Destroy(this.gameObject);
        }
        else {
            if(!answered) {
                bool solution = false;
                foreach(int a in chosenQuestions[currentQuestion].correctAnswers) {
                    if(x == a) {
                        solution = true;
                    }
                }

                if(solution) {
                    score++;
                    question.text = "Correct Answer!!";
                } else {
                    question.text = "Wrong Answer...";
                }

                answer1.text = "Continue";
                answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
                answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
                answer4.gameObject.transform.parent.transform.gameObject.SetActive(false);
                answered = true;
            }
            else {
                DelayMovement();
                answered = false;
            }            
        }        

        if(currentQuestion == chosenQuestions.Length) {
            currentQuestion++;
        }
        
    }//end answer questions

    void DelayMovement() {
        if(currentQuestion < chosenQuestions.Length) {
            currentQuestion++;
            DisplayQuestion(); 
        } else {
            answer2.gameObject.transform.parent.transform.gameObject.SetActive(false);
            answer3.gameObject.transform.parent.transform.gameObject.SetActive(false);
            answer4.gameObject.transform.parent.transform.gameObject.SetActive(false);
        }
    }//end delay movement

    void RandomQuestions() {
        List<int> usedQuestions = new List<int>();

        for(int i = 0; i < 3; i++) {
            bool satisfied = false;
            do {
                int rand = Random.Range(0,_AllQuestions.Length);
                if(!usedQuestions.Contains(rand)) {
                    chosenQuestions[i] = _AllQuestions[rand];
                    usedQuestions.Add(rand);
                    satisfied = true;
                }
            }
            while(!satisfied);
        }
    }//end random questions
    
}

