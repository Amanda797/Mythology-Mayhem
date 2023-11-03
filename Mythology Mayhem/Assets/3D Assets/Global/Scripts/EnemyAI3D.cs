using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3D : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    protected float fixedDeltaTime;
    protected enum AIState
    {
        Idle,
        Walking,
        Attack,
        Death
    }//end ai state enum

    [Header("Animation")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected string walkingBool;
    [SerializeField] protected string runningBool;
    [SerializeField] protected string[] attackTriggers;
    [SerializeField] protected AudioSource attackSound;

    [Header("Movement")]
    [SerializeField] protected int patrolSpeed = 10;
    [SerializeField] protected int attackSpeed = 20;
    [SerializeField] protected bool isIdle = false;
    [SerializeField] protected float idleTimer = 0f;
    [SerializeField] protected float idleDuration = 5f;
    
    [Header("Navigation")]
    [SerializeField] protected AIState currAIState;
    [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected int waypointIndex = -1;
    public Vector3 target;

    [Header("Combat")]
    public Collider player;
    [SerializeField] protected int attackDamage = 2;
    [SerializeField] protected float attackTimer = 5f;
    [SerializeField] protected float attackDuration = 5f;
    public bool isAttacking = false;

    /* [Header("Scriptable Abilities")]
    [SerializeField] IdleStateSOBase IdleStateSOBase;
    [SerializeField] PatrolStateSOBase PatrolStateSOBase;
    [SerializeField] AttackStateSOBase AttackStateSOBase;

    public IdleStateSOBase IdleStateSOInstance { get; set; }
    public PatrolStateSOBase PatrolStateSOInstance { get; set; }
    public AttackStateSOBase AttackStateSOInstance { get; set; } */
    
    // --------------------------
    // ***METHODS***
    // --------------------------
    void Awake() {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        attackSound = gameObject.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Move();
    }//end start

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if(currAIState != AIState.Death)
        {
            CalculateAIState();
        }
        
        transform.LookAt(agent.steeringTarget);        
    }//end update

    protected virtual void CalculateAIState() {
        #region Calculate AI State
        // Determine death state first
        if (this.GetComponent<Health>().GetHealth() <= 0) {
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
        #endregion

        #region Adjust Current AI State
        //Adjust the current AI state
        switch (currAIState)
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
                    print("CalculateAIState(): Defaulted");
                    Idle();
                    break;
                }
        }//end switch statement
        #endregion

    }//end calculate ai state

    public virtual void Idle() {
        if(anim != null)
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

    public virtual void Walking() {
        if(anim != null) 
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

    public virtual void Attack() {
        //Using scriptable object instance
        //AttackStateSOInstance.DoStateLogic();

        //Is there a detected player? If so, attack routine
        if(player != null)
        {
            //Attack target once near
            if (Vector3.Distance(transform.position, player.gameObject.transform.position) < 2f) {
                if(anim != null) {
                    anim.SetBool(runningBool, false);
                    anim.SetBool(walkingBool, false); 
                }
                
                if(attackTimer >= attackDuration) {
                    if(anim != null)
                    {
                        anim.SetTrigger(attackTriggers[Random.Range(0,attackTriggers.Length)]);
                    }
                    //Play Sound
                    attackSound.Play();
                    player.gameObject.GetComponent<FPSHealth>().TakeDamage(attackDamage);
                    attackTimer = 0f;
                }
                //Wait to attack again
                else { 
                    agent.ResetPath();
                    agent.speed = 0f;
                    attackTimer += Time.deltaTime;
                }//end timer check            
            }
            else
            {
                if(anim != null) {
                    anim.SetBool(runningBool, true);
                    anim.SetBool(walkingBool, false);
                }
                
                agent.speed = attackSpeed;
                //Go to target
                agent.SetDestination(player.gameObject.transform.position);
            }  
            //end proximity check
        } 
        else 
        { //If not, disable running animation
            if(anim != null)
                anim.SetBool(runningBool, false);
        }      
    }//end attack

    protected virtual void Death() {
        this.GetComponent<Health>().Death();	
    }// end death

    protected virtual void Move() {
        IterateWaypointDestination();
        UpdateDestination();
    }//end move

    protected virtual void IterateWaypointDestination() {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }//end iterate waypoint destination

    protected virtual void UpdateDestination() {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }//end Update Destination

    public virtual void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player" && !isAttacking) {
            isAttacking = true;
            target = col.gameObject.transform.position;
            player = col;        
        }
    }//end on trigger enter

    public virtual void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player" && isAttacking) {
            isAttacking = false;
            player = null;
        }
    }//end on trigger exit    

}