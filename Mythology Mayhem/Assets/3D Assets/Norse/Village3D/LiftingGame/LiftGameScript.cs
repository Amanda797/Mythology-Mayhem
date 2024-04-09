using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftGameScript : MonoBehaviour
{
    public State curState;
    public LiftVersion currentLift;

    public Animator anim;

    public float liftAmount;
    public float amountPerPress;
    public float drainRate;

    public GameObject indicator;

    public Vector2[] liftAndDrain;
    public Vector2[] indicatorMinMax;

    public Image[] liftBar;
    public LiftAnimScript animScript;

    public LiftVersion testLift;

    public GameObject gameUI;
    public GameObject[] TextDisplays;
    public TMPro.TMP_Text displayText;
    public State stateToReturn;

    [Header("AI Anim Timers")]
    public float timeStamp;
    public float AIAnim1Timer;
    public float AIAnim2Timer;
    public float AIAnim3Timer;

    public enum LiftVersion 
    { 
        Ten,
        Thirty,
        Sixty,
        Hundred
    }

    public enum State 
    { 
        Waiting,
        Confirm,
        Start,
        PlayerTurn1,
        AITurn1,
        PlayerTurn2,
        AITurn2,
        PlayerTurn3,
        AITurn3,
        BonusRound,
        Complete
    }

    private void Start()
    {
        ChangeState(State.Waiting);
    }

    private void Update()
    {
        RunState();
    }

    void SetLift(LiftVersion total) 
    {
        currentLift = total;
        liftAmount = 0;
        switch (total)
        {
            case LiftVersion.Ten:
                amountPerPress = liftAndDrain[0].x;
                drainRate = liftAndDrain[0].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                animScript.SetRock(0);
                break;
            case LiftVersion.Thirty:
                amountPerPress = liftAndDrain[1].x;
                drainRate = liftAndDrain[1].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                animScript.SetRock(1);
                break;
            case LiftVersion.Sixty:
                amountPerPress = liftAndDrain[2].x;
                drainRate = liftAndDrain[2].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                liftBar[3].gameObject.SetActive(true);
                liftBar[4].gameObject.SetActive(true);
                liftBar[5].gameObject.SetActive(true);
                animScript.SetRock(2);
                break;
            case LiftVersion.Hundred:
                amountPerPress = liftAndDrain[3].x;
                drainRate = liftAndDrain[3].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                liftBar[3].gameObject.SetActive(true);
                liftBar[4].gameObject.SetActive(true);
                liftBar[5].gameObject.SetActive(true);
                liftBar[6].gameObject.SetActive(true);
                liftBar[7].gameObject.SetActive(true);
                liftBar[8].gameObject.SetActive(true);
                liftBar[9].gameObject.SetActive(true);
                animScript.SetRock(3);
                break;
        }
    }

    void ClearPowerBar() 
    {
        foreach (Image img in liftBar) 
        {
            img.gameObject.SetActive(false);
        }
    }
    void RunState() 
    {
        switch (curState) 
        {
            case State.Waiting:
                if (Input.GetKeyDown(KeyCode.C)) 
                {
                    ChangeToPauseState(State.PlayerTurn1, "Player 1 Turn");
                    gameUI.SetActive(true);
                }
                return;
            case State.Confirm:
                if (Input.GetKeyDown(KeyCode.C)) 
                {
                    ChangeState(stateToReturn);
                }
                return;
            case State.PlayerTurn1:
                RunWeightLifting();
                return;
            case State.AITurn1:
                if (Time.time - timeStamp >= AIAnim1Timer) 
                {
                    ChangeToPauseState(State.PlayerTurn2, "Player 1 Turn");
                }
                return;
            case State.PlayerTurn2:
                RunWeightLifting();
                return;
            case State.AITurn2:
                if (Time.time - timeStamp >= AIAnim2Timer)
                {
                    ChangeToPauseState(State.PlayerTurn3, "Player 1 Turn");
                }
                return;
            case State.PlayerTurn3:
                RunWeightLifting();
                return;
            case State.AITurn3:
                if (Time.time - timeStamp >= AIAnim1Timer)
                {
                    ChangeToPauseState(State.BonusRound, "Player 1 wins! Bonus Round Time!");
                }
                return;
            case State.BonusRound:
                RunWeightLifting();
                return;
            case State.Complete:

                return;
        }
    }
    void ChangeState(State newState) 
    {
        curState = newState;
        switch (curState) 
        {
            case State.Start:

                return;
            case State.Confirm:

                return;
            case State.PlayerTurn1:
                ShowOnlyMessage("Press Space to Lift!");
                SetLift(LiftVersion.Ten);
                return;
            case State.AITurn1:
                HideText();
                timeStamp = Time.time;
                anim.SetTrigger("Lift1");
                return;
            case State.PlayerTurn2:
                ShowOnlyMessage("Press Space to Lift!");
                SetLift(LiftVersion.Thirty);
                return;
            case State.AITurn2:
                HideText();
                timeStamp = Time.time;
                anim.SetTrigger("Lift1");
                return;
            case State.PlayerTurn3:
                ShowOnlyMessage("Press Space to Lift!");
                SetLift(LiftVersion.Sixty);
                return;
            case State.AITurn3:
                HideText();
                timeStamp = Time.time;
                anim.SetTrigger("Lift2");
                return;
            case State.BonusRound:
                ShowOnlyMessage("Press Space to Lift!");
                SetLift(LiftVersion.Hundred);
                anim.SetTrigger("Shoot");
                return;
            case State.Complete:
                ShowOnlyMessage("You set a new Record!");
                anim.SetTrigger("Cheer2");
                return;
        }
    }
    void ChangeToPauseState(State newState, string message) 
    {
        stateToReturn = newState;
        ShowText(message);
        ChangeState(State.Confirm);
    }
    void RunWeightLifting() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            liftAmount += amountPerPress;
        }

        liftAmount -= Time.deltaTime * drainRate;
        if (liftAmount < 0)
        {
            liftAmount = 0;
        }

        if (liftAmount > 100)
        {
            liftAmount = 100;
            switch (curState)
            {
                case State.PlayerTurn1:
                    ChangeToPauseState(State.AITurn1, "Player 2's Turn");
                    break;
                case State.PlayerTurn2:
                    ChangeToPauseState(State.AITurn2, "Player 2's Turn");
                    break;
                case State.PlayerTurn3:
                    ChangeToPauseState(State.AITurn3, "Player 2's Turn");
                    break;
                case State.BonusRound:
                    ChangeState(State.Complete);
                    break;
            }
        }
        float percentage = liftAmount / 100;

        UpdateIndicator(percentage);
    }
    void UpdateIndicator(float percentage) 
    {
        switch (currentLift)
        {
            case LiftVersion.Ten:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[0].x, indicatorMinMax[0].y, percentage), 15, 0);
                break;
            case LiftVersion.Thirty:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[1].x, indicatorMinMax[1].y, percentage), 15, 0);
                break;
            case LiftVersion.Sixty:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[2].x, indicatorMinMax[2].y, percentage), 15, 0);
                break;
            case LiftVersion.Hundred:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[3].x, indicatorMinMax[3].y, percentage), 15, 0);
                break;
        }
    }

    void HideText() 
    {
        foreach (GameObject obj in TextDisplays) 
        {
            obj.SetActive(false);
        }
    }

    void ShowText(string message) 
    {
        displayText.text = message;
        foreach (GameObject obj in TextDisplays)
        {
            obj.SetActive(true);
        }
    }

    void ShowOnlyMessage(string message) 
    {
        HideText();
        displayText.text = message;
        displayText.gameObject.SetActive(true);
    }
}
