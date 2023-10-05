using System.Collections;
using System.Collections.Generic;
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
    public GameObject player;
    public LayerMask playerLayers;
    public Health health;
    public Animator animator;
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

    public enum EnemyStates { Idle, Patrol, Attack, Dead };
    public EnemyStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = EnemyStates.Idle;
        currentStatePosition = StatePosition.Entry;
        EnterState(currentState);
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //Check for Death

        if (health.GetHealth() <= 0)
        {
            StartCoroutine(SwitchStates(EnemyStates.Dead,0));
        }

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
                    break;
                }
            default: { break; }
        }
    }

    public IEnumerator SwitchStates(EnemyStates newState, float delay)
    {
        ExitState(currentState);
        yield return new WaitForSeconds(delay);
        currentState = newState;
        EnterState(currentState);
    }//switch states

    public void EnterState(EnemyStates newState)
    {
        switch (newState)
        {
            case EnemyStates.Idle:
                {
                    idleTimer = idleTimerMax;
                    currentStatePosition = StatePosition.Current;
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

                    currentStatePosition = StatePosition.Current;
                    break;
                }
            case EnemyStates.Attack:
                {
                    //Update target
                    target = player.transform.position;

                    //Update Nav mesh agent
                    if (agent != null)
                    {
                        agent.speed = runSpeed;
                    }

                    currentStatePosition = StatePosition.Current;
                    break;
                }
            case EnemyStates.Dead:
                {
                    currentStatePosition = StatePosition.Current;
                    break;
                }
            default: { break; }
        }
    }

    public void ExitState(EnemyStates oldState)
    {
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

    public IEnumerator AttackRate() {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
