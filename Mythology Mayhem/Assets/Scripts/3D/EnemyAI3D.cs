using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3D : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    private float fixedDeltaTime;
    private enum AIState
    {
        Idle,
        Walking,
        Attack,
        Death
    }//end ai state enum

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string walkingBool;
    [SerializeField] private string runningBool;
    [SerializeField] private string attackTrigger;

    [Header("Movement")]
    [SerializeField] private int patrolSpeed = 10;
    [SerializeField] private int attackSpeed = 20;
    [SerializeField] private bool isIdle = false;
    [SerializeField] private float idleTimer = 0f;
    [SerializeField] private float idleDuration = 5f;
    
    [Header("Navigation")]
    [SerializeField] private AIState currAIState;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    [SerializeField] private Vector3 target;

    [Header("Combat")]
    [SerializeField] private Collider player;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] private float attackDuration = 5f;
    private bool isAttacking = false;
    
    // --------------------------
    // ***METHODS***
    // --------------------------
    void Awake() {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 1.0f;
        waypointIndex = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move();
    }//end start

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateAIState();
        //Adjust the current AI state
        switch(currAIState) 
        {
            case AIState.Idle: 
            {
                Idle();
                break;
            }
            case AIState.Walking: 
            {
                Walking();
                break;
            }
            case AIState.Attack: 
            {
                Attack();
                break;
            }
            case AIState.Death: 
            {
                Death();
                break;
            }
            default: 
            {
                print("Calculate AI STate: Defaulted");
                Idle();
                break;
            }
        }//end switch statement
        transform.LookAt(agent.steeringTarget);        
    }//end update

    void CalculateAIState() {
        // Determine death state first
        if(this.GetComponent<Health>().GetHealth() <= 0) {
            currAIState = AIState.Death;
        } else 
        // Determine attack state	
        if(isAttacking) {
            currAIState = AIState.Attack;
        } else 
        // Determine walking state
        if(!isIdle) {
            currAIState = AIState.Walking;
        } else
        /// Determine idle state
        {
            currAIState = AIState.Idle;
        }
    }//end calculate ai state

    void Idle() {
        anim.SetBool(walkingBool, false);
	    agent.speed = 0f;
	    idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration) {
            idleTimer = 0f;
            agent.speed = patrolSpeed;
            isIdle = false;
            agent.isStopped = false;
            Move();
        }
    }//end idle

    void Walking() {
        anim.SetBool(walkingBool, true);
        agent.speed = patrolSpeed;
        //Going to target
        if (Vector3.Distance(transform.position, target) > 2) {
            agent.SetDestination(target);
        }
        //Reached target
        else {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            isIdle = true;
        }
    }//end walking

    void Attack() {
        //Is there a detected player? If so, attack routine
        if(player != null) {
            anim.SetBool(runningBool, true);
            agent.speed = attackSpeed;
            //Go to target
            agent.SetDestination(player.gameObject.transform.position);
            //Attack target once near
            if (Vector3.Distance(transform.position, player.gameObject.transform.position) < 2) {
                if(attackTimer >= attackDuration) {
                    anim.SetTrigger(attackTrigger);
                    player.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                    attackTimer = 0f;
                }
                //Wait to attack again
                else { 
                    agent.ResetPath();
                    agent.speed = 0f;
                    attackTimer += Time.deltaTime;
                }//end timer check            
            }//end proximity check
        } else { //If not, disable running animation
            anim.SetBool(runningBool, false);
        }      
    }//end attack

    void Death() {
        this.GetComponent<Health>().Death();	
    }// end death

    void Move() {
        IterateWaypointDestination();
        UpdateDestination();
    }//end move

    void IterateWaypointDestination() {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }//end iterate waypoint destination

    void UpdateDestination() {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }//end Update Destination

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player" && !isAttacking) {
            Debug.Log("Hit Player!");
            isAttacking = true;
            target = col.gameObject.transform.position;
            player = col;        
        }
    }//end on trigger enter

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player" && isAttacking) {
            Debug.Log("No Player!");
            isAttacking = false;
            player = null;
            UpdateDestination();
        }
    }//end on trigger exit

}