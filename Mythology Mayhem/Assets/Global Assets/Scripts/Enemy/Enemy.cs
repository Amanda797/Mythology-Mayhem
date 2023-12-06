using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy : MythologyMayhem
{
    [Header("2D Components")]
    public Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;

    [Header("3D Components")]
    public Rigidbody rigidBody3D;
    public NavMeshAgent agent;

    [Header("Stats")]
    public Dimension enemyDimension;
    public Health health;
    public Animator animator;
    public GameObject player;
    [SerializeField] public float attackRate;
    public float attackDamage;
    [SerializeField] private bool canAttack = true;
    [SerializeField] public GameObject attackCollider;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;
    public Vector3 target;
    public float idleTimerMax = 3f;
    public float idleTimer;

    public bool CanAttack {
        get {return canAttack;} 
        set {canAttack = value;}
    }

    public UnityEvent IdleDelegate;
    public UnityEvent PatrolDelegate;
    public UnityEvent AttackDelegate;
    public UnityEvent DeadDelegate;
    public LocalGameManager _localGameManager;

    public enum EnemyStates { Idle, Patrol, Attack, Dead };
    public EnemyStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    void Start()
    {
        health = gameObject.GetComponent<Health>();

        currentState = EnemyStates.Idle;
        currentStatePosition = StatePosition.Entry;
        StartCoroutine(SwitchStates(currentState,0));

        GameObject tempLocalManager = GameObject.FindGameObjectWithTag("LocalGameManager");

        if (tempLocalManager != null)
        {
            _localGameManager =  tempLocalManager.GetComponent<LocalGameManager>();
        }
        
    }//end Start

    void Update()
    { 
        //set reference to player if in a scene using the StartScene feature

        if(_localGameManager != null && player == null)
        {
            if(_localGameManager.player != null){
                player = _localGameManager.player.gameObject;
            }
        }
        //Check for Death

        if (health.GetHealth() <= 0 && currentState != EnemyStates.Dead)
        {
            StartCoroutine(SwitchStates(EnemyStates.Dead,0));
        }

        // If not transitioning states, invoke state action

        if(currentStatePosition == StatePosition.Current)
        {
            switch (currentState)
            {
                case EnemyStates.Idle:
                    {
                        IdleDelegate.Invoke();
                        break;
                    }
                case EnemyStates.Patrol:
                    {
                        PatrolDelegate.Invoke();
                        break;
                    }
                case EnemyStates.Attack:
                    {
                        AttackDelegate.Invoke();
                        break;
                    }
                case EnemyStates.Dead:
                    {
                        DeadDelegate.Invoke();
                        currentStatePosition = StatePosition.Exit;
                        break;
                    }
                default: { break; }
            }
        }        
    }//end update

    public IEnumerator SwitchStates(EnemyStates newState, float delay)
    {
        ExitState(currentState);
        yield return new WaitForSeconds(delay);
        currentState = newState;
        EnterState(currentState);
        currentStatePosition = StatePosition.Current;
    }//switch states

    public void EnterState(EnemyStates newState)
    {
        currentStatePosition = StatePosition.Entry;

        switch (newState)
        {
            case EnemyStates.Idle:
                {
                    idleTimer = idleTimerMax;
                    break;
                }
            case EnemyStates.Patrol:
                {
                    //Update waypoint
                    if(waypoints.Length > 0)
                    {
                        if(waypointIndex >= waypoints.Length - 1)
                        {
                            waypointIndex = 0;
                        } else
                        {
                            waypointIndex++;
                        }

                        //Update target
                        target = waypoints[waypointIndex].position;
                    }                    

                    //Update Nav mesh agent
                    if(agent != null)
                    {
                        agent.speed = walkSpeed;
                    }
                    break;
                }
            case EnemyStates.Attack:
                {
                    //Update Nav mesh agent
                    if (agent != null)
                    {
                        agent.speed = runSpeed;
                    }
                    break;
                }
            case EnemyStates.Dead:
                {
                    break;
                }
            default: { break; }
        }
    }

    public void ExitState(EnemyStates oldState)
    {
        currentStatePosition = StatePosition.Exit;

        switch (oldState)
        {
            case EnemyStates.Idle:
                {
                    break;
                }
            case EnemyStates.Patrol:
                {
                    break;
                }
            case EnemyStates.Attack:
                {
                    canAttack = false;
                    break;
                }
            case EnemyStates.Dead:
                {
                    canAttack = false;
                    break;
                }
            default: { break; }
        }
    }

    public IEnumerator AttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    public bool DetectPlayer()
    {
        if(enemyDimension == Dimension.TwoD)
        {
            if(attackCollider.GetComponent<TriggerDetector2D>().triggered)
            {
                if(attackCollider.GetComponent<TriggerDetector2D>().otherCollider2D.CompareTag("Player"))
                {
                    target = attackCollider.GetComponent<TriggerDetector2D>().otherCollider2D.transform.position;
                    player = attackCollider.GetComponent<TriggerDetector2D>().otherCollider2D.gameObject;
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        } else
        {
            if(attackCollider.GetComponent<TriggerDetector3D>().triggered)
            {
                if (attackCollider.GetComponent<TriggerDetector3D>().otherCollider3D.CompareTag("Player"))
                {
                    target = attackCollider.GetComponent<TriggerDetector3D>().otherCollider3D.transform.position;
                    player = attackCollider.GetComponent<TriggerDetector3D>().otherCollider3D.gameObject;
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
    }
}
