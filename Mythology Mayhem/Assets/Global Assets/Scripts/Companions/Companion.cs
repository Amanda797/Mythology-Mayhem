using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Companion : MythologyMayhem
{
    [Header("2D Components")]
    public Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;

    [Header("3D Components")]
    public Rigidbody rigidBody3D;
    public NavMeshAgent agent;

    [Header("Stats")]
    public Dimension companionDimension;
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
    public LocalGameManager _localGameManager;

    public enum CompanionStates { Idle, Patrol, Attack, Dead };
    public CompanionStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    void Start()
    {
        health = gameObject.GetComponent<Health>();

        currentState = CompanionStates.Idle;
        currentStatePosition = StatePosition.Entry;
        StartCoroutine(SwitchStates(currentState,0));

        if (player == null)
        {
            if (_localGameManager != null)
            {
                if (_localGameManager.player != null)
                {
                    player = _localGameManager.player.gameObject;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

        }
    }//end Awake

    void Update()
    { 
        //Check for Death

        if (health.GetHealth() <= 0 && currentState != CompanionStates.Dead)
        {
            StartCoroutine(SwitchStates(CompanionStates.Dead,0));
        }

        // If not transitioning states, invoke state action

        if(currentStatePosition == StatePosition.Current)
        {
            switch (currentState)
            {
                case CompanionStates.Idle:
                    {
                        IdleDelegate.Invoke();
                        break;
                    }
                case CompanionStates.Patrol:
                    {
                        PatrolDelegate.Invoke();
                        break;
                    }
                case CompanionStates.Attack:
                    {
                        AttackDelegate.Invoke();
                        break;
                    }
                case CompanionStates.Dead:
                    {
                        DeadDelegate.Invoke();
                        currentStatePosition = StatePosition.Exit;
                        break;
                    }
                default: { break; }
            }
        }        
    }//end update

    public IEnumerator SwitchStates(CompanionStates newState, float delay)
    {
        ExitState(currentState);
        yield return new WaitForSeconds(delay);
        currentState = newState;
        EnterState(currentState);
        currentStatePosition = StatePosition.Current;
    }//switch states

    public void EnterState(CompanionStates newState)
    {
        currentStatePosition = StatePosition.Entry;

        switch (newState)
        {
            case CompanionStates.Idle:
                {
                    idleTimer = idleTimerMax;
                    break;
                }
            case CompanionStates.Patrol:
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
            case CompanionStates.Attack:
                {
                    //Update target
                    target = player.transform.position;

                    //Update Nav mesh agent
                    if (agent != null)
                    {
                        agent.speed = runSpeed;
                    }
                    break;
                }
            case CompanionStates.Dead:
                {
                    break;
                }
            default: { break; }
        }
    }

    public void ExitState(CompanionStates oldState)
    {
        currentStatePosition = StatePosition.Exit;

        switch (oldState)
        {
            case CompanionStates.Idle:
                {
                    break;
                }
            case CompanionStates.Patrol:
                {
                    break;
                }
            case CompanionStates.Attack:
                {
                    canAttack = false;
                    break;
                }
            case CompanionStates.Dead:
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
