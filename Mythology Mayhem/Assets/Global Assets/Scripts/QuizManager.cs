using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;

public class QuizManager : MonoBehaviour
{
    GameManager gameManager;
    public SceneTransitionPoint2D transitionPoint;

    [SerializeField] TMP_Text question;
    [SerializeField] TMP_Text answer1;
    [SerializeField] TMP_Text answer2;
    [SerializeField] TMP_Text answer3;
    [SerializeField] TMP_Text answer4;
    [SerializeField] GameObject answer1Obj;
    [SerializeField] GameObject answer2Obj;
    [SerializeField] GameObject answer3Obj;
    [SerializeField] GameObject answer4Obj;
    [SerializeField] Questions[] _AllQuestions;
    Questions[] chosenQuestions = new Questions[3];
    [TextArea(3,7)]
    [SerializeField] string introduction;
    int currentQuestion = -1;
    int score = 0;
    bool answered = false;
    bool won = false;
    public PlayerStats playerStats;

    [SerializeField] GameObject[] enemy_go;
    [SerializeField] GameObject quizTrigger;

    public void StartQuiz()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        answer2Obj.SetActive(false);
        answer3Obj.SetActive(false);
        answer4Obj.SetActive(false);

        if (gameManager.gameData.collectedMirror)
        {
            question.text = introduction;
            answer1.text = "Begin";
            RandomQuestions();
        }
        else
        {
            question.text = "You do not have the mirror.\nReturn when you have it to continue.";
            answer1.text = "Close";
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
    void DisplayQuestion() {
        if(currentQuestion < chosenQuestions.Length) {
            question.text = chosenQuestions[currentQuestion].questionText;
            answer1.text = chosenQuestions[currentQuestion].answers[0];
            answer2.text = chosenQuestions[currentQuestion].answers[1];
            answer3.text = chosenQuestions[currentQuestion].answers[2];
            answer4.text = chosenQuestions[currentQuestion].answers[3];

            answer2Obj.SetActive(true);
            answer3Obj.SetActive(true);
            answer4Obj.SetActive(true);
        } else {
            answered = true;
        }
                
    }//end display question

    public void AnswerQuestion(int x) {
        if (!gameManager.gameData.collectedMirror)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.SetActive(false);
            return;
        }
        if (currentQuestion == -1)
        {
            answer2Obj.SetActive(true);
            answer3Obj.SetActive(true);
            answer4Obj.SetActive(true);
            currentQuestion++;
            DisplayQuestion();
        }
        else if(currentQuestion == chosenQuestions.Length - 1 & answered)
        {
            question.text = "Final Score: " + score;

            if(score >= 2)
            {
                answer1.text = "You won! You may pass...";
                won = true;
            }
            else
            {
                answer1.text = "You lost and bear a curse!";
                won = false;
            }

            answer2Obj.SetActive(false);
            answer3Obj.SetActive(false);
            answer4Obj.SetActive(false);
            currentQuestion++;
        } 
        else if(currentQuestion > chosenQuestions.Length)
        {
            if (!won)
            {
                foreach (GameObject enemy in enemy_go)
                {
                    if (enemy.gameObject.name == "OldLadyFate") enemy.GetComponent<Animator>().Play("OldLadyFate_AttackAnim");
                    if (enemy.gameObject.name == "YoungLadyFate") enemy.GetComponent<Animator>().Play("YoungFateAttack");
                    if (enemy.gameObject.name == "MiddleLadyFate") enemy.GetComponent<Animator>().Play("MiddleFateAttack");

                }
                playerStats.TakeDamage(10);
            }

            this.gameObject.SetActive(false);
            LoadMedussa();
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
                answer2Obj.SetActive(false);
                answer3Obj.SetActive(false);
                answer4Obj.SetActive(false);
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
            answer2Obj.SetActive(false);
            answer3Obj.SetActive(false);
            answer4Obj.SetActive(false);
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
    }

    void LoadMedussa()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transitionPoint.localGameManager.mainGameManager.TransitionScene(transitionPoint.sceneToTransition, transitionPoint.spawnpointNameOverride);
    }
}

