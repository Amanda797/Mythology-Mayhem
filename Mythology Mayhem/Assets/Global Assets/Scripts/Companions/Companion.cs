using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Companion : MythologyMayhem
{
    public Scene thisScene;
    public Dimension companionDimension;

    public GameObject player2D;
    public GameObject player3D;

    public float playerRange = 2f;

    public Rigidbody rb3D;
    public Rigidbody2D rb2D;
    public NavMeshAgent agent;
    public Animator anim;
    public string attackTriggerName;

    public Health health;

    public float speed = 9f;
    public float attackDamage = 5f;
    protected bool canAttack;
    protected float attackRate = 3f;
    public float attackTimer = 6f;
    public TriggerDetector attackTrigger;

    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemyLayer;

    public bool CanAttack {
        get {return canAttack;} 
        set {canAttack = value;}
    }

    public UnityEvent FollowPlayerDelegate;
    public UnityEvent AttackDelegate;
    public UnityEvent DeadDelegate;
    public LocalGameManager _localGameManager;

    public enum CompanionStates { Hidden, FollowingPlayer, Patrol, Attack, Dead };
    public CompanionStates currentState;
    public enum StatePosition { Exit, Current, Entry }; //0 = exit, 1 = current, 2 = entry
    public StatePosition currentStatePosition;

    void Start()
    {
        health = gameObject.GetComponent<Health>();

        currentState = CompanionStates.Hidden;
        currentStatePosition = StatePosition.Entry;
        StartCoroutine(SwitchStates(currentState,0));

        
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
                case CompanionStates.Hidden:
                    {
                        //IdleDelegate.Invoke();
                        break;
                    }
                case CompanionStates.Patrol:
                    {
                        //PatrolDelegate.Invoke();
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

        /*switch (newState)
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
    */
    }

    public void ExitState(CompanionStates oldState)
    {
        currentStatePosition = StatePosition.Exit;

        /*switch (oldState)
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
        }*/
    }

    public IEnumerator AttackRate() {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
