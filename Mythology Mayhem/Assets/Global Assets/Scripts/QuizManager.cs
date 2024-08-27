using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;

public class QuizManager : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip buttonClip, correctClip, incorrectClip, winClip, loseClip, scrollOpen;

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

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
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

        animator.Play("scroll", 0);
        audioSource.Play();
        StartCoroutine(OpenUI());
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
        }
        else answered = true;
                
    }//end display question

    public void AnswerQuestion(int x)
    {
        if (!gameManager.gameData.collectedMirror)
        {
            AudioSource source = gameManager.GetComponent<AudioSource>();
            source.clip = buttonClip;
            source.Play();
            animator.Play("Scroll_Close", 0);
            audioSource.Play();
            StartCoroutine(CloseUI());
            return;
        }
        if (currentQuestion == -1)
        {
            answer2Obj.SetActive(true);
            answer3Obj.SetActive(true);
            answer4Obj.SetActive(true);
            currentQuestion++;
            AudioSource source = gameManager.GetComponent<AudioSource>();
            source.clip = buttonClip;
            source.Play();
            DisplayQuestion();
            return;
        }
        else if(currentQuestion == chosenQuestions.Length - 1 & answered)
        {
            question.text = "Final Score: " + score;

            if(score >= 2)
            {
                answer1.text = "You won! You may pass...";
                won = true;
                AudioSource source = gameManager.GetComponent<AudioSource>();
                source.clip = winClip;
                source.Play();
            }
            else
            {
                answer1.text = "You lost and bear a curse!";
                won = false;
                AudioSource source = gameManager.GetComponent<AudioSource>();
                source.clip = loseClip;
                source.Play();
            }

            answer2Obj.SetActive(false);
            answer3Obj.SetActive(false);
            answer4Obj.SetActive(false);
            currentQuestion++;
            return;
        } 
        else if(currentQuestion >= chosenQuestions.Length)
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

            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            animator.Play("Scroll_Close", 0);
            audioSource.Play();
            StartCoroutine(LoadMedussa());
            return;
        }
        else {
            if(!answered) {
                bool solution = false;
                foreach(int a in chosenQuestions[currentQuestion].correctAnswers) {
                    if(x == a) {
                        solution = true;
                    }
                }

                if(solution)
                {
                    score++;
                    question.text = "Correct Answer!!";
                    AudioSource source = gameManager.GetComponent<AudioSource>();
                    source.clip = correctClip;
                    source.Play();
                }
                else
                {
                    question.text = "Wrong Answer...";
                    AudioSource source = gameManager.GetComponent<AudioSource>();
                    source.clip = incorrectClip;
                    source.Play();
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

    IEnumerator LoadMedussa()
    {
        yield return new WaitForSecondsRealtime(1);
        this.gameObject.SetActive(false);
        transitionPoint.localGameManager.mainGameManager.TransitionScene(transitionPoint.sceneToTransition, transitionPoint.spawnpointNameOverride);
    }

    IEnumerator CloseUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(1);
        this.gameObject.SetActive(false);
    }
    IEnumerator OpenUI()
    {
        yield return new WaitForSecondsRealtime(1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
}

