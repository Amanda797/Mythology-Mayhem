using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KubbGame : MonoBehaviour
{
    public PlayerTurn curPlayer;
    public PlayerTurn winner;
    public State curState;

    public float targetMoveSpeed;

    public Transform[] PlayerCornerPins;
    public Transform[] CenterPins;
    public Transform[] AICornerPins;

    public Rigidbody[] batons;
    public List<Kubb> playerKubbs;
    public List<Kubb> playerThrowKubbs;
    public List<Kubb> AIKubbs;
    public List<Kubb> AIThrowKubbs;
    public Kubb kingKubb;

    public Transform target;
    public Vector3 actualPosition;

    public Vector3[] playerBaselinePositions;
    public Transform[] playerPositions;
    public Transform[] playerHands;
    public bool[] canGoForKing;

    public Vector3 outDirection;
    [Range(10, 30)]
    public float arcAngle;

    [Range(-1, 1)]
    public float power;
    [Range(-1, 1)]
    public float accuracy;

    [Range(4, 20)]
    public int arcStep;
    public float arcStepDist;
    public float finalAngle;

    public float lockedAccuracy;
    public float lockedPower;

    public float powerErrorAmount;
    public float accuracyErrorAmount;
    public Vector3 accuracyPos;
    public Vector3 powerPos;
    public AnimationCurve powerAccuracyCurve;
    public AnimationCurve intensityCurve;

    public Throw curThrow;

    public int curBaton;
    public Vector2 throwTimers;
    public float throwTimeStamp;
    public Vector3 curBatonPreviousPos;
    public int curBatonTravel;
    public float batonSpeed;
    public Vector3 batonSpin;
    public float linePointDist;
    public bool applyPhysics;
    public float flickPower;

    public LineRenderer lineRend;

    public float totalAngleChange;
    public float angleStep;
    public float totalDist;

    public float maxX;
    public float maxZ;

    public Camera kubbCam;
    public GameObject readyText;
    public GameObject accuracyBarObj;
    public Image[] accuracyBar;
    public Transform accuracyIndicator;
    public float accuracyIndicatorMax;
    public GameObject powerBarObj;
    public Image[] powerBar;
    public Transform powerIndicator;
    public float powerIndicatorMax;
    public Color dangerColor;
    public Color finalDangerColor;
    public Color perfectColor;

    [Header("Testing")]
    public float accuracyPercentage;
    public float powerPercentage;
    public Color testColor;

    [Header("Ai Toss Kubb")]
    public int curKubb;
    public Vector3 curKubbPreviousPosition;
    public Vector3 AITarget;
    public float pauseTime;
    public float animPlayTime;
    public float pauseTimeStamp;
    public bool AIAnimPlaying;
    public bool AIThrowing;
    public int curKubbTravel;

    [Header("AIKubbStand")]
    public bool AIStandStep1;
    public List<Vector3> AIKubbLandings;
    public float dropTimeStamp;
    public float dropTimer;

    [Header("Player Kubb Stand")]
    public bool playerStandStep1;
    public List<Vector3> PlayerKubbLandings;

    public Animator AIAnim;
    public bool gameOver;

    public enum PlayerTurn
    {
        Player,
        AI,
        None
    }

    public enum Position
    {
        Baseline,
        Field
    }
    public enum Throw
    {
        Baton,
        Kubb
    }
    public enum State
    {
        ReadyUp,
        Accuracy,
        Power,
        Throw,
        CheckThrow,
        CheckTurn,
        AITossKubb,
        AICheckKubb,
        AIStandKubb,
        AIMoveToKubb,
        AIThrow,
        AICheckThrow,
        AICheckTurn,
        TossKubb,
        CheckToss,
        StandKubb,
        MoveToKubb,
        EndGame,
        None
    }
    // Start is called before the first frame update
    void Start()
    {
        readyText.SetActive(true);
        curPlayer = PlayerTurn.Player;
        curThrow = Throw.Baton;
        playerBaselinePositions = new Vector3[playerPositions.Length];
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerBaselinePositions[i] = playerPositions[i].position;
        }
        canGoForKing = new bool[2];
        canGoForKing[0] = false;
        canGoForKing[1] = false;
        AIAnim.SetTrigger("Waiting");
    }
    // Update is called once per frame
    void Update()
    {
        RunState();
    }
    void RunState()
    {
        switch (curState)
        {
            case State.ReadyUp:
                curBaton = 0;
                curBatonTravel = 1;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(State.Accuracy);
                }
                return;
            case State.Accuracy:
                InputCheck();
                RunAccuracy();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(State.Power);
                }
                return;
            case State.Power:
                RunPower();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (curThrow == Throw.Baton)
                    {
                        ChangeState(State.Throw);
                    }
                    else
                    {
                        ChangeState(State.TossKubb);
                    }
                }
                return;
            case State.Throw:
                if (curBatonTravel < lineRend.positionCount && !applyPhysics)
                {
                    batons[curBaton].velocity = Vector3.zero;
                    batons[curBaton].transform.position = Vector3.MoveTowards(batons[curBaton].transform.position, lineRend.GetPosition(curBatonTravel), Time.deltaTime * batonSpeed);
                    Vector3 targetDirection = actualPosition - batons[curBaton].transform.position;
                    if (targetDirection.magnitude <= 2)
                    {

                        batons[curBaton].useGravity = true;
                        batons[curBaton].isKinematic = false;
                        batons[curBaton].AddForce(targetDirection.normalized * flickPower);
                        applyPhysics = true;

                        throwTimeStamp = Time.time;

                    }
                    if (Vector3.Distance(batons[curBaton].transform.position, lineRend.GetPosition(curBatonTravel)) <= linePointDist)
                    {
                        curBatonTravel++;
                    }
                }
                else
                {
                    if (batons[curBaton].velocity.magnitude > 0.01f)
                    {
                        if (Time.time - throwTimeStamp > throwTimers.x)
                        {
                            batons[curBaton].velocity = Vector3.zero;
                            ChangeState(State.CheckThrow);
                        }
                        curBatonPreviousPos = batons[curBaton].transform.position;
                    }
                    else
                    {
                        if (Time.time - throwTimeStamp > throwTimers.y)
                        {
                            ChangeState(State.CheckThrow);
                        }
                    }
                }
                break;
            case State.CheckThrow:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(State.Accuracy);
                }
                return;
            case State.AITossKubb:
                if (!AIThrowing)
                {
                    CreateThrowArc(1, AITarget);
                }
                if (Time.time - pauseTimeStamp >= pauseTime && !AIAnimPlaying)
                {
                    AIAnimPlaying = true;
                    AIAnim.SetTrigger("Toss");
                }
                if (Time.time - pauseTimeStamp >= animPlayTime && !AIThrowing && AIAnimPlaying)
                {
                    AIThrowing = true;
                    AIThrowKubbs[curKubb].rb.isKinematic = false;
                    AIThrowKubbs[curKubb].rb.AddTorque(batonSpin, ForceMode.VelocityChange);
                    lineRend.enabled = false;
                }
                if (AIThrowing)
                {
                    if (curKubbTravel < lineRend.positionCount && !applyPhysics)
                    {
                        AIThrowKubbs[curKubb].rb.velocity = Vector3.zero;
                        AIThrowKubbs[curKubb].rb.transform.position = Vector3.MoveTowards(AIThrowKubbs[curKubb].rb.transform.position, lineRend.GetPosition(curKubbTravel), Time.deltaTime * batonSpeed);
                        Vector3 targetDirection = actualPosition - AIThrowKubbs[curKubb].rb.transform.position;
                        if (targetDirection.magnitude <= 2)
                        {
                            AIThrowKubbs[curKubb].rb.useGravity = true;
                            AIThrowKubbs[curKubb].rb.isKinematic = false;
                            AIThrowKubbs[curKubb].rb.AddForce(targetDirection.normalized * flickPower);
                            applyPhysics = true;

                            throwTimeStamp = Time.time;
                        }
                        if (Vector3.Distance(AIThrowKubbs[curKubb].rb.transform.position, lineRend.GetPosition(curKubbTravel)) <= linePointDist)
                        {
                            curKubbTravel++;
                        }
                    }
                    else
                    {
                        if (AIThrowKubbs[curKubb].rb.velocity.magnitude > 0.01f)
                        {
                            if (Time.time - throwTimeStamp > throwTimers.x)
                            {
                                AIThrowKubbs[curKubb].rb.velocity = Vector3.zero;
                                AIThrowing = false;
                                AIAnimPlaying = false;
                                ChangeState(State.AICheckKubb);
                            }
                            curKubbPreviousPosition = AIThrowKubbs[curKubb].rb.transform.position;
                        }
                        else
                        {
                            if (Time.time - throwTimeStamp > throwTimers.y)
                            {
                                AIThrowing = false;
                                AIAnimPlaying = false;
                                ChangeState(State.AICheckKubb);
                            }
                        }
                    }
                }
                return;
            case State.AIStandKubb:
                if (AIStandStep1)
                {
                    if (Time.time - dropTimeStamp > dropTimer)
                    {
                        curBaton = 0;
                        MovePlayer(1, Position.Field);
                        GrabBatons(1);
                        ChangeState(State.AIThrow);
                    }
                }
                else
                {
                    bool CheckHeight = true;
                    for (int i = 0; i < AIThrowKubbs.Count; i++)
                    {
                        float kubbHeight = AIThrowKubbs[i].rb.transform.position.y - AIKubbLandings[i].y;
                        if (kubbHeight < 1)
                        {
                            CheckHeight = false;
                            AIThrowKubbs[i].rb.transform.position = Vector3.MoveTowards(AIThrowKubbs[i].rb.transform.position, AIKubbLandings[i] + new Vector3(0, 1, 0), Time.deltaTime);
                        }
                        else
                        {
                            AIThrowKubbs[i].rb.transform.position = AIKubbLandings[i] + new Vector3(0, 1, 0);
                        }
                    }
                    if (CheckHeight)
                    {
                        AIStandStep1 = true;
                        foreach (Kubb kubb in AIThrowKubbs)
                        {
                            kubb.rb.transform.rotation = Quaternion.identity;
                            kubb.rb.isKinematic = false;
                            kubb.rb.useGravity = true;
                        }
                        for (int i = AIThrowKubbs.Count - 1; i >= 0; i--)
                        {
                            AIThrowKubbs[i].type = Kubb.Type.Field;
                            playerKubbs.Add(AIThrowKubbs[i]);
                            AIThrowKubbs.RemoveAt(i);
                        }
                        dropTimeStamp = Time.time;
                    }
                }
                return;
            case State.AIThrow:
                if (!AIThrowing)
                {
                    CreateThrowArc(1, AITarget);
                }
                if (Time.time - pauseTimeStamp >= pauseTime && !AIAnimPlaying)
                {
                    AIAnimPlaying = true;
                    AIAnim.SetTrigger("Toss");
                }
                if (Time.time - pauseTimeStamp >= animPlayTime && !AIThrowing && AIAnimPlaying)
                {
                    AIThrowing = true;
                    batons[curBaton].isKinematic = false;
                    batons[curBaton].AddTorque(batonSpin, ForceMode.VelocityChange);
                    lineRend.enabled = false;
                }
                if (AIThrowing)
                {
                    if (curBatonTravel < lineRend.positionCount && !applyPhysics)
                    {
                        batons[curBaton].velocity = Vector3.zero;
                        batons[curBaton].transform.position = Vector3.MoveTowards(batons[curBaton].transform.position, lineRend.GetPosition(curBatonTravel), Time.deltaTime * batonSpeed);
                        Vector3 targetDirection = actualPosition - batons[curBaton].transform.position;
                        if (targetDirection.magnitude <= 2)
                        {
                            batons[curBaton].useGravity = true;
                            batons[curBaton].isKinematic = false;
                            batons[curBaton].AddForce(targetDirection.normalized * flickPower);
                            applyPhysics = true;

                            throwTimeStamp = Time.time;
                        }
                        if (Vector3.Distance(batons[curBaton].transform.position, lineRend.GetPosition(curBatonTravel)) <= linePointDist)
                        {
                            curBatonTravel++;
                        }
                    }
                    else
                    {
                        if (batons[curBaton].velocity.magnitude > 0.01f)
                        {
                            if (Time.time - throwTimeStamp > throwTimers.x)
                            {
                                AIThrowing = false;
                                AIAnimPlaying = false;
                                batons[curBaton].velocity = Vector3.zero;
                                ChangeState(State.AICheckThrow);
                            }
                            curBatonPreviousPos = batons[curBaton].transform.position;
                        }
                        else
                        {
                            if (Time.time - throwTimeStamp > throwTimers.y)
                            {
                                AIThrowing = false;
                                AIAnimPlaying = false;
                                ChangeState(State.AICheckThrow);
                            }
                        }
                    }
                }
                return;
            case State.TossKubb:
                if (curKubbTravel < lineRend.positionCount && !applyPhysics)
                {
                    playerThrowKubbs[curKubb].rb.velocity = Vector3.zero;
                    playerThrowKubbs[curKubb].rb.transform.position = Vector3.MoveTowards(playerThrowKubbs[curKubb].rb.transform.position, lineRend.GetPosition(curKubbTravel), Time.deltaTime * batonSpeed);
                    Vector3 targetDirection = actualPosition - playerThrowKubbs[curKubb].rb.transform.position;
                    if (targetDirection.magnitude <= 2)
                    {

                        playerThrowKubbs[curKubb].rb.useGravity = true;
                        playerThrowKubbs[curKubb].rb.isKinematic = false;
                        playerThrowKubbs[curKubb].rb.AddForce(targetDirection.normalized * flickPower);
                        applyPhysics = true;

                        throwTimeStamp = Time.time;
                    }
                    if (Vector3.Distance(playerThrowKubbs[curKubb].rb.transform.position, lineRend.GetPosition(curKubbTravel)) <= linePointDist)
                    {
                        curKubbTravel++;
                    }
                }
                else
                {
                    if (playerThrowKubbs[curKubb].rb.velocity.magnitude > 0.01f)
                    {
                        if (Time.time - throwTimeStamp > throwTimers.x)
                        {
                            playerThrowKubbs[curKubb].rb.velocity = Vector3.zero;
                            ChangeState(State.CheckToss);
                        }
                        curBatonPreviousPos = batons[curBaton].transform.position;
                    }
                    else
                    {
                        if (Time.time - throwTimeStamp > throwTimers.y)
                        {
                            ChangeState(State.CheckToss);
                        }
                    }
                }
                return;
            case State.CheckToss:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(State.Accuracy);
                }
                return;
            case State.StandKubb:
                if (playerStandStep1)
                {
                    if (Time.time - dropTimeStamp > dropTimer)
                    {
                        curBaton = 0;
                        MovePlayer(1, Position.Field);
                        GrabBatons(0);
                        curThrow = Throw.Baton;
                        ChangeState(State.ReadyUp);
                    }
                }
                else
                {
                    bool CheckHeight = true;
                    for (int i = 0; i < playerThrowKubbs.Count; i++)
                    {
                        float kubbHeight = playerThrowKubbs[i].rb.transform.position.y - PlayerKubbLandings[i].y;
                        if (kubbHeight < 1)
                        {
                            CheckHeight = false;
                            playerThrowKubbs[i].rb.transform.position = Vector3.MoveTowards(playerThrowKubbs[i].rb.transform.position, PlayerKubbLandings[i] + new Vector3(0, 1, 0), Time.deltaTime);
                        }
                        else
                        {
                            playerThrowKubbs[i].rb.transform.position = PlayerKubbLandings[i] + new Vector3(0, 1, 0);
                        }
                    }
                    if (CheckHeight)
                    {
                        playerStandStep1 = true;
                        foreach (Kubb kubb in playerThrowKubbs)
                        {
                            kubb.rb.transform.rotation = Quaternion.identity;
                            kubb.rb.isKinematic = false;
                            kubb.rb.useGravity = true;
                        }
                        for (int i = playerThrowKubbs.Count - 1; i >= 0; i--)
                        {
                            playerThrowKubbs[i].type = Kubb.Type.Field;
                            AIKubbs.Add(playerThrowKubbs[i]);
                            playerThrowKubbs.RemoveAt(i);
                        }
                        dropTimeStamp = Time.time;
                    }
                }
                return;
        }
    }
    void ChangeState(State newState)
    {
        curState = newState;

        switch (curState)
        {
            case State.ReadyUp:
                readyText.SetActive(true);
                return;
            case State.Accuracy:
                readyText.SetActive(false);
                lineRend.enabled = true;
                accuracyBarObj.SetActive(true);
                if (curThrow == Throw.Baton)
                {
                    batons[curBaton].transform.position = playerHands[0].transform.position;
                    batons[curBaton].gameObject.SetActive(true);
                }
                else
                {
                    playerThrowKubbs[curKubb].rb.gameObject.SetActive(true);
                }
                return;
            case State.Power:
                powerBarObj.SetActive(true);
                return;
            case State.Throw:
                lineRend.enabled = false;
                accuracyBarObj.SetActive(false);
                powerBarObj.SetActive(false);
                batons[curBaton].isKinematic = false;
                batons[curBaton].AddTorque(batonSpin, ForceMode.VelocityChange);
                applyPhysics = false;
                return;
            case State.CheckThrow:
                canGoForKing[0] = !CheckOpponentKubb();
                if (canGoForKing[0])
                {
                    if (CheckKing(PlayerTurn.Player)) 
                    {
                        ChangeState(State.EndGame);
                        return;
                    }
                }
                else
                {
                    if (CheckKing(PlayerTurn.AI))
                    {
                        ChangeState(State.EndGame);
                        return;
                    }
                }
                curBaton++;
                curBatonTravel = 1;
                if (curBaton < batons.Length)
                {
                    readyText.SetActive(true);
                }
                else
                {
                    ChangeState(State.CheckTurn);
                }
                return;
            case State.CheckTurn:
                MovePlayer(0, Position.Baseline);
                GrabBatons(1);
                CheckKubbs(AIKubbs, AIThrowKubbs);
                curPlayer = PlayerTurn.AI;
                if (AIThrowKubbs.Count > 0)
                {
                    curKubb = 0;
                    GrabKubbs(1, AIThrowKubbs);
                    ChangeState(State.AITossKubb);
                }
                else
                {
                    curBaton = 0;
                    ChangeState(State.AIThrow);
                }
                return;
            case State.AITossKubb:
                AIThrowKubbs[curKubb].rb.gameObject.SetActive(true);
                curKubbTravel = 1;
                AITarget = new Vector3(Random.Range(CenterPins[0].position.x, CenterPins[1].position.x), target.position.y, Random.Range(PlayerCornerPins[0].position.z, CenterPins[0].position.z));
                pauseTimeStamp = Time.time;
                applyPhysics = false;
                AIAnim.SetTrigger("Pickup");
                return;
            case State.AICheckKubb:
                if (CheckKing(PlayerTurn.Player))
                {
                    ChangeState(State.EndGame);
                    return;
                }
                curKubb++;
                if (curKubb < AIThrowKubbs.Count)
                {
                    ChangeState(State.AITossKubb);
                }
                else
                {
                    lineRend.enabled = false;
                    AIKubbLandings = new List<Vector3>();
                    for (int i = 0; i < AIThrowKubbs.Count; i++)
                    {
                        AIKubbLandings.Add(AIThrowKubbs[i].rb.transform.position);
                        AIThrowKubbs[i].rb.isKinematic = true;
                        AIThrowKubbs[i].rb.useGravity = false;
                    }
                    AIStandStep1 = false;
                    ChangeState(State.AIStandKubb);
                }
                return;
            case State.AIStandKubb:

                return;
            case State.AIThrow:
                curBatonTravel = 1;
                AITarget = FindTargetKubb(playerKubbs, 1).rb.transform.position;
                AIAnim.SetTrigger("Pickup");
                batons[curBaton].gameObject.SetActive(true);
                pauseTimeStamp = Time.time;
                applyPhysics = false;
                return;
            case State.AICheckThrow:
                if (canGoForKing[1])
                {
                    if (CheckKing(PlayerTurn.AI))
                    {
                        ChangeState(State.EndGame);
                        return;
                    }
                }
                else
                {
                    if (CheckKing(PlayerTurn.Player))
                    {
                        ChangeState(State.EndGame);
                        return;
                    }
                }
                curBaton++;
                curBatonTravel = 1;
                if (curBaton < batons.Length)
                {
                    ChangeState(State.AIThrow);
                }
                else
                {
                    MovePlayer(1, Position.Baseline);
                    ChangeState(State.AICheckTurn);
                }
                return;
            case State.AICheckTurn:
                GrabBatons(0);
                CheckKubbs(playerKubbs, playerThrowKubbs);
                curPlayer = PlayerTurn.Player;
                if (playerThrowKubbs.Count > 0)
                {
                    curKubb = 0;
                    GrabKubbs(0, playerThrowKubbs);
                    MovePlayer(0, Position.Baseline);
                    curThrow = Throw.Kubb;
                    ChangeState(State.ReadyUp);
                }
                else
                {
                    MovePlayer(0, Position.Field);
                    curThrow = Throw.Baton;
                    ChangeState(State.ReadyUp);
                }
                return;
            case State.TossKubb:
                lineRend.enabled = false;
                accuracyBarObj.SetActive(false);
                powerBarObj.SetActive(false);
                playerThrowKubbs[curKubb].rb.isKinematic = false;
                playerThrowKubbs[curKubb].rb.AddTorque(batonSpin, ForceMode.VelocityChange);
                curKubbTravel = 1;
                applyPhysics = false;
                return;
            case State.CheckToss:
                if (CheckKing(PlayerTurn.AI))
                {
                    ChangeState(State.EndGame);
                    return;
                }
                curKubb++;
                curKubbTravel = 1;
                if (curKubb < playerThrowKubbs.Count)
                {
                    readyText.SetActive(true);
                }
                else
                {
                    lineRend.enabled = false;
                    PlayerKubbLandings = new List<Vector3>();
                    for (int i = 0; i < playerThrowKubbs.Count; i++)
                    {
                        PlayerKubbLandings.Add(playerThrowKubbs[i].rb.transform.position);
                        playerThrowKubbs[i].rb.isKinematic = true;
                        playerThrowKubbs[i].rb.useGravity = false;
                    }
                    playerStandStep1 = false;
                    ChangeState(State.StandKubb);
                }
                return;
            case State.StandKubb:

                return;
            case State.EndGame:
                if (winner == PlayerTurn.Player)
                {
                    AIAnim.SetTrigger("Defeat");
                }
                else 
                {
                    AIAnim.SetTrigger("Cheer");
                }
                break;
        }
    }
    void InputCheck()
    {
        if (Input.GetKey(KeyCode.W))
        {
            target.position += new Vector3(0, 0, 0.1f) * targetMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            target.position += new Vector3(0, 0, -0.1f) * targetMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            target.position += new Vector3(-0.1f, 0, 0) * targetMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            target.position += new Vector3(0.1f, 0, 0) * targetMoveSpeed * Time.deltaTime;
        }

        if (target.localPosition.x > maxX)
        {
            target.localPosition = new Vector3(maxX, target.localPosition.y, target.localPosition.z);
        }
        if (target.localPosition.x < (maxX * -1))
        {
            target.localPosition = new Vector3(maxX * -1, target.localPosition.y, target.localPosition.z);
        }

        if (target.localPosition.z > maxZ)
        {
            target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, maxZ);
        }
        if (target.localPosition.z < (maxZ * -1))
        {
            target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, maxZ * -1);
        }

    }
    void RunAccuracy()
    {
        kubbCam.transform.LookAt(target.position);

        float currentLoopPos = Time.time % 1;
        float currentOffset = powerAccuracyCurve.Evaluate(currentLoopPos);

        accuracy = currentOffset;
        totalDist = Vector3.Distance(target.position, playerHands[0].position);

        float accuracyChange = accuracyErrorAmount * intensityCurve.Evaluate(totalDist);

        accuracyPos = new Vector3(accuracyChange, 0, 0) * accuracy;
        lockedAccuracy = accuracy;
        finalDangerColor = Color.Lerp(perfectColor, dangerColor, intensityCurve.Evaluate(totalDist) / 0.5f);

        accuracyPercentage = (accuracy + 1);
        if (accuracyPercentage > 1)
        {
            accuracyPercentage = 1 - (accuracyPercentage - 1);
        }
        testColor = Color.Lerp(finalDangerColor, perfectColor, accuracyPercentage);


        accuracyIndicator.localPosition = new Vector3(lockedAccuracy * accuracyIndicatorMax, 0, 0);

        lineRend.startColor = testColor;
        lineRend.endColor = testColor;

        accuracyBar[0].color = Color.Lerp(finalDangerColor, perfectColor, 0);
        accuracyBar[1].color = Color.Lerp(finalDangerColor, perfectColor, 0.15f);
        accuracyBar[2].color = Color.Lerp(finalDangerColor, perfectColor, 0.25f);
        accuracyBar[3].color = Color.Lerp(finalDangerColor, perfectColor, 0.5f);
        accuracyBar[4].color = Color.Lerp(finalDangerColor, perfectColor, 1);
        accuracyBar[5].color = Color.Lerp(finalDangerColor, perfectColor, 0.5f);
        accuracyBar[6].color = Color.Lerp(finalDangerColor, perfectColor, 0.25f);
        accuracyBar[7].color = Color.Lerp(finalDangerColor, perfectColor, 0.15f);
        accuracyBar[8].color = Color.Lerp(finalDangerColor, perfectColor, 0);

        CreateThrowArc(0, target.position);
    }
    void RunPower()
    {
        kubbCam.transform.LookAt(target.position);

        float currentLoopPos = Time.time % 1;
        float currentOffset = powerAccuracyCurve.Evaluate(currentLoopPos);

        power = currentOffset;
        totalDist = Vector3.Distance(target.position, playerHands[0].position);

        float powerChange = powerErrorAmount * intensityCurve.Evaluate(totalDist);

        powerPos = new Vector3(0, 0, powerChange) * lockedPower;

        powerPos = new Vector3(0, 0, powerErrorAmount * intensityCurve.Evaluate(totalDist)) * power;
        lockedPower = power;


        finalDangerColor = Color.Lerp(perfectColor, dangerColor, intensityCurve.Evaluate(totalDist) / 0.5f);

        powerPercentage = (power + 1);
        if (powerPercentage > 1)
        {
            powerPercentage = 1 - (powerPercentage - 1);
        }
        testColor = Color.Lerp(finalDangerColor, perfectColor, powerPercentage);


        accuracyIndicator.localPosition = new Vector3(lockedAccuracy * accuracyIndicatorMax, 0, 0);
        powerIndicator.localPosition = new Vector3(lockedPower * powerIndicatorMax, 0, 0);

        lineRend.startColor = testColor;
        lineRend.endColor = testColor;

        powerBar[0].color = Color.Lerp(finalDangerColor, perfectColor, 0);
        powerBar[1].color = Color.Lerp(finalDangerColor, perfectColor, 0.15f);
        powerBar[2].color = Color.Lerp(finalDangerColor, perfectColor, 0.25f);
        powerBar[3].color = Color.Lerp(finalDangerColor, perfectColor, 0.5f);
        powerBar[4].color = Color.Lerp(finalDangerColor, perfectColor, 1);
        powerBar[5].color = Color.Lerp(finalDangerColor, perfectColor, 0.5f);
        powerBar[6].color = Color.Lerp(finalDangerColor, perfectColor, 0.25f);
        powerBar[7].color = Color.Lerp(finalDangerColor, perfectColor, 0.15f);
        powerBar[8].color = Color.Lerp(finalDangerColor, perfectColor, 0);

        CreateThrowArc(0, target.position);
    }
    void CreateThrowArc(int player, Vector3 curTarget)
    {
        if (player == 0)
        {
            actualPosition = curTarget + (powerPos) + (accuracyPos);
        }
        else
        {
            actualPosition = curTarget;
        }
        outDirection = actualPosition - playerHands[player].position;
        outDirection.y = 0;
        arcStepDist = (outDirection.magnitude / arcStep);

        finalAngle = arcAngle * -1;
        totalAngleChange = arcAngle - finalAngle;
        angleStep = totalAngleChange / (arcStep - 1);

        float yDiff = curTarget.y - playerHands[player].position.y;
        float yDiffStep = yDiff / arcStep;

        lineRend.positionCount = arcStep + 1;
        lineRend.SetPosition(0, playerHands[player].position);
        Vector3 pos = playerHands[player].position;

        for (int i = 0; i < arcStep; i++)
        {
            float rad = (arcAngle - (i * angleStep)) * Mathf.PI / 100;

            float tanAngle = Mathf.Tan(rad);

            Vector3 step = outDirection.normalized * arcStepDist;
            step.y = (tanAngle * arcStepDist) + yDiffStep;
            pos = pos + step;
            lineRend.SetPosition(i + 1, pos);
        }
        lineRend.enabled = true;
    }
    Kubb FindTargetKubb(List<Kubb> kubbList, int player)
    {
        Kubb closestKubb = null;
        float dist = 10000;
        canGoForKing[1] = false;
        for (int i = 0; i < kubbList.Count; i++)
        {
            if (kubbList[i].type == Kubb.Type.Field)
            {
                float angleCheck = Vector3.Angle(kubbList[i].rb.transform.up, Vector3.up);
                if (angleCheck < 1)
                {
                    float checkDist = Vector3.Distance(playerHands[player].position, kubbList[i].rb.transform.position);
                    if (checkDist < dist)
                    {
                        closestKubb = kubbList[i];
                        dist = checkDist;
                    }
                }
            }
        }
        if (closestKubb == null)
        {
            for (int i = 0; i < kubbList.Count; i++)
            {
                float angleCheck = Vector3.Angle(kubbList[i].rb.transform.up, Vector3.up);
                if (angleCheck < 1)
                {
                    float checkDist = Vector3.Distance(playerHands[player].position, kubbList[i].rb.transform.position);
                    if (checkDist < dist)
                    {
                        closestKubb = kubbList[i];
                        dist = checkDist;
                    }
                }
            }
        }
        if (closestKubb == null)
        {
            canGoForKing[1] = true;
            return kingKubb;
        }
        return closestKubb;
    }
    void MovePlayer(int player, Position position)
    {
        if (position == Position.Field)
        {
            List<Kubb> whichKubb = null;
            if (player == 0)
            {
                whichKubb = playerKubbs;
            }
            else
            {
                whichKubb = AIKubbs;
            }
            Kubb furthestFieldKubb = null;
            float checkDist = 0;
            for (int i = 0; i < whichKubb.Count; i++)
            {
                if (whichKubb[i].type == Kubb.Type.Field)
                {
                    float zDist = whichKubb[i].rb.transform.position.z - playerPositions[player].position.z;
                    if (zDist > checkDist)
                    {
                        furthestFieldKubb = whichKubb[i];
                        checkDist = zDist;
                    }
                }
            }
            if (furthestFieldKubb != null)
            {
                playerPositions[player].position = new Vector3(furthestFieldKubb.rb.transform.position.x, playerPositions[player].position.y, furthestFieldKubb.rb.transform.position.z);
            }
            else
            {
                playerPositions[player].position = playerBaselinePositions[player];
            }
        }
        else
        {
            playerPositions[player].position = playerBaselinePositions[player];
        }
    }
    void GrabBatons(int player)
    {
        foreach (Rigidbody rb in batons)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.gameObject.SetActive(false);
            rb.transform.position = playerHands[player].position;
            rb.transform.rotation = Quaternion.identity;
        }
    }
    void CheckKubbs(List<Kubb> KubbToCheck, List<Kubb> ThrowKubb)
    {
        for (int i = KubbToCheck.Count - 1; i >= 0; i--)
        {
            float angleCheck = Vector3.Angle(KubbToCheck[i].rb.transform.up, Vector3.up);
            if (angleCheck > 1)
            {
                ThrowKubb.Add(KubbToCheck[i]);
                KubbToCheck.RemoveAt(i);
            }
        }
    }
    void GrabKubbs(int player, List<Kubb> whichKubbs)
    {
        foreach (Kubb kubb in whichKubbs)
        {
            kubb.rb.isKinematic = true;
            kubb.rb.useGravity = false;
            kubb.rb.transform.position = playerHands[player].position;
            kubb.rb.transform.rotation = Quaternion.identity;
            kubb.rb.gameObject.SetActive(false);
        }
    }

    bool CheckKing(PlayerTurn whoWins)
    {
        print("Checking King");
        float angleCheck = Vector3.Angle(kingKubb.rb.transform.up, Vector3.up);
        print(angleCheck + " : Angle of King to World Up.");
        if (angleCheck > 1)
        {
            winner = whoWins;
            return true;
        }
        return false;
    }

    bool CheckOpponentKubb()
    {
        bool remainingKubb = false;
        for (int i = 0; i < AIKubbs.Count; i++)
        {
            float angleCheck = Vector3.Angle(AIKubbs[i].rb.transform.up, Vector3.up);
            if (angleCheck < 1)
            {
                remainingKubb = true;
            }
        }
        return remainingKubb;
    }
}
